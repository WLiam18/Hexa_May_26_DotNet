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
    <div className="container py-4">
      {/* Header */}
      <div className="d-flex justify-content-between align-items-center border-bottom pb-3 mb-3">
        <div>
          <h2 className="mb-0">Products</h2>
          <p className="text-muted mb-0">
            Welcome, <strong>{user.name}</strong> ({user.role})
          </p>
        </div>
        <button className="btn btn-danger" onClick={onLogout}>Logout</button>
      </div>

      {isAdmin && (
        <div className="alert alert-warning">
          🔧 Admin: You can delete low-rated products.
        </div>
      )}


      {isSeller && <ProductForm onAddProduct={onAddProduct} user={user} />}


      <div className="row g-2 mb-3">
        <div className="col-md-8">
          <input
            type="text"
            className="form-control"
            placeholder="Search..."
            value={search}
            onChange={(e) => setSearch(e.target.value)}
          />
        </div>
        <div className="col-md-4">
          <select
            className="form-select"
            value={filter}
            onChange={(e) => setFilter(e.target.value)}
          >
            <option value="All">All</option>
            <option value="Electronics">Electronics</option>
            <option value="Fashion">Fashion</option>
          </select>
        </div>
      </div>

      <ProductList
        products={filtered}
        user={user}
        onDeleteProduct={onDeleteProduct}
        isAdmin={isAdmin}
      />
    </div>
  );
}

export default Dashboard;