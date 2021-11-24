using Entity;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ICategoryService
    {
        Category CreateCategory(Category category);
        List<CategoryDTO> GetCategories();
        CategoryDTO GetCategory(int Id);
        CategoryDTO GetCategory(string Name);
        void UpdateCategory(CategoryDTO category);
        void DeleteCategory(CategoryDTO category);
    }
}