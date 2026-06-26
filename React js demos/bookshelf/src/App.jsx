import { useState } from "react";
import reactLogo from "./assets/react.svg";
import viteLogo from "./assets/vite.svg";
import heroImg from "./assets/hero.png";
import "./App.css";
import Welcome from "./Welcome";
import { ProductList } from "./components/ProductList.jsx";
import { products } from "./data/products.js";
import { ProductCard } from "./components/ProductCard.jsx";
import { SearchBox } from "./components/SearchBox.jsx";
import { CategoryFilter } from "./components/CategoryFilter.jsx";
import { SortDropdown } from "./components/SortDropdown.jsx";
function App() {
  const [searchText, setSearchText] = useState("");
  const [selectedCategory, setSelectedCategory] = useState("All");
  const [sortBy, setSortBy] = useState("");
  const filteredProducts = products.filter((product) => {
    const matchesSearch = product.name
      .toLowerCase()
      .includes(searchText.toLowerCase());
    const matchesCategory =
      selectedCategory === "All" || product.category === selectedCategory;
    return matchesCategory && matchesSearch;
  });

  const sortedProducts = [...filteredProducts].sort((a, b) => {
    if (sortBy === "priceLowHigh") {
      return a.price - b.price;
    }

    if (sortBy === "priceHighLow") {
      return b.price - a.price;
    }
    if (sortBy === "ratingHighLow") {
      return b.rating - a.rating;
    }

    if (sortBy === "stockHighLow") {
      return b.stock - a.stock;
    }
    return 0;
  });

  return (
    <div className="app">
      <h1> Product Gallery</h1>
      <SearchBox searchText={searchText} onSearchChange={setSearchText} />
      <CategoryFilter
        selectedCategory={selectedCategory}
        onCategoryChange={setSelectedCategory}
      />

      <SortDropdown soryBy={sortBy} onSortChange={setSortBy} />
      <ProductList products={sortedProducts} />
    </div>
  );
}

export default App;
