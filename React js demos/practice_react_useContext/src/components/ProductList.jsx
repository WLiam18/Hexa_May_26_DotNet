import { useContext } from "react";
import { CartContext } from "../context/CartContext";

export function ProductList() {
  const { products, addToCart } = useContext(CartContext);

  return (
    <div className="products">
      <h2>Products</h2>
      {products.map((p) => (
        <div key={p.id} className="product">
          <span>
            {p.name} - {p.price}
          </span>
          <button onClick={() => addToCart(p)}>Add</button>
        </div>
      ))}
    </div>
  );
}