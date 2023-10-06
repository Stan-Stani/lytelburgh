import { useState } from 'react'

export const Input = () => {
  const [value, setValue] = useState('')

  return (
    <>
      <button>😊</button>
      <input
        value={value}
        onChange={(e) => {
          setValue(e.target.value)
        }}
      />
    </>
  )
}
