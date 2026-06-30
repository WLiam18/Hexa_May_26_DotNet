import axios from "axios";

const axiosClient = axios.create({
  baseURL: "https://localhost:7250/api",
  headers: {
    "Content-Type": "application/json",
  },
});

function getErrorMessage(error, fallbackMessage) {
  if (error.response?.data?.message) {
    return error.response.data.message;
  }

  if (error.response?.data?.title) {
    return error.response.data.title;
  }

  if (error.message) {
    return error.message;
  }
  return fallbackMessage;
}

export async function getEmployeesUsingAxios() {
  try {
    const response = await axiosClient.get("/Employees");
    return response.data.data;
  } catch (error) {
    throw new Error(getErrorMessage(error, "Failed to fetch employees."));
  }
}

export async function getDepartmentsUsingAxios() {
  try {
    const response = await axiosClient.get("/Departments");
    return response.data.data;
  } catch (error) {
    throw new Error(getErrorMessage(error, "Failed to fetch departments."));
  }
}

export async function createEmployeeUsingAxios(employeeData) {
  try {
    const response = await axiosClient.post("/Employees", employeeData);
    return response.data.data;
  } catch (error) {
    throw new Error(getErrorMessage(error, "Failed to Add employee."));
  }
}

export async function updateteEmployeeUsingAxios(employeeId, employeeData) {
  try {
    const response = await axiosClient.put(
      `/Employees/${employeeId}`,
      employeeData,
    );
    return response.data.data;
  } catch (error) {
    throw new Error(getErrorMessage(error, "Failed to  update employee."));
  }
}

export async function deleteEmployeeUsingAxios(employeeId) {
  try {
    const response = await axiosClient.delete(`/Employees/${employeeId}`);
    return response.data.data;
  } catch (error) {
    throw new Error(getErrorMessage(error, "Failed to delete Employee."));
  }
}
