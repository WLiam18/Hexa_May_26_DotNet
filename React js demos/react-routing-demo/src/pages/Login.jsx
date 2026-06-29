import { useState } from "react";
import { useNavigate } from "react-router-dom";

export function Login() {
  const [username, setUsername] = useState("");
  const [role, setRole] = useState("User");
  const [error, setError] = useState("");

  const navigate = useNavigate();

  function handleLogin(event) {
    event.preventDefault();
    if (username.trim() === "") {
      setError("Username is required");
      return;
    }

    const user = {
      username: username,
      role: role,
      isAuthenticated: true,
    };
    localStorage.setItem("user", JSON.stringify(user));

    navigate("/dashboard");
  }
  return (
    <div className="page">
      <h2> Login Page</h2>

      <form className="form" onSubmit={handleLogin}>
        <div>
          <label>Username</label>
          <input
            type="text"
            value={username}
            placeholder="Enter username"
            onChange={(event) => setUsername(event.target.value)}
          />
        </div>
        <div>
          <label>Role</label>
          <select
            value={role}
            onChange={(event) => setRole(event.target.value)}
          >
            <option value="User">User</option>
            <option value="Admin">Admin</option>
          </select>
        </div>
        {error && <p className="error">{error}</p>}

        <button type="submit">Login</button>
      </form>
    </div>
  );
}
