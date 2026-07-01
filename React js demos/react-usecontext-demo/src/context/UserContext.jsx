import { createContext, useContext, useState } from "react";

export const UserContext = createContext();

export function UserProvider({ children }) {
  const [user, setUser] = useState(null);

  function loginUser() {
    const sampleUser = {
      name: "Geetha",
      role: "admin",
      email: "geetha.training@gmail.com",
    };
    setUser(sampleUser);
  }

  function logoutUser() {
    setUser(null);
  }

  return (
    <UserContext.Provider value={{ user, loginUser, logoutUser }}>
      {children}
    </UserContext.Provider>
  );
}
