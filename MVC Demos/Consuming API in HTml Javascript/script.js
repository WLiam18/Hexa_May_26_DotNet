const apiBaseUrl = "https://localhost:7130/api";

// Check login on page load
window.onload = function () {
  const token = localStorage.getItem("jwtToken");

  if (token) {
    showMainSection();
    loadDoctors();
    loadPatients();
    loadAppointments();
  }
};

// Login API call
async function login() {
  const username = document.getElementById("username").value;
  const password = document.getElementById("password").value;

  const loginData = {
    username: username,
    password: password,
  };

  try {
    const response = await fetch(`${apiBaseUrl}/Auth/login`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(loginData),
    });

    if (!response.ok) {
      document.getElementById("loginMessage").innerText =
        "Invalid username or password.";
      return;
    }

    const result = await response.json();

    if (!result.success || !result.token) {
      document.getElementById("loginMessage").innerText =
        "Login failed. Token not received.";
      return;
    }

    localStorage.setItem("jwtToken", result.token);

    document.getElementById("loginMessage").innerText = "";

    showMainSection();

    await loadDoctors();
    await loadPatients();
    await loadAppointments();
  } catch (error) {
    document.getElementById("loginMessage").innerText =
      "API connection failed.";
    console.error(error);
  }
}

// Show main screen after login
function showMainSection() {
  document.getElementById("loginSection").style.display = "none";
  document.getElementById("mainSection").style.display = "block";
}

// Logout
function logout() {
  localStorage.removeItem("jwtToken");

  document.getElementById("mainSection").style.display = "none";
  document.getElementById("loginSection").style.display = "block";

  document.getElementById("username").value = "";
  document.getElementById("password").value = "";
}

// Helper method to get auth headers
function getAuthHeaders() {
  const token = localStorage.getItem("jwtToken");

  return {
    "Content-Type": "application/json",
    Authorization: `Bearer ${token}`,
  };
}

// Load doctors into dropdown
async function loadDoctors() {
  try {
    const response = await fetch(`${apiBaseUrl}/Doctors`, {
      method: "GET",
      headers: getAuthHeaders(),
    });

    if (!response.ok) {
      showMessage("Unable to load doctors. Check role permission.");
      return;
    }

    const doctors = await response.json();

    const doctorDropdown = document.getElementById("doctorId");
    doctorDropdown.innerHTML = `<option value="">-- Select Doctor --</option>`;

    doctors.forEach((doctor) => {
      const option = document.createElement("option");

      option.value = doctor.doctorId;
      option.text = `${doctor.fullName} - ${doctor.specialization}`;

      doctorDropdown.appendChild(option);
    });
  } catch (error) {
    console.error(error);
    showMessage("Error loading doctors.");
  }
}

// Load patients into dropdown
async function loadPatients() {
  try {
    const response = await fetch(`${apiBaseUrl}/Patients`, {
      method: "GET",
      headers: getAuthHeaders(),
    });

    if (!response.ok) {
      showMessage("Unable to load patients. Check role permission.");
      return;
    }

    const patients = await response.json();

    const patientDropdown = document.getElementById("patientId");
    patientDropdown.innerHTML = `<option value="">-- Select Patient --</option>`;

    patients.forEach((patient) => {
      const option = document.createElement("option");

      option.value = patient.patientId;
      option.text = `${patient.fullName} - ${patient.gender}`;

      patientDropdown.appendChild(option);
    });
  } catch (error) {
    console.error(error);
    showMessage("Error loading patients.");
  }
}

