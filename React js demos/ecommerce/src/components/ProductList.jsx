import React from "react";
import { ProductCard } from "./ProductCard";

export function ProductList({
  products = [],
  loggedInUser,
  onUpdateProduct,
  onRemoveLowRatedProduct,
  isAdmin,
  isSeller,
}) {
  if (products.length === 0) {
    return <div className="alert alert-warning">No Products found.</div>;
  }
  return (
    <div className="row g-4">
      {products.map((product) => (
        <ProductCard
          key={product.id}
          product={product}
          loggedInUser={loggedInUser}
          onUpdateProduct={onUpdateProduct}
          onRemoveLowRatedProduct={onRemoveLowRatedProduct}
          isAdmin={isAdmin}
          isSeller={isSeller}
        />
      ))}
    </div>
  );
}
