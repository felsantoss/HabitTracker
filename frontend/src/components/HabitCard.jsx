function formatDate(dateString) {
  if (!dateString) {
    return 'No date'
  }

  return new Date(dateString).toLocaleDateString()
}

function HabitCard({ habit, onCheckIn }) {
  return (
    <article className="habit-card">
      <div className="habit-card-header">
        <div>
          <h3>{habit.title}</h3>
          <p className="habit-date">Started on {formatDate(habit.startDate)}</p>
        </div>
        <button onClick={() => onCheckIn(habit.id)} type="button">
          Check in today
        </button>
      </div>

      <p className="habit-description">
        {habit.description || 'No description provided.'}
      </p>
    </article>
  )
}

export default HabitCard
