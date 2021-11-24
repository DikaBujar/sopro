using Entity;
using Services.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IBookmarkService
    {
        Bookmark CreateBookmark(BookmarkDTO bookmark);
        List<BookmarkDTO> GetBookmarks();
        BookmarkDTO GetBookmarkById(int Id);
        void UpdateBookmark(BookmarkDTO bookmark);
        void DeleteBookmark(BookmarkDTO bookmark);
    }
}