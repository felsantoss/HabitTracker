// URL base vem da variável de ambiente do Vite; localhost é fallback local.
const API_BASE_URL = import.meta.env.VITE_API_BASE_URL ?? 'http://localhost:5145'

// Função HTTP genérica usada por todas as chamadas de API deste arquivo.
async function request(path, options = {}) {
  const response = await fetch(`${API_BASE_URL}${path}`, {
    headers: {
      'Content-Type': 'application/json',
      ...options.headers,
    },
    ...options,
  })

  // Faz parse dinâmico da resposta porque alguns endpoints retornam texto.
  const contentType = response.headers.get('content-type') || ''
  const isJson = contentType.includes('application/json')
  const body = isJson ? await response.json() : await response.text()

  // Padroniza erros do backend em Error JavaScript com status HTTP.
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

// Cria uma nova conta de usuário.
export function registerUser(payload) {
  return request('/v1/api/user', {
    method: 'POST',
    body: JSON.stringify(payload),
  })
}

// Autentica as credenciais do usuário e retorna dados do JWT.
export function loginUser(payload) {
  return request('/v1/api/authentication/login', {
    method: 'POST',
    body: JSON.stringify(payload),
  })
}

// Retorna hábitos paginados do usuário autenticado.
export function getHabits({ token, pageNumber, pageSize }) {
  return request(`/api/v1/habit?pageNumber=${pageNumber}&pageSize=${pageSize}`, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  })
}

// Cria um novo hábito; data vazia é enviada como null por compatibilidade.
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

// Registra o check-in diário para um id de hábito específico.
export function createCheckIn({ token, habitId }) {
  return request(`/api/v1/habit/${habitId}/checkin`, {
    method: 'POST',
    headers: {
      Authorization: `Bearer ${token}`,
    },
  })
}
