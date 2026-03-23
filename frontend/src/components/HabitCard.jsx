// Converte datas vindas do backend para um formato local amigável.
function formatDate(dateString) {
  // Quando o hábito não tem data inicial, mostra um texto padrão.
  if (!dateString) {
    return 'No date'
  }

  return new Date(dateString).toLocaleDateString()
}

// Renderiza um card de hábito com detalhes e ação de check-in.
function HabitCard({ habit, onCheckIn }) {
  return (
    <article className="habit-card">
      <div className="habit-card-header">
        <div>
          <h3>{habit.title}</h3>
          <p className="habit-date">Started on {formatDate(habit.startDate)}</p>
        </div>
        {/* Dispara o check-in diário para o id do hábito atual. */}
        <button onClick={() => onCheckIn(habit.id)} type="button">
          Check in today
        </button>
      </div>

      {/* Se não houver descrição, mantém a UI informativa com texto padrão. */}
      <p className="habit-description">
        {habit.description || 'No description provided.'}
      </p>
    </article>
  )
}

export default HabitCard
