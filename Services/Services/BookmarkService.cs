using Data;
using Entity;
using Services.DTO;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class BookmarkService : IBookmarkService
    {
        private ReadLaterDataContext _ReadLaterDataContext;
        private ICategoryService _iCategoryService;

        public BookmarkService(ReadLaterDataContext readLaterDataContext, ICategoryService iCategoryService)
        {
            _ReadLaterDataContext = readLaterDataContext;
            _iCategoryService = iCategoryService;
        }

        public Bookmark CreateBookmark(BookmarkDTO bookmark)
        {
            var entityBookmark = new Bookmark();
            var bookmarkId = _iCategoryService.GetCategory(bookmark.category.Name);

            if (bookmarkId != null)
            {
                entityBookmark.CategoryId = bookmarkId.id;
            }

            if (bookmark.url != null)
            {
                entityBookmark.URL = bookmark.url;
            }

            if (bookmark.shortdescription != null)
            {
                entityBookmark.ShortDescription = bookmark.shortdescription;
            }

            entityBookmark.UserID = bookmark.userID;
            entityBookmark.CreateDate = DateTime.UtcNow;

            if (entityBookmark.URL != null)
            {
                _ReadLaterDataContext.Add(entityBookmark);
                _ReadLaterDataContext.SaveChanges();
            }

            return entityBookmark;
        }

        public void UpdateBookmark(BookmarkDTO bookmark)
        {
            var bookmarks = _ReadLaterDataContext.Bookmark.Where(c => c.ID == bookmark.id).FirstOrDefault();

            if (bookmarks.URL != null)
            {
                bookmarks.UserID = bookmark.userID;
                //bookmarks.CategoryId = bookmark.category.ID;
                bookmarks.ID = (int)bookmark.id;
                bookmarks.CreateDate = bookmark.createdate;
                bookmarks.ShortDescription = bookmark.shortdescription;
                bookmarks.URL = bookmark.url;

                _ReadLaterDataContext.Update(bookmarks);
                _ReadLaterDataContext.SaveChanges();
            }
        }

        public void DeleteBookmark(BookmarkDTO bookmark)
        {
            var removedBookmark = _ReadLaterDataContext.Bookmark.Where(c => c.ID == bookmark.id).FirstOrDefault();

            if (removedBookmark != null)
            {
                _ReadLaterDataContext.Bookmark.Remove(removedBookmark);
                _ReadLaterDataContext.SaveChanges();
            }
        }

        public List<BookmarkDTO> GetBookmarks()
        {
            var bookmarks = _ReadLaterDataContext.Bookmark.ToList();

            var listOfBookmarks = new List<BookmarkDTO>();
            if (bookmarks.Any())
            {
                foreach (var item in bookmarks)
                {
                    var bookmardto = new BookmarkDTO();
                    bookmardto.id = item.ID;
                    bookmardto.shortdescription = item.ShortDescription;
                    bookmardto.url = item.URL;
                    bookmardto.createdate = item.CreateDate;
                    bookmardto.userID = item.UserID;
                    bookmardto.category = _ReadLaterDataContext.Categories.Where(c => c.ID == item.CategoryId).FirstOrDefault();
                    listOfBookmarks.Add(bookmardto);
                }
            }

            return listOfBookmarks;
        }

        public BookmarkDTO GetBookmarkById(int Id)
        {
            var bookmark = _ReadLaterDataContext.Bookmark.Where(c => c.ID == Id).FirstOrDefault();

            var bookmarkdto = new BookmarkDTO();
            if (bookmark.URL != null)
            {
                bookmarkdto.id = bookmark.ID;
                bookmarkdto.shortdescription = bookmark.ShortDescription;
                bookmarkdto.url = bookmark.URL;
                bookmarkdto.createdate = bookmark.CreateDate;
                bookmarkdto.userID = bookmark.UserID;
                bookmarkdto.category = _ReadLaterDataContext.Categories.Where(c => c.ID == bookmark.CategoryId).FirstOrDefault();
            }

            return bookmarkdto;
        }

        public Task<List<BookmarkDTO>> GetBookmarksByUser(string userId)
        {
            var bookmarks = _ReadLaterDataContext.Bookmark.Where(c => c.UserID == userId).ToList();

            var listOfBookmarksVM = new List<BookmarkDTO>();
            if (bookmarks.Any())
            {
                foreach (var item in bookmarks)
                {
                    var bookmardto = new BookmarkDTO();
                    bookmardto.id = item.ID;
                    bookmardto.shortdescription = item.ShortDescription;
                    bookmardto.url = item.URL;
                    bookmardto.createdate = item.CreateDate;
                    bookmardto.category = _ReadLaterDataContext.Categories.Where(c => c.ID == item.CategoryId).FirstOrDefault();
                    bookmardto.userID = item.UserID;
                    listOfBookmarksVM.Add(bookmardto);
                }
            }

            return Task.FromResult(listOfBookmarksVM);
        }
    }
}