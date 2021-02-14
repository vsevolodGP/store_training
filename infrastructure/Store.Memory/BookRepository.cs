using System.Collections.Generic;
using System.Linq;

namespace Store.Memory
{
    public class BookRepository : IBookRepository
    {
        private readonly Book[] books = new[]
        {
            new Book(1, "ISBN 12345-78910", "D. Knuth", "Art of programming", "This volume begins basic programming concepts and ...", 
                    7.19m),
            new Book(2, "ISBN 109876-54321", "M. Fowler", "Refactoring", "As the application of object technology...", 
                    12.43m),
            new Book(3, "ISBN 654321 - 10987", "B. Kernighan, D. Ritchie", "The C programming language", "known as the bible of C, this classic bestseller", 
                    14.98m)
        };

        public Book[] GetAllByIds(IEnumerable<int> bookIds)
        {
            var foundBooks = from book in books
                             join bookId in bookIds on book.Id equals bookId
                             select book;

            return foundBooks.ToArray();
        }

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

        public Book GetById(int id)
        {
            return books.Single(books => books.Id == id);
        }
    }
}
