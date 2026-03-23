import { useState } from 'react'

// Estado inicial dos campos do formulário de criação de hábito.
const INITIAL_VALUES = {
  title: '',
  description: '',
  date: '',
}

// Coleta dados do hábito e delega o envio para o componente pai.
function HabitForm({ loading, onSubmit }) {
  const [formData, setFormData] = useState(INITIAL_VALUES)

  // Mantém o estado do formulário sincronizado com input/textarea por name.
  function handleChange(event) {
    const { name, value } = event.target
    setFormData((current) => ({
      ...current,
      [name]: value,
    }))
  }

  // Evita reload da página, envia dados ao pai e limpa só em caso de sucesso.
  async function handleSubmit(event) {
    event.preventDefault()
    const success = await onSubmit(formData)

    if (success) {
      setFormData(INITIAL_VALUES)
    }
  }

  return (
    <form className="form-grid" onSubmit={handleSubmit}>
      <label>
        Title
        <input
          name="title"
          onChange={handleChange}
          placeholder="Read 10 pages"
          required
          value={formData.title}
        />
      </label>

      <label>
        Description
        <textarea
          name="description"
          onChange={handleChange}
          placeholder="A small daily reading goal"
          rows="4"
          value={formData.description}
        />
      </label>

      <label>
        Start date
        <input
          name="date"
          onChange={handleChange}
          type="date"
          value={formData.date}
        />
      </label>

      <button disabled={loading} type="submit">
        {loading ? 'Creating...' : 'Create habit'}
      </button>
    </form>
  )
}

export default HabitForm
