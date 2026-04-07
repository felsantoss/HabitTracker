import { useState } from 'react'

const INITIAL_EDIT_VALUES = {
  title: '',
  description: '',
}

// Converte datas vindas do backend para um formato local amigável.
function formatDate(dateString) {
  // Quando o hábito não tem data inicial, mostra um texto padrão.
  if (!dateString) {
    return 'No date'
  }

  return new Date(dateString).toLocaleDateString()
}

// Renderiza um card de hábito com detalhes e ação de check-in.
function HabitCard({
  actionLoading,
  habit,
  onArchive,
  onCheckIn,
  onUpdate,
}) {
  const [isEditing, setIsEditing] = useState(false)
  const [editData, setEditData] = useState(INITIAL_EDIT_VALUES)
  const isBusy = actionLoading === habit.id

  function handleStartEdit() {
    setEditData({
      title: habit.title,
      description: habit.description || '',
    })
    setIsEditing(true)
  }

  function handleCancelEdit() {
    setEditData(INITIAL_EDIT_VALUES)
    setIsEditing(false)
  }

  function handleEditChange(event) {
    const { name, value } = event.target
    setEditData((current) => ({
      ...current,
      [name]: value,
    }))
  }

  async function handleSubmit(event) {
    event.preventDefault()
    const success = await onUpdate(habit.id, editData)

    if (success) {
      setIsEditing(false)
    }
  }

  return (
    <article className="habit-card">
      {isEditing ? (
        <form className="form-grid habit-edit-form" onSubmit={handleSubmit}>
          <label>
            Title
            <input
              disabled={isBusy}
              name="title"
              onChange={handleEditChange}
              required
              value={editData.title}
            />
          </label>

          <label>
            Description
            <textarea
              disabled={isBusy}
              name="description"
              onChange={handleEditChange}
              rows="4"
              value={editData.description}
            />
          </label>

          <div className="habit-actions">
            <button disabled={isBusy} type="submit">
              {isBusy ? 'Saving...' : 'Save'}
            </button>
            <button
              className="ghost-button"
              disabled={isBusy}
              onClick={handleCancelEdit}
              type="button"
            >
              Cancel
            </button>
          </div>
        </form>
      ) : (
        <>
          <div className="habit-card-header">
            <div>
              <h3>{habit.title}</h3>
              <p className="habit-date">
                Started on {formatDate(habit.startDate)}
              </p>
            </div>
            {/* Dispara o check-in diário para o id do hábito atual. */}
            <button
              disabled={isBusy}
              onClick={() => onCheckIn(habit.id)}
              type="button"
            >
              Check in today
            </button>
          </div>

          {/* Se não houver descrição, mantém a UI informativa com texto padrão. */}
          <p className="habit-description">
            {habit.description || 'No description provided.'}
          </p>

          <div className="habit-actions">
            <button
              className="ghost-button"
              disabled={isBusy}
              onClick={handleStartEdit}
              type="button"
            >
              Edit
            </button>
            <button
              className="ghost-button danger-button"
              disabled={isBusy}
              onClick={() => onArchive(habit.id)}
              type="button"
            >
              {isBusy ? 'Archiving...' : 'Archive'}
            </button>
          </div>
        </>
      )}
    </article>
  )
}

export default HabitCard
