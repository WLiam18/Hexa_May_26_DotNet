import { Link, useNavigate, useParams } from "react-router-dom";
import { products } from "../data/product";

export function ProductDetails() {
  const { id } = useParams();
  const navigate = useNavigate();

  const product = products.find((item) => item.id === Number(id));

  if (!product) {
    return (
      <div className="page">
        <h2>Product Not Found</h2>
        <button onClick={() => navigate("/products")}>Back to Products</button>
      </div>
    );
  }

  return (
    <div className="page">
      <h2>{product.name}</h2>

      <p>Category: {product.category}</p>
      <p>Price: ₹{product.price}</p>
      <p>Rating: {product.rating}</p>
      <p>Stock: {product.stock}</p>
      <p>{product.description}</p>

      <Link to={`/products`} className="button-link">
        Back to Products
      </Link>
    </div>
  );
}
