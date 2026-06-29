import { useState, useEffect } from "react";
import { EmployeeTable } from "../compoenents/EmployeeTable";
import { EmployeeForm } from "../compoenents/EmployeeForm";
import {
  getEmployeeUsingFetch,
  createEmployeeUsingFetch,
  updateEmployeeUsingFetch,
  deleteEmployeeUsingFetch,
  getDepartmentsUsingFetch,
} from "../api/employeeFetchApi";

export function EmployeeFetchDemo() {
  const [employees, setEmployees] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  const [errorMessage, setErrorMessage] = useState("");
  const [departments, setDepartments] = useState([]);
  const [selectedEmployee, setSelectedEmployee] = useState(null);
  const [message, setMessage] = useState("");
  useEffect(() => {
    loadInitialData();
  }, []);

  async function loadInitialData() {
    setIsLoading(true);
    try {
      const employeeList = await getEmployeeUsingFetch();
      const departmentList = await getDepartmentsUsingFetch();

      setEmployees(employeeList);
      setDepartments(departmentList);
    } catch (error) {
      setErrorMessage(error.message);
    } finally {
      setIsLoading(false);
    }
  }

  async function handleSubmitEmployee(employeeData) {
    try {
      setErrorMessage("");
      setMessage("");

      if (selectedEmployee) {
        await updateEmployeeUsingFetch(
          selectedEmployee.employeeId,
          employeeData,
        );

        setMessage("Employee updated successfully. ");
      } else {
        await createEmployeeUsingFetch(employeeData);

        setMessage("Employee Added successfully.");
      }

      setSelectedEmployee(null);
      await loadInitialData();
    } catch (error) {
      setErrorMessage(error.message);
    }
  }
  function handleEditEmployee(employee) {
    setSelectedEmployee(employee);
  }

  function handleCancelEdit() {
    setSelectedEmployee(null);
  }

  async function handleDeleteEmployee(employeeId) {
    const confirmDelete = window.confirm(
      "Are you sure you want to delete this employee?",
    );

    if (!confirmDelete) {
      return;
    }

    try {
      setErrorMessage("");
      setMessage("");

      await deleteEmployeeUsingFetch(employeeId);

      setMessage("Employee deleted successfully.");

      await loadInitialData();
    } catch (error) {
      setErrorMessage(error.message);
    }
  }

  return (
    <div className="page">
      <h1>Employee CRUD using fetch()</h1>
      <p>
        This page uses browser built-in <strong>fetch()</strong> to consume the
        ASP.NET Core Web API.
      </p>

      {isLoading && <p>Loading employees...</p>}

      {message && <p className="success-message">{message}</p>}

      {errorMessage && <p className="error-message">{errorMessage}</p>}
      <EmployeeForm
        departments={departments}
        selectedEmployee={selectedEmployee}
        onSubmitEmployee={handleSubmitEmployee}
        onCancelEdit={handleCancelEdit}
      />
      <EmployeeTable
        employees={employees}
        onEditEmployee={handleEditEmployee}
        onDeleteEmployee={handleDeleteEmployee}
      />
    </div>
  );
}
