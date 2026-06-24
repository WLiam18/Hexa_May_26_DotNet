// console.log("Hello Team, Welcome to Javascript ");
// alert("welcome to Smart Learning Academy");

const registerationForm = document.getElementById("registerForm");
const studentNameInput = document.getElementById("studentName");
const emailInput = document.getElementById("email");
const mobileInput = document.getElementById("mobile");
const courseSelect = document.getElementById("course");
const termsCheckbox = document.getElementById("terms");
const formMessage = document.getElementById("formMessage");
const studentOutput = document.getElementById("studentOutput");
const courseFee = document.getElementById("courseFee");
const themeBtn = document.getElementById("themeBtn");
registerationForm.addEventListener("submit", function (event) {
  event.preventDefault();

  const studentName = studentNameInput.value.trim();
  const email = emailInput.value.trim();
  const mobile = mobileInput.value.trim();
  const course = courseSelect.value.trim();
  const selectedMode = document.querySelector('input[name="mode"]:checked');
  console.log(studentName);
  console.log(`${email}  - ${mobile} -  ${course}`);

  if (studentName === "") {
    formMessage.textContent = "Please Enter your fullName.";
    formMessage.className = "error-message";
    retrun;
  }
  if (email === "") {
    formMessage.textContent = "Please Enter your Email.";
    formMessage.className = "error-message";
    retrun;
  }

  if (mobile === "") {
    formMessage.textContent = "Please Enter your mobile Number.";
    formMessage.className = "error-message";
    retrun;
  }
  if (course === "") {
    formMessage.textContent = "Please Select a course.";
    formMessage.className = "error-message";
    return;
  }
  if (!termsCheckbox.checked) {
    formMessage.textContent = "Please accept the terms and Conditions.";
    formMessage.className = "error-message";
    return;
  }

  if (!isValidEmail(email)) {
    formMessage.textContent = "Please enter a valid email address.";
    formMessage.className = "error-message";
    return;
  }
  if (!isValidMobile(mobile)) {
    formMessage.textContent = "Mobile number must contain exactly 10 digits.";
    formMessage.className = "error-message";
    return;
  }

  if (selectedMode === null) {
    formMessage.textContent = "Please select traning mode.";
    formMessage.className = "error-message";
    return;
  }
  formMessage.textContent = "Registration submitted successfully!";
  formMessage.className = "success-message";

  studentOutput.innerHTML = `
  <h3> Submitted Student Details </h3>
  <p><strong>Name: </strong> ${studentName} </p>
  <p><strong>Email: </strong> ${email} </p>
  <p><strong>Mobile: </strong> ${mobile} </p>
  <p><strong>Course: </strong> ${course} </p>
  <p><strong>Training Mode: </strong> ${selectedMode.value} </p>
  `;
});

function isValidEmail(email) {
  return email.includes("@") && email.includes(".");
}

function isValidMobile(mobile) {
  return mobile.length === 10 && !isNaN(mobile);
}

courseSelect.addEventListener("change", function () {
  const selectedCourse = courseSelect.value;

  if (selectedCourse === "HTML") {
    courseFee.textContent = "Course Fee: ₹999";
  } else if (selectedCourse === "CSS") {
    courseFee.textContent = "Course Fee: ₹1499";
  } else if (selectedCourse === "javascript") {
    courseFee.textContent = "Course Fee: ₹1999";
  } else {
    courseFee.textContent = "";
  }
});

function clearBtn() {
  formMessage.textContent = "";
  formMessage.className = "";
  studentOutput.innerHTML = "";
  courseFee.textContent = "";
}

function changeTehme() {
  document.body.classList.toggle("dark-mode");

  if (document.body.classList.contains("dark-mode")) {
    themeBtn.textContent = "Light  Mode";
  } else {
    themeBtn.textContent = "Dark Mode";
  }
}
