import { useState } from "react";
import Login from "./components/Login";
import Dashboard from "./components/Dashboard";
import { users, initialProducts } from "./data";
import './App.css';

function App() {
  const [user, setUser] = useState(null);
  const [products, setProducts] = useState(initialProducts);

  const handleLogin = (email, password) => {
    const found = users.find(u => u.email === email && u.password === password);
    if (found) setUser(found);
    else alert("Invalid credentials");
  };

  const handleLogout = () => setUser(null);

  const addProduct = (newProduct) => {
    setProducts([...products, { ...newProduct, id: Date.now() }]);
  };

  const deleteProduct = (id) => {
    setProducts(products.filter(p => p.id !== id));
  };

  if (!user) return <Login onLogin={handleLogin} />;

  return (
    <Dashboard
      user={user}
      onLogout={handleLogout}
      products={products}
      onAddProduct={addProduct}
      onDeleteProduct={deleteProduct}
    />
  );
}

export default App;
