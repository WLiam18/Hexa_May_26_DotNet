export function EmployeeTable({ employees, onEditEmployee, onDeleteEmployee }) {
  if (employees.length === 0) {
    return <p>No employees found.</p>;
  }

  return (
    <table className="employee-table">
      <thead>
        <tr>
          <th>ID</th>
          <th>Employee Name</th>
          <th>Gender</th>
          <th>Salary</th>
          <th>City</th>
          <th>Department</th>
          <th>Actions</th>
        </tr>
      </thead>

      <tbody>
        {employees.map((employee) => (
          <tr key={employee.employeeId}>
            <td>{employee.employeeId}</td>
            <td>{employee.employeeName}</td>
            <td>{employee.gender}</td>
            <td>{employee.salary}</td>
            <td>{employee.city}</td>
            <td>{employee.departmentName}</td>
            <td>
              <button onClick={() => onEditEmployee(employee)}>Edit</button>

              <button
                className="delete-button"
                onClick={() => onDeleteEmployee(employee.employeeId)}
              >
                Delete
              </button>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}
