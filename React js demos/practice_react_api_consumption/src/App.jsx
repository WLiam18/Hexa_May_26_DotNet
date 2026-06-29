import { useState, useEffect } from "react";
import { getStudents, createStudent, updateStudent, deleteStudent } from "./api/studentApi";
import StudentForm from "./components/StudentForm";
import StudentTable from "./components/StudentTable";
import "./App.css";

function App() {
  const [students, setStudents] = useState([]);
  const [selected, setSelected] = useState(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");

  useEffect(() => {
    loadStudents();
  }, []);

  async function loadStudents() {
    setLoading(true);
    setError("");
    try {
      const data = await getStudents();
      setStudents(data);
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  }

  async function handleSubmit(data) {
    setError("");
    setSuccess("");
    try {
      if (selected) {
        await updateStudent(selected.id, data);
        setSuccess("Student updated!");
      } else {
        await createStudent(data);
        setSuccess("Student added!");
      }
      setSelected(null);
      await loadStudents();
    } catch (err) {
      setError(err.message);
    }
  }

  function handleEdit(student) {
    setSelected(student);
  }

  function handleCancel() {
    setSelected(null);
  }

async function handleDelete(id) {
  if (!window.confirm("Delete this student?")) return;
  setError("");
  setSuccess("");
  try {
    await deleteStudent(id);
    setSuccess("Student deleted!");
    await loadStudents();
  } catch (err) {
    setError(err.message);
  }
}

  return (
    <div className="app">
      <h1>Student Management</h1>

      {loading && <p>Loading...</p>}
      {error && <p className="error">{error}</p>}
      {success && <p className="success">{success}</p>}

      <StudentForm selected={selected} onSubmit={handleSubmit} onCancel={handleCancel} />
      <StudentTable students={students} onEdit={handleEdit} onDelete={handleDelete} />
    </div>
  );
}

export default App;
