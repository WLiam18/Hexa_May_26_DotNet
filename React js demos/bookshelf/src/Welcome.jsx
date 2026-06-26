import React from "react";

function Welcome({ name, books }) {
  console.log(books);
  return (
    <>
      <h1> Hello {name}</h1>
      {books !== undefined ? (
        <>
          <p>you own {books.length} books</p>
          <p> {books.length > 0 ? "Nice Library" : "Add your First Book."}</p>
        </>
      ) : (
        <p>No books available</p>
      )}
    </>
  );
}

export default Welcome;
