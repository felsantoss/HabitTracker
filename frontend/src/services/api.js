const API_BASE_URL = import.meta.env.VITE_API_BASE_URL ?? 'http://localhost:5145'

async function request(path, options = {}) {
  const response = await fetch(`${API_BASE_URL}${path}`, {
    headers: {
      'Content-Type': 'application/json',
      ...options.headers,
    },
    ...options,
  })

  const contentType = response.headers.get('content-type') || ''
  const isJson = contentType.includes('application/json')
  const body = isJson ? await response.json() : await response.text()

  if (!response.ok) {
    const message =
      typeof body === 'string'
        ? body
        : body?.message || body?.title || 'Request failed.'

    const error = new Error(message)
    error.status = response.status
    throw error
  }

  return body
}

export function registerUser(payload) {
  return request('/v1/api/user', {
    method: 'POST',
    body: JSON.stringify(payload),
  })
}

export function loginUser(payload) {
  return request('/v1/api/authentication/login', {
    method: 'POST',
    body: JSON.stringify(payload),
  })
}

export function getHabits({ token, pageNumber, pageSize }) {
  return request(`/api/v1/habit?pageNumber=${pageNumber}&pageSize=${pageSize}`, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  })
}

export function createHabit({ token, payload }) {
  const body = {
    ...payload,
    date: payload.date || null,
  }

  return request('/api/v1/habit', {
    method: 'POST',
    headers: {
      Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify(body),
  })
}

export function createCheckIn({ token, habitId }) {
  return request(`/api/v1/habit/${habitId}/checkin`, {
    method: 'POST',
    headers: {
      Authorization: `Bearer ${token}`,
    },
  })
}
