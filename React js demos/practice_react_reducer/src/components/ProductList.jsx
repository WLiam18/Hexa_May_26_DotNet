import { useCart } from "../context/CartContext";

const products = [
  { id: 1, name: "Laptop", price: 55000 },
  { id: 2, name: "Keyboard", price: 1200 },
  { id: 3, name: "Mouse", price: 700 },
  { id: 4, name: "Monitor", price: 9000 },
];

export function ProductList() {
  const { addItem } = useCart();

  return (
    <div className="products">
      <h2>Products</h2>
      {products.map((p) => (
        <div key={p.id} className="product">
          <span>
            {p.name} - {p.price}
          </span>
          <button onClick={() => addItem(p)}>Add</button>
        </div>
      ))}
    </div>
  );
}