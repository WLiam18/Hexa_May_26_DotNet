import { CategoryFilter } from "./CategoryFilter";
import { ProductList } from "./ProductList";
import { SearchBox } from "./SearchBox";
import { SortDropdown } from "./SortDropdown";

export function Dashboard({
  searchText,
  selectedCategory,
  sortBy,
  onSearchChange,
  onCategoryChange,
  onSortChange,
  products,
}) {
  return (
    <main className="dashboard">
      <section className="dashboard-title">
        <h2> Products Dashboard</h2>
        <p>search,filter and sort e- commerce products</p>
      </section>
      <section className="toolbar">
        <SearchBox searchText={searchText} onSearchChange={onSearchChange} />

        <CategoryFilter
          selectedCategory={selectedCategory}
          onCategoryChange={onCategoryChange}
        />

        <SortDropdown sortBy={sortBy} onSortChange={onSortChange} />
      </section>
      <ProductList products={products} />
    </main>
  );
}
