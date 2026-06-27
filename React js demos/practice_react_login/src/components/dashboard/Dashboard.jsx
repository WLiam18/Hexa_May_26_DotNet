import { useState } from "react";
import { books } from "../../data/books";
import BookList from "../books/BookList";
import SearchBox from "../books/SearchBox";
import GenreFilter from "../books/GenreFilter";
import SortDropdown from "../books/SortDropdown";

function Dashboard({ user, onLogout }) {
  const [search, setSearch] = useState("");
  const [genre, setGenre] = useState("All");
  const [sortBy, setSortBy] = useState("");

  const filteredBooks = books.filter((book) => {
    const matchesSearch =
      book.title.toLowerCase().includes(search.toLowerCase()) ||
      book.author.toLowerCase().includes(search.toLowerCase());

    const matchesGenre = genre === "All" || book.genre === genre;

    return matchesSearch && matchesGenre;
  });

  const sortedBooks = [...filteredBooks].sort((a, b) => {
    if (sortBy === "yearNewest") return b.year - a.year;
    if (sortBy === "yearOldest") return a.year - b.year;
    if (sortBy === "ratingHigh") return b.rating - a.rating;
    return 0;
  });

  return (
    <div className="dashboard">
      <div className="header">
        <div>
          <h2>My Book Collection</h2>
          <p className="user-info">
            Logged in as <strong>{user.username}</strong> ({user.role})
          </p>
        </div>
        <button onClick={onLogout}>Logout</button>
      </div>

      {user.role === "admin" && (
        <div className="admin-banner">
          Admin Panel — You can manage books and users.
        </div>
      )}

      <div className="controls">
        <SearchBox search={search} onSearchChange={setSearch} />
        <GenreFilter genre={genre} onGenreChange={setGenre} />
        <SortDropdown sortBy={sortBy} onSortChange={setSortBy} />
      </div>

      <p className="result-count">
        Showing {sortedBooks.length} of {filteredBooks.length} books
      </p>

      <BookList books={sortedBooks} />
    </div>
  );
}

export default Dashboard;