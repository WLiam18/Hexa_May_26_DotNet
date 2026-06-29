import ProductCard from "./ProductCard";

function ProductList({ products, user, onDeleteProduct, isAdmin }) {
  if (products.length === 0) return <p>No products found.</p>;

  return (
    <div className="product-grid">
      {products.map((p) => (
        <ProductCard
          key={p.id}
          product={p}
          user={user}
          onDeleteProduct={onDeleteProduct}
          isAdmin={isAdmin}
        />
      ))}
    </div>
  );
}

export default ProductList;