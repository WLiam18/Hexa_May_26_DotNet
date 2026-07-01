import axios from "axios";

const API_BASE = "http://localhost:5092/api";

// Create axios instance with base URL
const apiClient = axios.create({
  baseURL: API_BASE,
  headers: {
    "Content-Type": "application/json",
  },
});

// GET all students
export async function getStudents() {
  try {
    const response = await apiClient.get("/Students");
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data?.message || "Failed to fetch students");
  }
}

// POST create student
export async function createStudent(data) {
  try {
    const response = await apiClient.post("/Students", data);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data?.message || "Failed to create student");
  }
}

// PUT update student
export async function updateStudent(id, data) {
  try {
    const payload = { id, ...data };
    const response = await apiClient.put(`/Students/${id}`, payload);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data?.message || "Failed to update student");
  }
}

// DELETE student
export async function deleteStudent(id) {
  try {
    await apiClient.delete(`/Students/${id}`);
    return { success: true };
  } catch (error) {
    throw new Error(error.response?.data?.message || "Failed to delete student");
  }
}