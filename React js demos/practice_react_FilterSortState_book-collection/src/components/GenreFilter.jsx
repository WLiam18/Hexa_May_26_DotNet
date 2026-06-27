export function GenreFilter({ genre, onGenreChange }) {
  const genres = ["All", "Thriller", "Self-Help", "Sci-Fi", "Classic", "Memoir", "Fantasy"];

  return (
    <select
      value={genre}
      onChange={(e) => onGenreChange(e.target.value)}
      className="filter-select"
    >
      {genres.map((g) => (
        <option key={g} value={g}>
          {g}
        </option>
      ))}
    </select>
  );
}