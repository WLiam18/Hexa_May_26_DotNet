import { CategoryFilter } from "./CategoryFilter";
import { ProductList } from "./ProductList";
import { SearchBox } from "./SearchBox";
import { SortDropdown } from "./SortDropdown";
import { ProductForm } from "./ProductForm";

export function Dashboard({
  loggedInUser,
  searchText,
  selectedCategory,
  sortBy,
  onSearchChange,
  onCategoryChange,
  onSortChange,
  products,
  onAddProduct,
  onUpdateProduct,
  onRemoveLowRatedProduct,
}) {
  const isSeller = loggedInUser.role === "Seller";
  const isAdmin = loggedInUser.role === "Admin";
  const isCustomer = loggedInUser?.role === "customer";
  let dashboardClass = "conatiner py-4";
  if (isSeller) {
    dashboardClass += " seller-dashboard-bg";
  }

  if (isAdmin) {
    dashboardClass += " admin-dashboard-bg";
  }
  if (isCustomer) {
    dashboardClass += " customer-dashboard-bg";
  }
  return (
    <main className={dashboardClass}>
      <section className="card border-0 shadow-sm mb-4">
        <div className="card-body">
          <h1 className="h3 text-primary fw-bold">
            {isSeller
              ? "Seller product Management Dashboard"
              : "Products Dashboard"}
          </h1>
          <p className="text-muted mb-0">
            {isSeller
              ? "Add, update and manage your Product catalog"
              : "search,filter and sort e- commerce product"}
          </p>
        </div>
      </section>
      {isSeller && (
        <ProductForm
          mode="add"
          onSubmitProduct={onAddProduct}
          loggedInUser={loggedInUser}
        />
      )}

      <section className="card border-0 shadow-sm mb-4">
        <div className="card-body">
          <div className="row g-3">
            <div className="col-md-5">
              <SearchBox
                searchText={searchText}
                onSearchChange={onSearchChange}
              />
            </div>
            <div className=" col-md-3">
              <CategoryFilter
                selectedCategory={selectedCategory}
                onCategoryChange={onCategoryChange}
              />
            </div>
            <div className="col-md-4">
              <SortDropdown sortBy={sortBy} onSortChange={onSortChange} />
            </div>
          </div>
        </div>
      </section>
      <ProductList
        products={products}
        loggedInUser={loggedInUser}
        onUpdateProduct={onUpdateProduct}
        onRemoveLowRatedProduct={onRemoveLowRatedProduct}
        isAdmin={isAdmin}
        isSeller={isSeller}
      />
    </main>
  );
}
