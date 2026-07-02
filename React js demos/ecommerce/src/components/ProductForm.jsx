import { useState } from "react";

const emptyProduct = {
  name: "",
  price: "",
  category: "",
  stock: "",
  rating: "",
  seller: "",
  image: "",
  description: "",
};

export function ProductForm({
  mode = "add",
  productToEdit = null,
  onSubmitProduct,
  onCancel,
  loggedInUser,
}) {
  const [productData, setProductData] = useState(
    productToEdit || {
      ...emptyProduct,
      seller: loggedInUser?.name || "",
      image: "https://picsum.photos/seed/new-product/600/400",
    },
  );

  const [errorMessage, seterrorMessage] = useState("");
  const [successMessage, setSuccessMessage] = useState("");

  function handleInputChange(event) {
    const { name, value } = event.target;

    setProductData({
      ...productData,
      [name]: value,
    });
  }
  function validateProduct() {
    if (productData.name.trim() === "") {
      return "product name is required.";
    }

    if (productData.description.trim() === "") {
      return "product description is required.";
    }
    if (productData.category.trim() === "") {
      return "category  is required.";
    }
    if (productData.price === "" || Number(productData.price) <= 0) {
      return "Price must be greater than 0.";
    }
    if (productData.stock === "" || Number(productData.stock) < 0) {
      return "Stock cannot be negative.";
    }

    if (
      productData.rating === "" ||
      Number(productData.rating) < 1 ||
      Number(productData.rating) > 5
    ) {
      return "Rating must be between 1-5.";
    }
    if (productData.seller.trim() === "") {
      return "seller name is required.";
    }
    if (productData.image.trim() === "") {
      return "Image URL  is required.";
    }
    return "";
  }

  function handleSubmit(event) {
    event.preventDefault();

    const validationError = validateProduct();
    if (validationError) {
      seterrorMessage(validationError);
      setSuccessMessage("");
      return;
    }
    const finalProduct = {
      ...productData,
      price: Number(productData.price),
      stock: Number(productData.stock),
      rating: Number(productData.rating),
    };
    onSubmitProduct(finalProduct);
    seterrorMessage("");
    setSuccessMessage(
      mode === "add"
        ? "Product Added Successfully."
        : "Product Updated Successfully.",
    );

    if (mode === "add") {
      setProductData({
        ...emptyProduct,
        seller: loggedInUser.name || "",
        image: "https://picsum.photos/seed/new-product/600/400",
      });
    }

    if (mode === "edit" && onCancel) {
      onCancel();
    }
  }

  return (
    <>
      <section className="card border-0 shadow-sm mb-4">
        <div className="card-body">
          <h2 className="h4 text-warning fw-bold mb-3">
            {mode === "add" ? "Add New Product" : "Update Product"}
          </h2>

          {errorMessage && (
            <div className="alert alert-danger">{errorMessage}</div>
          )}
          {successMessage && (
            <div className="alert alert-success">{successMessage}</div>
          )}
          <form onSubmit={handleSubmit}>
            <div className="row g-3">
              <div className="col-md-6">
                <label htmlFor={`${mode}-name`} className="form-label">
                  Product Name
                </label>
                <input
                  type="text"
                  id={`${mode}-name`}
                  name="name"
                  className="form-control"
                  value={productData.name}
                  onChange={handleInputChange}
                  placeholder="Enter  Product name"
                />
              </div>
              <div className="col-md-6">
                <label htmlFor={`${mode}-category`} className="form-label">
                  Category
                </label>
                <select
                  id={`${mode}-category`}
                  name="category"
                  className="form-select"
                  value={productData.category}
                  onChange={handleInputChange}
                >
                  <option value="Electronics">Electronics</option>
                  <option value="Home">Home</option>
                  <option value="Fashion">Fashion</option>
                  <option value="Kitchen">Kitchen</option>
                </select>
              </div>

              <div className="col-md-4">
                <label htmlFor={`${mode}-price`} className="form-label">
                  Price
                </label>
                <input
                  id={`${mode}-price`}
                  name="price"
                  type="number"
                  className="form-control"
                  value={productData.price}
                  onChange={handleInputChange}
                  placeholder="Enter price"
                />
              </div>
              <div className="col-md-4">
                <label htmlFor={`${mode}-stock`} className="form-label">
                  Stock
                </label>
                <input
                  id={`${mode}-stock`}
                  name="stock"
                  type="number"
                  className="form-control"
                  value={productData.stock}
                  onChange={handleInputChange}
                  placeholder="Enter stock"
                />
              </div>
              <div className="col-md-4">
                <label htmlFor={`${mode}-rating`} className="form-label">
                  Rating
                </label>
                <input
                  id={`${mode}-rating`}
                  name="rating"
                  type="number"
                  step="0.1"
                  min="1"
                  max="5"
                  className="form-control"
                  value={productData.rating}
                  onChange={handleInputChange}
                  placeholder="1 to 5"
                />
              </div>
              <div className="col-md-6">
                <label htmlFor={`${mode}-seller`} className="form-label">
                  Seller
                </label>
                <input
                  id={`${mode}-seller`}
                  name="seller"
                  type="text"
                  className="form-control"
                  value={productData.seller}
                  onChange={handleInputChange}
                  placeholder="Enter seller name"
                />
              </div>
              <div className="col-12">
                <label htmlFor={`${mode}-description`} className="form-label">
                  Description
                </label>
                <input
                  id={`${mode}-description`}
                  name="description"
                  type="text"
                  className="form-control"
                  value={productData.description}
                  onChange={handleInputChange}
                  placeholder="Enter product description"
                />
              </div>
              <div className="col-12 d-flex gap-2">
                <button type="submit" className="btn btn-warning">
                  {mode === "add" ? "Add Product" : "Save changes"}
                </button>
                {mode === "edit" && (
                  <button
                    type="button"
                    className="btn btn-outlint-secondary"
                    onClick={onCancel}
                  >
                    Cancel
                  </button>
                )}
              </div>
            </div>
          </form>
        </div>
      </section>
    </>
  );
}
