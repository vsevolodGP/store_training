using System;
using System.Linq;

namespace Store.Memory
{
    public class BookRepository : IBookRepository
    {
        private readonly Book[] books = new[]
        {
            new Book(1, "ISBN 12345-78910", "D. Knuth", "Art of programming"),
            new Book(2, "ISBN 109876-54321", "R. Martin", "Clean code"),
            new Book(3, "ISBN 654321 - 10987", "B. Kernighan, D. Ritchie", "The C programming language")
        };

        public Book[] GetAllByIsbn(string isbn)
        {
            return books.Where(book => book.Isbn == isbn)
                        .ToArray();
        }

        public Book[] GetAllByTitleOrAuthor(string query)
        {
            return books.Where(book => book.Author.Contains(query) 
                                    || book.Title.Contains(query))
                                    .ToArray();
        }
    }
}
