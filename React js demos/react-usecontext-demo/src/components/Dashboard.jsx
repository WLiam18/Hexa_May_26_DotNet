import { UserContext } from "../context/UserContext";
import { useContext } from "react";

export function Dashboard() {
  const { user } = useContext(UserContext);
  {
    user ? (
      <>
        <p>
          <strong>Name:</strong> {user.name}
        </p>
        <p>
          <strong>Role:</strong> {user.role}
        </p>
        <p>
          <strong>Email:</strong> {user.email}
        </p>
      </>
    ) : (
      <p> No dashboard data available.please login.</p>
    );
  }
}