// Load appointment list
async function loadAppointments() {
  try {
    const response = await fetch(`${apiBaseUrl}/Appointments`, {
      method: "GET",
      headers: getAuthHeaders(),
    });

    if (!response.ok) {
      showMessage("Unable to load appointments. Check API permission.");
      return;
    }

    const appointments = await response.json();

    const tableBody = document.getElementById("appointmentTableBody");
    tableBody.innerHTML = "";

    appointments.forEach((appointment) => {
      const row = document.createElement("tr");

      row.innerHTML = `
                <td>${appointment.appointmentId}</td>
                <td>${appointment.doctorName}</td>
                <td>${appointment.patientName}</td>
                <td>${appointment.appointmentDate}</td>
                <td>${appointment.appointmentTime}</td>
                <td>${appointment.appointmentStatus}</td>
                <td>${appointment.reason ?? ""}</td>
                <td>
                    <button class="warning" onclick="updateStatus(${appointment.appointmentId}, 'Completed')">
                        Complete
                    </button>

                    <button class="warning" onclick="updateStatus(${appointment.appointmentId}, 'Cancelled')">
                        Cancel
                    </button>

                    <button class="danger" onclick="deleteAppointment(${appointment.appointmentId})">
                        Delete
                    </button>
                </td>
            `;

      tableBody.appendChild(row);
    });
  } catch (error) {
    console.error(error);
    showMessage("Error loading appointments.");
  }
}

// Create appointment
async function createAppointment() {
  const doctorId = document.getElementById("doctorId").value;
  const patientId = document.getElementById("patientId").value;
  const appointmentDate = document.getElementById("appointmentDate").value;
  const appointmentTime = document.getElementById("appointmentTime").value;
  const reason = document.getElementById("reason").value;

  if (!doctorId || !patientId || !appointmentDate || !appointmentTime) {
    showMessage("Please fill doctor, patient, date and time.");
    return;
  }

  const appointmentData = {
    doctorId: parseInt(doctorId),
    patientId: parseInt(patientId),
    appointmentDate: appointmentDate,
    appointmentTime: appointmentTime,
    reason: reason,
  };

  try {
    const response = await fetch(`${apiBaseUrl}/Appointments`, {
      method: "POST",
      headers: getAuthHeaders(),
      body: JSON.stringify(appointmentData),
    });

    if (!response.ok) {
      showMessage("Unable to create appointment. Check API role permission.");
      return;
    }

    showMessage("Appointment created successfully.");

    clearForm();

    await loadAppointments();
  } catch (error) {
    console.error(error);
    showMessage("Error creating appointment.");
  }
}

// Update appointment status
async function updateStatus(appointmentId, status) {
  const statusData = {
    appointmentStatus: status,
  };

  try {
    const response = await fetch(
      `${apiBaseUrl}/Appointments/${appointmentId}/status`,
      {
        method: "PUT",
        headers: getAuthHeaders(),
        body: JSON.stringify(statusData),
      },
    );

    if (!response.ok) {
      showMessage(
        "Unable to update status. Check Admin/Doctor role permission.",
      );
      return;
    }

    showMessage(`Appointment status updated to ${status}.`);

    await loadAppointments();
  } catch (error) {
    console.error(error);
    showMessage("Error updating status.");
  }
}

// Delete appointment
async function deleteAppointment(appointmentId) {
  const confirmDelete = confirm(
    "Are you sure you want to delete this appointment?",
  );

  if (!confirmDelete) {
    return;
  }

  try {
    const response = await fetch(
      `${apiBaseUrl}/Appointments/${appointmentId}`,
      {
        method: "DELETE",
        headers: getAuthHeaders(),
      },
    );

    if (!response.ok) {
      showMessage("Unable to delete appointment. Only Admin role can delete.");
      return;
    }

    showMessage("Appointment deleted successfully.");

    await loadAppointments();
  } catch (error) {
    console.error(error);
    showMessage("Error deleting appointment.");
  }
}

// Clear form
function clearForm() {
  document.getElementById("doctorId").value = "";
  document.getElementById("patientId").value = "";
  document.getElementById("appointmentDate").value = "";
  document.getElementById("appointmentTime").value = "";
  document.getElementById("reason").value = "";
}

// Show message
function showMessage(message) {
  document.getElementById("message").innerText = message;

  setTimeout(() => {
    document.getElementById("message").innerText = "";
  }, 4000);
}
