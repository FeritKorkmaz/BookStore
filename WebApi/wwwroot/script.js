document.addEventListener("DOMContentLoaded", function () {
  const List = document.getElementById("List");

  // Fetch and display books
  showList.addEventListener(
    "click",
    function (event) {
      event.preventDefault();
      fetch("https://localhost:7019/books")
        .then((response) => response.json())
        .then((data) => {
          console.log(data);
          data.forEach((book) => {
            const listItem = document.createElement("li");
            listItem.className = "list-group-item text-dark fs-5";

            listItem.textContent = `ID: ${book.id} -- Title: ${book.title} -- PageCount: ${book.pageCount} -- PublishDate: ${book.publishDate} -- Genre: ${book.genre} -- Author: ${book.author}`;
            List.appendChild(listItem);
          });
        });
    },
    { once: true }
  );

  // Bring and display the Book by ID
  const bookList = document.getElementById("book");

  showBook.addEventListener(
    "click",
    function (event) {
      event.preventDefault();
      const bookId = document.getElementById("bookId");
      const bookIdValue = bookId.value;
      fetch(`https://localhost:7019/books/${bookIdValue}`)
        .then((response) => response.json())
        .then((book) => {
          console.log(book);
          const listItem = document.createElement("li");
          listItem.className = "list-group-item text-dark fs-5";
          listItem.textContent = `Title: ${book.title} -- PageCount: ${book.pageCount} -- PublishDate: ${book.publishDate} -- Genre: ${book.genre} -- Author: ${book.author}`;
          bookList.appendChild(listItem);
        });
    },
    { once: true }
  );

  // Add new book on form submit
  const form = document.getElementById("authorForm");
  form.addEventListener("submit", function (event) {
    event.preventDefault();
    const title = document.getElementById("title");
    const pageCount = document.getElementById("pageCount");
    const publishDate = document.getElementById("publishDate");
    const genreId = document.getElementById("genre");
    const authorId = document.getElementById("author");

    const newBook = {
      title: title.value,
      pageCount: pageCount.value,
      publishDate: publishDate.value,
      genreId: genreId.value,
      authorId: authorId.value,
    };

    fetch("https://localhost:7019/books", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(newBook),
    });
  });

  // Delete book on form submit
  const deleteForm = document.getElementById("deleteForm");
  deleteForm.addEventListener("submit", function (event) {
    event.preventDefault();
    const bookId = document.getElementById("deleteBookId");
    const bookIdValue = bookId.value;
    fetch(`https://localhost:7019/books/${bookIdValue}`, {
      method: "DELETE",
    });
  });

  // update book on form submit
  const updateForm = document.getElementById("updateForm");
  updateForm.addEventListener("submit", function (event) {
    event.preventDefault();
    const bookId = document.getElementById("UpdateBookId");
    const bookIdValue = bookId.value;
    const title = document.getElementById("UpdateTitle");
    const genreId = document.getElementById("UpdateGenre");
    const authorId = document.getElementById("UpdateAuthor");

    const updateBook = {
      title: title.value,
      genreId: genreId.value,
      authorId: authorId.value,
    };

    fetch(`https://localhost:7019/books/${bookIdValue}`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(updateBook),
    });
  });
});
