const API_BASE_URL = "https://localhost:7250/api";

export async function getEmployeeUsingFetch() {
  const response = await fetch(`${API_BASE_URL}/Employees`);

  if (!response.ok) {
    throw new Error("Failed to fetch employees");
  }
  const result = await response.json();
  return result.data;
}

export async function getDepartmentsUsingFetch() {
  const response = await fetch(`${API_BASE_URL}/Departments`);

  if (!response.ok) {
    throw new Error("Failed to fetch departments");
  }

  const result = await response.json();

  return result.data;
}
export async function createEmployeeUsingFetch(employeeData) {
  const response = await fetch(`${API_BASE_URL}/Employees`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(employeeData),
  });
  if (!response.ok) {
    const errorResult = await response.json();
    throw new Error(errorResult.message || "Failed to create employee");
  }

  const result = await response.json();
  return result.data;
}

export async function updateEmployeeUsingFetch(employeeId, employeeData) {
  const response = await fetch(`${API_BASE_URL}/Employees/${employeeId}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(employeeData),
  });
  if (!response.ok) {
    const errorResult = await response.json();
    throw new Error(errorResult.message || "Failed to update employee");
  }

  const result = await response.json();
  return result.data;
}

export async function deleteEmployeeUsingFetch(employeeId) {
  const response = await fetch(`${API_BASE_URL}/Employees/${employeeId}`, {
    method: "DELETE",
  });
  if (!response.ok) {
    const errorResult = await response.json();
    throw new Error(errorResult.message || "Failed to delete employee");
  }

  const result = await response.json();
  return result.message;
}
