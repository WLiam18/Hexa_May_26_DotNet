import { UserContext } from "../context/UserContext";
import { useContext } from "react";
import { ThemeContext } from "../context/ThemeContext";
export function LoginPanel() {
  const { user, loginUser, logoutUser } = useContext(UserContext);
  const { theme, toggleTheme } = useContext(ThemeContext);
  return (
    <div className={`card ${theme}`}>
      <h2> Login Panel</h2>

      {user ? (
        <button onClick={logoutUser}>Logout</button>
      ) : (
        <button onClick={loginUser}>Login</button>
      )}
    </div>
  );
}
