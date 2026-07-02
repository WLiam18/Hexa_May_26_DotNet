import ProductCard from "./ProductCard";

function ProductList({ products, user, onDeleteProduct, isAdmin }) {
  if (products.length === 0) {
    return (
      <div className="text-center py-5">
        <i className="bi bi-box-seam fs-1 text-muted d-block mb-3"></i>
        <h5 className="text-muted">No products found</h5>
        <p className="text-muted small">Try adjusting your search or filter</p>
      </div>
    );
  }

  return (
    <div className="row row-cols-1 row-cols-md-2 row-cols-lg-3 g-4">
      {products.map((p) => (
        <div className="col" key={p.id}>
          <ProductCard
            product={p}
            user={user}
            onDeleteProduct={onDeleteProduct}
            isAdmin={isAdmin}
          />
        </div>
      ))}
    </div>
  );
}

export default ProductList;