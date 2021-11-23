using Entity;
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
        List<Category> GetCategories();
        Category GetCategory(int Id);
        Category GetCategory(string Name);
        Category UpdateCategory(Category category);
        Category DeleteCategory(Category category);
    }
}
