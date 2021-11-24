using Data;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    [Route("Category")]

    public class CategoryService : ICategoryService
    {
        private ReadLaterDataContext _ReadLaterDataContext;
        public CategoryService(ReadLaterDataContext readLaterDataContext) 
        {
            _ReadLaterDataContext = readLaterDataContext;            
        }

        public Category CreateCategory(Category category)
        {
            var entitycategory = new Category();

            if (category != null)
            {
                entitycategory.ID = category.ID;
                entitycategory.Name = category.Name;

                _ReadLaterDataContext.Add(entitycategory);
                _ReadLaterDataContext.SaveChanges();
            }
            return entitycategory;
        }

        public void UpdateCategory(CategoryDTO category)
        {
            var item = _ReadLaterDataContext.Categories.Where(c => c.ID == category.id).FirstOrDefault();

            if (item != null)
            {
                item.Name = category.name;
                item.ID = category.id;

                _ReadLaterDataContext.Update(item);
                _ReadLaterDataContext.SaveChanges();
            }
        }

        public void DeleteCategory(CategoryDTO category)
        {
            var ctg = _ReadLaterDataContext.Categories.FirstOrDefault(x => x.ID == category.id);

            if (ctg != null)
            {
                _ReadLaterDataContext.Categories.Remove(ctg);
                _ReadLaterDataContext.SaveChanges();
            }
        }

        public List<CategoryDTO> GetCategories()
        {
            var categories = _ReadLaterDataContext.Categories.ToList();

            var listOfCategories = new List<CategoryDTO>();

            if (categories.Any())
            {
                foreach (var item in categories)
                {
                    var category = new CategoryDTO();
                    category.id = item.ID;
                    category.name = item.Name;

                    listOfCategories.Add(category);
                }
            }

            return listOfCategories;
        }

        public CategoryDTO GetCategory(int Id)
        {
            var category = _ReadLaterDataContext.Categories.Where(c => c.ID == Id).FirstOrDefault();

            if (category != null)
            {
                var categoryDTO = new CategoryDTO
                {
                    id = category.ID,
                    name = category.Name,
                };

                return categoryDTO;
            }
            else
            {
                return new CategoryDTO();
            }
        }

        public CategoryDTO GetCategory(string Name)
        {
            var category = _ReadLaterDataContext.Categories.Where(c => c.Name == Name).FirstOrDefault();

            var categorye = new CategoryDTO();

            if (category != null)
            {
                categorye.id = category.ID;
                categorye.name = category?.Name;
            }

            return categorye;
        }
    }
}