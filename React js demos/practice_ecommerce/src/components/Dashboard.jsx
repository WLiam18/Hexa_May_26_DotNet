import { useState } from "react";
import ProductList from "./ProductList";
import ProductForm from "./ProductForm";

function Dashboard({ user, onLogout, products, onAddProduct, onDeleteProduct }) {
  const [search, setSearch] = useState("");
  const [filter, setFilter] = useState("All");

  const filtered = products.filter(p => {
    const matchSearch = p.name.toLowerCase().includes(search.toLowerCase());
    const matchFilter = filter === "All" || p.category === filter;
    return matchSearch && matchFilter;
  });

  const isSeller = user.role === "Seller";
  const isAdmin = user.role === "Admin";

  return (
    <div className="dashboard-container">
      <div className="dashboard-header">
        <div>
          <h2>Products</h2>
          <p>Welcome, {user.name} ({user.role})</p>
        </div>
        <button className="logout-btn" onClick={onLogout}>Logout</button>
      </div>

      {isAdmin && (
        <div className="admin-banner">
          🔧 Admin: You can delete low-rated products.
        </div>
      )}

      {isSeller && <ProductForm onAddProduct={onAddProduct} user={user} />}

      <div className="controls">
        <input
          type="text"
          placeholder="Search..."
          value={search}
          onChange={(e) => setSearch(e.target.value)}
          className="search-input"
        />
        <select value={filter} onChange={(e) => setFilter(e.target.value)} className="filter-select">
          <option value="All">All</option>
          <option value="Electronics">Electronics</option>
          <option value="Fashion">Fashion</option>
        </select>
      </div>

      <ProductList products={filtered} user={user} onDeleteProduct={onDeleteProduct} isAdmin={isAdmin} />
    </div>
  );
}

export default Dashboard;