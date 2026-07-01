import { useContext } from "react";
import { UserContext } from "../context/UserContext";

export function BasicProfile() {
  const user = useContext(UserContext);

  return (
    <div className="card">
      <h2>User Profile </h2>
      <p>
        <strong>Name:</strong> {user.name}
      </p>
      <p>
        <strong>Role:</strong> {user.role}
      </p>
      <p>
        <strong>Email:</strong> {user.email}
      </p>
    </div>
  );
}
