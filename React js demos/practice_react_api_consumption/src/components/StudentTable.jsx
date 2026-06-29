function StudentTable({ students, onEdit, onDelete }) {
  if (students.length === 0) return <p>No students found.</p>;

  return (
    <table className="student-table">
      <thead>
        <tr>
          <th>ID</th>
          <th>Name</th>
          <th>Email</th>
          <th>Age</th>
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>
        {students.map((s) => (
          <tr key={s.id}>
            <td>{s.id}</td>
            <td>{s.name}</td>
            <td>{s.email}</td>
            <td>{s.age}</td>
            <td>
              <button onClick={() => onEdit(s)}>Edit</button>
              <button className="delete-btn" onClick={() => onDelete(s.id)}>Delete</button>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}

export default StudentTable;
