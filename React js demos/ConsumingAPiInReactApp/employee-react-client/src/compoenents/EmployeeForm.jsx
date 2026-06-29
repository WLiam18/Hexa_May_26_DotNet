import { useState, useEffect } from "react";

const emptyEmployee = {
  employeeName: "",
  gender: "",
  salary: "",
  city: "",
  departmentId: "",
};

export function EmployeeForm({
  departments,
  selectedEmployee,
  onSubmitEmployee,
  onCancelEdit,
}) {
  const [employeeData, setEmployeeData] = useState(emptyEmployee);

  useEffect(() => {
    if (selectedEmployee) {
      setEmployeeData({
        employeeName: selectedEmployee.employeeName,
        gender: selectedEmployee.gender,
        salary: selectedEmployee.salary,
        city: selectedEmployee.city,
        departmentId: selectedEmployee.departmentId,
      });
    } else {
      setEmployeeData(emptyEmployee);
    }
  }, [selectedEmployee]);

  function handleInputChange(event) {
    const { name, value } = event.target;
    setEmployeeData({
      ...employeeData,
      [name]: value,
    });
  }

  function handleSubmit(event) {
    event.preventDefault();

    const finalEmployeeData = {
      employeeName: employeeData.employeeName,
      gender: employeeData.gender,
      salary: Number(employeeData.salary),
      city: employeeData.city,
      departmentId: Number(employeeData.departmentId),
    };

    onSubmitEmployee(finalEmployeeData);
  }

  return (
    <form className="employee-form" onSubmit={handleSubmit}>
      <h2>{selectedEmployee ? "Edit Employee" : "Add Employee"}</h2>

      <div>
        <label>Employee Name</label>
        <input
          type="text"
          name="employeeName"
          value={employeeData.employeeName}
          onChange={handleInputChange}
          required
        />
      </div>

      <div>
        <label>Gender</label>
        <select
          name="gender"
          value={employeeData.gender}
          onChange={handleInputChange}
          required
        >
          <option value="">-- Select Gender --</option>
          <option value="Male">Male</option>
          <option value="Female">Female</option>
        </select>
      </div>

      <div>
        <label>Salary</label>
        <input
          type="number"
          name="salary"
          value={employeeData.salary}
          onChange={handleInputChange}
          required
        />
      </div>

      <div>
        <label>City</label>
        <input
          type="text"
          name="city"
          value={employeeData.city}
          onChange={handleInputChange}
          required
        />
      </div>

      <div>
        <label>Department</label>
        <select
          name="departmentId"
          value={employeeData.departmentId}
          onChange={handleInputChange}
          required
        >
          <option value="">-- Select Department --</option>

          {departments.map((department) => (
            <option
              key={department.departmentId}
              value={department.departmentId}
            >
              {department.departmentName}
            </option>
          ))}
        </select>
      </div>

      <button type="submit">
        {selectedEmployee ? "Update Employee" : "Add Employee"}
      </button>

      {selectedEmployee && (
        <button type="button" onClick={onCancelEdit}>
          Cancel
        </button>
      )}
    </form>
  );
}
