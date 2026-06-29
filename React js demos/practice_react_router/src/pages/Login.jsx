import { useState } from "react";
import { useNavigate } from "react-router-dom";

function Login() {
  const [username, setUsername] = useState("");
  const navigate = useNavigate();

  const handleSubmit = (e) => {
    e.preventDefault();
    if (username.trim() === "") {
      alert("Username is required");
      return;
    }

    localStorage.setItem("user", JSON.stringify({ username, isAuthenticated: true }));
    navigate("/dashboard");
  };

  return (
    <div className="page">
      <h2>Login</h2>
      <form onSubmit={handleSubmit}>
        <input
          type="text"
          placeholder="Enter username"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
        />
        <button type="submit">Login</button>
      </form>
      
    </div>
  );
}

export default Login;