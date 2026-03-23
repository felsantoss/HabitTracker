// Chave usada no armazenamento local para persistir o JWT no navegador.
const TOKEN_KEY = 'habit-tracker-token'

// Lê o token do localStorage; retorna string vazia quando não existe.
export function getToken() {
  return localStorage.getItem(TOKEN_KEY) || ''
}

// Persiste o token após login bem-sucedido.
export function saveToken(token) {
  localStorage.setItem(TOKEN_KEY, token)
}

// Remove o token no logout ou no reset da sessão.
export function clearToken() {
  localStorage.removeItem(TOKEN_KEY)
}
