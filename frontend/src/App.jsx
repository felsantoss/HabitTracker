import { useCallback, useEffect, useRef, useState } from 'react'
import './App.css'
import AuthForm from './components/AuthForm.jsx'
import HabitCard from './components/HabitCard.jsx'
import HabitForm from './components/HabitForm.jsx'
import { clearToken, getToken, saveToken } from './services/auth.js'
import {
  createCheckIn,
  createHabit,
  getHabits,
  loginUser,
  registerUser,
} from './services/api.js'

// Componente raiz: coordena o fluxo de autenticação e o estado do dashboard.
function App() {
  const [token, setToken] = useState(() => getToken())
  const [mode, setMode] = useState('login')
  const [habits, setHabits] = useState([])
  const [pagination, setPagination] = useState({
    pageNumber: 1,
    pageSize: 10,
    totalItems: 0,
    totalPages: 0,
  })
  const [authLoading, setAuthLoading] = useState(false)
  const [habitsLoading, setHabitsLoading] = useState(false)
  const [habitSubmitting, setHabitSubmitting] = useState(false)
  const [authError, setAuthError] = useState('')
  const [authMessage, setAuthMessage] = useState('')
  const [dashboardMessage, setDashboardMessage] = useState('')
  // Mantém o pageSize mais recente sem recriar a função loadHabits.
  const pageSizeRef = useRef(pagination.pageSize)

  // Limpa a autenticação persistida e reseta o estado do dashboard.
  const handleLogout = useCallback(() => {
    clearToken()
    setToken('')
    setHabits([])
    setPagination({
      pageNumber: 1,
      pageSize: 10,
      totalItems: 0,
      totalPages: 0,
    })
  }, [])

  // Busca hábitos da página solicitada e atualiza os metadados da paginação.
  const loadHabits = useCallback(
    async (currentToken, pageNumber) => {
      setHabitsLoading(true)
      setDashboardMessage('')

      try {
        const response = await getHabits({
          token: currentToken,
          pageNumber,
          pageSize: pageSizeRef.current,
        })

        setHabits(response.items)
        setPagination((current) => ({
          ...current,
          pageNumber: response.pageNumber,
          pageSize: response.pageSize,
          totalItems: response.totalItems,
          totalPages: response.totalPages,
        }))
      } catch (error) {
        if (error.status === 401) {
          handleLogout()
          setAuthError('Your session expired. Please log in again.')
          return
        }

        setDashboardMessage(error.message)
      } finally {
        setHabitsLoading(false)
      }
    },
    [handleLogout],
  )

  // Quando token ou página atual mudam, recarrega os hábitos do dashboard.
  useEffect(() => {
    if (!token) {
      setHabits([])
      return
    }

    loadHabits(token, pagination.pageNumber)
  }, [token, pagination.pageNumber, loadHabits])

  // Executa registro/login conforme o modo atual e salva JWT no login.
  async function handleAuthenticate(formData) {
    setAuthLoading(true)
    setAuthError('')
    setAuthMessage('')

    try {
      if (mode === 'register') {
        await registerUser(formData)
        setMode('login')
        setAuthMessage('Account created. You can log in now.')
        return true
      }

      const response = await loginUser(formData)
      saveToken(response.token)
      setToken(response.token)
      setAuthMessage('')
      return true
    } catch (error) {
      setAuthError(error.message)
      return false
    } finally {
      setAuthLoading(false)
    }
  }

  // Cria um novo hábito, adiciona no topo da UI e atualiza o total.
  async function handleCreateHabit(formData) {
    setHabitSubmitting(true)
    setDashboardMessage('')

    try {
      const habit = await createHabit({ token, payload: formData })
      setHabits((current) => [habit, ...current])
      setPagination((current) => ({
        ...current,
        totalItems: current.totalItems + 1,
      }))
      setDashboardMessage('Habit created successfully.')
      return true
    } catch (error) {
      setDashboardMessage(error.message)
      return false
    } finally {
      setHabitSubmitting(false)
    }
  }

  // Realiza o check-in de hoje para um hábito específico.
  async function handleCheckIn(habitId) {
    setDashboardMessage('')

    try {
      await createCheckIn({ token, habitId })
      setDashboardMessage('Check-in completed for today.')
    } catch (error) {
      setDashboardMessage(error.message)
    }
  }

  // Muda para outra página; o reload dos dados é feito pelo useEffect.
  function handlePageChange(nextPage) {
    setPagination((current) => ({
      ...current,
      pageNumber: nextPage,
    }))
  }

  if (!token) {
    return (
      <main className="shell auth-shell">
        <section className="hero-panel">
          <p className="eyebrow">Habit Tracker</p>
          <h1>Track habits without a complex frontend.</h1>
          <p className="hero-copy">
            This first version keeps the flow focused: create an account, log in,
            register your habits, and check in every day.
          </p>
          <ul className="hero-points">
            <li>JWT authentication integrated with your backend</li>
            <li>Simple dashboard for daily usage</li>
            <li>Clean code split into components and API services</li>
          </ul>
        </section>

        <AuthForm
          authError={authError}
          authMessage={authMessage}
          loading={authLoading}
          mode={mode}
          onSubmit={handleAuthenticate}
          onToggleMode={setMode}
        />
      </main>
    )
  }

  return (
    <main className="shell app-shell">
      <header className="topbar">
        <div>
          <p className="eyebrow">Habit Tracker</p>
          <h1 className="dashboard-title">Your daily dashboard</h1>
        </div>
        <button className="ghost-button" onClick={handleLogout} type="button">
          Logout
        </button>
      </header>

      <section className="dashboard-grid">
        <aside className="panel">
          <h2>Create a new habit</h2>
          <p className="section-copy">
            Start with the smallest useful rule you want to repeat.
          </p>
          <HabitForm loading={habitSubmitting} onSubmit={handleCreateHabit} />
        </aside>

        <section className="panel">
          <div className="section-header">
            <div>
              <h2>My habits</h2>
              <p className="section-copy">
                {pagination.totalItems} tracked habit
                {pagination.totalItems === 1 ? '' : 's'}
              </p>
            </div>
            <button
              className="ghost-button"
              onClick={() => loadHabits(token, pagination.pageNumber)}
              type="button"
            >
              Refresh
            </button>
          </div>

          {dashboardMessage && (
            <p className="inline-message">{dashboardMessage}</p>
          )}

          {habitsLoading ? (
            <p className="empty-state">Loading habits...</p>
          ) : habits.length === 0 ? (
            <p className="empty-state">
              No habits yet. Create your first one on the left.
            </p>
          ) : (
            <div className="habit-list">
              {habits.map((habit) => (
                <HabitCard
                  habit={habit}
                  key={habit.id}
                  onCheckIn={handleCheckIn}
                />
              ))}
            </div>
          )}

          {pagination.totalPages > 1 && (
            <div className="pagination">
              <button
                disabled={pagination.pageNumber === 1}
                onClick={() => handlePageChange(pagination.pageNumber - 1)}
                type="button"
              >
                Previous
              </button>
              <span>
                Page {pagination.pageNumber} of {pagination.totalPages}
              </span>
              <button
                disabled={pagination.pageNumber === pagination.totalPages}
                onClick={() => handlePageChange(pagination.pageNumber + 1)}
                type="button"
              >
                Next
              </button>
            </div>
          )}
        </section>
      </section>
    </main>
  )
}

export default App
