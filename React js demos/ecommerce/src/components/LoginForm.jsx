import { useState } from "react";

export function LoginForm({ onLogin }) {
  const [loginData, setLoginData] = useState({
    email: "",
    password: "",
  });

  const [errorMessage, setErrorMessage] = useState("");

  function handleInputChange(event) {
    const { name, value } = event.target;

    setLoginData({
      ...loginData,
      [name]: value,
    });
  }
  function handleSubmit(event) {
    event.preventDefault();

    if (loginData.email.trim() === "") {
      setErrorMessage("email is required.");
      return;
    }
    if (loginData.password.trim() === "") {
      setErrorMessage("password is required.");
      return;
    }
    onLogin(loginData);
  }

  return (
    <>
      <div className="login-page d-flex align-items-center justify-content-center p-3">
        <div
          className="card shadow-lg border-0"
          style={{ maxWidth: "430px", width: "100%" }}
        >
          <div className="card-body p-4">
            <h1 className="h3 text-primary fw-bold mb-2">E Commerce Login</h1>
            <p className="text-muted mb-4">
              Login to view the product dashboard
            </p>
            {errorMessage && (
              <div className="alert alert-danger">{errorMessage}</div>
            )}
            <form onSubmit={handleSubmit}>
              <div className="mb-3">
                <label htmlFor="email" className="form-label fw-semibold">
                  Email Address
                </label>
                <input
                  id="email"
                  name="email"
                  type="email"
                  className="form-control"
                  value={loginData.email}
                  onChange={handleInputChange}
                  placeholder="Enter Email"
                />
              </div>
              <div className="form-group">
                <label htmlFor="password" className="form-label fw-semibold">
                  Password
                </label>
                <input
                  id="password"
                  name="password"
                  type="password"
                  className="form-control"
                  value={loginData.password}
                  onChange={handleInputChange}
                  placeholder="Enter password"
                />
              </div>
              <button type="submit" className="btn btn-primary w-100">
                Login
              </button>
            </form>
          </div>
        </div>
      </div>
    </>
  );
}
