import { useState, useEffect } from "react";

const emptyForm = { name: "", email: "", age: "" };

function StudentForm({ selected, onSubmit, onCancel }) {
  const [form, setForm] = useState(emptyForm);

  useEffect(() => {
    if (selected) {
      setForm({
        name: selected.name || "",
        email: selected.email || "",
        age: selected.age || "",
      });
    } else {
      setForm(emptyForm);
    }
  }, [selected]);

  function handleChange(e) {
    const { name, value } = e.target;
    setForm((prev) => ({ ...prev, [name]: value }));
  }

  function handleSubmit(e) {
    e.preventDefault();
    onSubmit({
      name: form.name,
      email: form.email,
      age: Number(form.age),
    });
  }

  return (
    <form className="student-form" onSubmit={handleSubmit}>
      <h2>{selected ? "Edit Student" : "Add Student"}</h2>

      <div>
        <label>Name</label>
        <input type="text" name="name" value={form.name} onChange={handleChange} required />
      </div>

      <div>
        <label>Email</label>
        <input type="email" name="email" value={form.email} onChange={handleChange} required />
      </div>

      <div>
        <label>Age</label>
        <input type="number" name="age" value={form.age} onChange={handleChange} required />
      </div>

      <button type="submit">{selected ? "Update" : "Add"}</button>
      {selected && <button type="button" onClick={onCancel}>Cancel</button>}
    </form>
  );
}

export default StudentForm;
