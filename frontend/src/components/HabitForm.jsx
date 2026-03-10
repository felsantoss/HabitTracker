import { useState } from 'react'

const INITIAL_VALUES = {
  title: '',
  description: '',
  date: '',
}

function HabitForm({ loading, onSubmit }) {
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
