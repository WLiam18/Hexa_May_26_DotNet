import { useState } from "react";

function ProductForm({ onAddProduct, user }) {
  const [name, setName] = useState("");
  const [price, setPrice] = useState("");
  const [category, setCategory] = useState("Electronics");
  const [stock, setStock] = useState("");

  const handleSubmit = (e) => {
    e.preventDefault();
    if (!name || !price || !stock) {
      alert("Fill all fields");
      return;
    }
    onAddProduct({
      name,
      price: Number(price),
      category,
      stock: Number(stock),
      rating: 0,
      seller: user.name,
    });
    setName("");
    setPrice("");
    setStock("");
  };

  return (
    <div>
      <h4>Add Product</h4>
      <form onSubmit={handleSubmit}>
        <input
          type="text"
          placeholder="Name"
          value={name}
          onChange={(e) => setName(e.target.value)}
        />
        <input
          type="number"
          placeholder="Price"
          value={price}
          onChange={(e) => setPrice(e.target.value)}
        />
        <select value={category} onChange={(e) => setCategory(e.target.value)}>
          <option value="Electronics">Electronics</option>
          <option value="Fashion">Fashion</option>
        </select>
        <input
          type="number"
          placeholder="Stock"
          value={stock}
          onChange={(e) => setStock(e.target.value)}
        />
        <button type="submit">Add</button>
      </form>
    </div>
  );
}

export default ProductForm;