import { useCart } from "../context/CartContext";

const products = [
  {
    id: 1,
    name: "Laptop",
    price: 55000,
  },
  {
    id: 2,
    name: "Keyboard",
    price: 1200,
  },
  {
    id: 3,
    name: "Mouse",
    price: 700,
  },
  {
    id: 4,
    name: "Monitor",
    price: 9000,
  },
];

export function ProductList() {
  const { addItem } = useCart();

  return (
    <div className="card">
      <h2> Products</h2>
      <div className="product-grid">
        {products.map((product) => (
          <div className="product-card" key={product.id}>
            <h3> {product.name}</h3>
            <p>Price: {product.price}</p>
            <button onClick={() => addItem(product)}>Add to cart</button>
          </div>
        ))}
      </div>
    </div>
  );
}
