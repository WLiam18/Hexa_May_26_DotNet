import { Link, useSearchParams } from "react-router-dom";
import { products } from "../data/product";

export function Products() {
  const [searchParams, setSearchParams] = useSearchParams();

  const selectedCategory = searchParams.get("category") || "All";

  const filteredProducts =
    selectedCategory === "All"
      ? products
      : products.filter((product) => product.category === selectedCategory);

  function handleCategoryChange(event) {
    const category = event.target.value;

    if (category === "All") {
      setSearchParams({});
    } else {
      setSearchParams({ category: category });
    }
  }

  return (
    <div className="page">
      <h2>Products Page</h2>

      <div className="filter-box">
        <label>Filter by Category: </label>

        <select value={selectedCategory} onChange={handleCategoryChange}>
          <option value="All">All</option>
          <option value="Electronics">Electronics</option>
          <option value="Accessories">Accessories</option>
          <option value="Furniture">Furniture</option>
        </select>
      </div>

      <div className="product-grid">
        {filteredProducts.map((product) => (
          <div className="product-card" key={product.id}>
            <h3>{product.name}</h3>
            <p>Category: {product.category}</p>
            <p>Price: ₹{product.price}</p>
            <p>Rating: {product.rating}</p>

            <Link to={`/products/${product.id}`} className="button-link">
              View Details
            </Link>
          </div>
        ))}
      </div>
    </div>
  );
}
