using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store
{
    public interface IBookRepository
    {
        Task<Book[]> GetAllByIsbnAsync(string isbn);

        Task<Book[]> GetAllByTitleOrAuthorAsync(string titleorAuthor);

        Task<Book> GetByIdAsync(int id);

        Task<Book[]> GetAllByIdsAsync(IEnumerable<int> bookIds);
    }
}
