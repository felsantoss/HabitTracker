import { useState } from 'react'

const INITIAL_VALUES = {
  name: '',
  email: '',
  password: '',
}

function AuthForm({
  mode,
  loading,
  authError,
  authMessage,
  onSubmit,
  onToggleMode,
}) {
  const [formData, setFormData] = useState(INITIAL_VALUES)

  function handleChange(event) {
    const { name, value } = event.target
    setFormData((current) => ({
      ...current,
      [name]: value,
    }))
  }

  async function handleSubmit(event) {
    event.preventDefault()
    const success = await onSubmit(formData)

    if (!success) {
      return
    }

    if (mode === 'register') {
      setFormData((current) => ({
        ...current,
        password: '',
      }))
      return
    }

    setFormData(INITIAL_VALUES)
  }

  function handleModeChange(nextMode) {
    onToggleMode(nextMode)
    setFormData(INITIAL_VALUES)
  }

  const isRegister = mode === 'register'

  return (
    <section className="panel auth-panel">
      <div className="auth-switch">
        <button
          className={!isRegister ? 'active' : ''}
          onClick={() => handleModeChange('login')}
          type="button"
        >
          Login
        </button>
        <button
          className={isRegister ? 'active' : ''}
          onClick={() => handleModeChange('register')}
          type="button"
        >
          Register
        </button>
      </div>

      <h2>{isRegister ? 'Create account' : 'Welcome back'}</h2>
      <p className="section-copy">
        {isRegister
          ? 'Use the same backend validation rules from your API.'
          : 'Log in with the JWT endpoint already exposed by the API.'}
      </p>

      <form className="form-grid" onSubmit={handleSubmit}>
        {isRegister && (
          <label>
            Name
            <input
              name="name"
              onChange={handleChange}
              placeholder="Felipe"
              required
              value={formData.name}
            />
          </label>
        )}

        <label>
          Email
          <input
            name="email"
            onChange={handleChange}
            placeholder="name@email.com"
            required
            type="email"
            value={formData.email}
          />
        </label>

        <label>
          Password
          <input
            name="password"
            onChange={handleChange}
            placeholder="Your password"
            required
            type="password"
            value={formData.password}
          />
        </label>

        {authError && <p className="form-error">{authError}</p>}
        {authMessage && <p className="form-success">{authMessage}</p>}

        <button disabled={loading} type="submit">
          {loading
            ? 'Please wait...'
            : isRegister
              ? 'Create account'
              : 'Login'}
        </button>
      </form>
    </section>
  )
}

export default AuthForm
