using Data;
using Entity;
using Entity.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    [Route("Category")]

    public class CategoryService : ICategoryService
    {
        private ReadLaterDataContext _ReadLaterDataContext;
        //private ErrorViewModel _errorViewModel;
        public CategoryService(ReadLaterDataContext readLaterDataContext) 
        {
            _ReadLaterDataContext = readLaterDataContext;            
        }

        [Route("CreateCategory")]
        [HttpPost]
        public Category CreateCategory(Category category)
        {
            var categories = new Category();

            if (categories.ID > 0) categories = _ReadLaterDataContext.Categories.FirstOrDefault(x => x.ID == category.ID);

            if (categories == null)
            {
                return Json("CategoryNotFound");
            }

            categories.ID = category.ID;
            category.Name = category.Name;
            category.Bookmarks = category.Bookmarks.Select(b => new Bookmark()
            {
                ID = b.ID,
                URL = b.URL,
                ShortDescription = b.ShortDescription,
                CreateDate = DateTime.Now
            }).ToList();

            if(_ReadLaterDataContext.Categories.Where( x=> x.Name == category.Name
                                                       && x.ID != category.ID).FirstOrDefault() != null)
            {
                return Json("ClientExists");
            }

            _ReadLaterDataContext.Add(category);
            _ReadLaterDataContext.SaveChanges();

            CategoryDTO categoryDTO = new CategoryDTO()
            {
                id = categories.ID,
                name = categories.Name,
                BookmarksDTO = categories.Bookmarks.Select(d => new BookmarksDTO()
                {
                    id = d.ID,
                    url = d.URL,
                    shortdescription = d.ShortDescription,
                    createdate = DateTime.Now
                }).ToList()
            };

            return category;
        }

        [Route("UpdateCategory")]
        [HttpPost]
        public Category UpdateCategory(Category category)
        {
            var categories = new Category();

            if (categories.ID > 0) categories = _ReadLaterDataContext.Categories.FirstOrDefault(x => x.ID == category.ID);

            if (categories == null)
            {
                return Json("CategoryNotFound");
            }

            categories.ID = category.ID;
            category.Name = category.Name;
            category.Bookmarks = category.Bookmarks.Select(b => new Bookmark()
            {
                ID = b.ID,
                URL = b.URL,
                ShortDescription = b.ShortDescription,
                CreateDate = DateTime.Now
            }).ToList();

            if (categories.ID > 0)
            {
                _ReadLaterDataContext.Update(category);
            }

            _ReadLaterDataContext.SaveChanges();

            CategoryDTO categoryDTO = new CategoryDTO()
            {
                id = categories.ID,
                name = categories.Name,
                BookmarksDTO = categories.Bookmarks.Select(d => new BookmarksDTO()
                {
                    id = d.ID,
                    url = d.URL,
                    shortdescription = d.ShortDescription,
                    createdate = DateTime.Now
                }).ToList()
            };

            return category;
        }

        [Route("GetCategories")]
        [HttpGet]
        public List<Category> GetCategories()
        {
            List<CategoryDTO> res = new List<CategoryDTO>();

            var categoryList = _ReadLaterDataContext.Categories
                               .Include(c => c.Bookmarks)
                               .OrderBy(c => c.Name).ToList();

            foreach (var category in categoryList)
            {
                //var c = category.
                CategoryDTO cc = new CategoryDTO();
                cc.id = category.ID;
                cc.name = category.Name;
                List<BookmarksDTO> bookmarksDTO = new List<BookmarksDTO>();
                foreach (var b in bookmarksDTO)
                {
                    BookmarksDTO bm = new BookmarksDTO();
                    bm.id = b.id;
                    bm.url = b.url;
                    bm.shortdescription = b.shortdescription;
                    bm.createdate = b.createdate;
                }

                cc.BookmarksDTO = bookmarksDTO;
                res.Add(cc);
            }

            return _ReadLaterDataContext.Categories.ToList();
            //return (res);
        }

        [Route("GetCategory")]
        [HttpGet]
        public Category GetCategory(int Id)
        {
            var categories = _ReadLaterDataContext.Categories
                             .Include(x => x.ID)
                             .Where(x => x.ID > 0 ? x.ID == Id : true)
                             .ToList();

            //CategoryDTO res = new CategoryDTO(); 

            foreach(var c in categories)
            {
                CategoryDTO cdto = new CategoryDTO();
                cdto.id = c.ID;
                cdto.name = c.Name;

                List<BookmarksDTO> bmdto = new List<BookmarksDTO>();
                foreach(var b in c.Bookmarks)
                {
                    BookmarksDTO bm = new BookmarksDTO();
                    bm.id = b.ID;
                    bm.url = b.URL;
                    bm.shortdescription = b.ShortDescription;
                    bm.createdate = b.CreateDate;
                    bmdto.Add(bm);
                }

                cdto.BookmarksDTO = bmdto;

            }

            return _ReadLaterDataContext.Categories.Where(c => c.ID == Id).FirstOrDefault();
        }

        [Route("GetCategory")]
        [HttpGet]
        public Category GetCategory(string Name)
        {
            var categories = _ReadLaterDataContext.Categories
                             .Include(x => x.Name)
                             .Where(x => x.Name != null ? x.Name == Name : true)
                             .ToList();

            //CategoryDTO res = new CategoryDTO(); 

            foreach (var c in categories)
            {
                CategoryDTO cdto = new CategoryDTO();
                cdto.id = c.ID;
                cdto.name = c.Name;
                List<BookmarksDTO> bmdto = new List<BookmarksDTO>();
                foreach (var b in c.Bookmarks)
                {
                    BookmarksDTO bm = new BookmarksDTO();
                    bm.id = b.ID;
                    bm.url = b.URL;
                    bm.shortdescription = b.ShortDescription;
                    bm.createdate = b.CreateDate;
                    bmdto.Add(bm);
                }

                cdto.BookmarksDTO = bmdto;

            }

            return _ReadLaterDataContext.Categories.Where(c => c.Name == Name).FirstOrDefault();
        }

        [Route("DeleteCategory")]
        [HttpDelete]
        public Category DeleteCategory([FromBody]Category category)
        {
            var ctg = _ReadLaterDataContext.Categories.FirstOrDefault(x => x.ID == category.ID);

            if(ctg == null)
            {
                return Json("CategoryNotFound");
            }
            else
            {
                _ReadLaterDataContext.Categories.Remove(ctg);
                _ReadLaterDataContext.SaveChanges();
            }

            return Json("CategoryDeleted!");
        }

        private Category Json(string v)
        {
            throw new NotImplementedException();
        }
    }
}
