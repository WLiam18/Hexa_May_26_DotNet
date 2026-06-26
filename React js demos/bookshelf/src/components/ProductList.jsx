import React from "react";
import { ProductCard } from "./ProductCard";

export function ProductList({ products }) {
  if (products.length === 0) {
    return <p className="empty-message">No Products found.</p>;
  }
  return (
    <div className="product-grid">
      {products.map((product) => (
        <ProductCard key={product.id} product={product} />
      ))}
    </div>
  );
}
