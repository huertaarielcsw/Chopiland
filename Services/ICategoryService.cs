using Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();
        Category GetCategory(long id);
        void InsertCategory(Category Category);
        void UpdateCategory(Category Category);
        void DeleteCategory(long id);
    }
}
