function GenreFilter({ genre, onGenreChange }) {
  const genres = ["All", "CS", "Fantasy", "Self-Help", "Classic"];

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

export default GenreFilter;