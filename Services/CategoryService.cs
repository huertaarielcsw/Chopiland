using Data;
using Repo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class CategoryService: ICategoryService
    {
        private IRepository<Category> categoryRepository;

        public CategoryService(IRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public IEnumerable<Category> GetCategories()
        {
            return categoryRepository.GetAll();
        }

        public Category GetCategory(long id)
        {
            return categoryRepository.Get(id);
        }

        public void InsertCategory(Category Category)
        {
            categoryRepository.Insert(Category);
        }

        public void UpdateCategory(Category Category)
        {
            categoryRepository.Update(Category);
        }

        public void DeleteCategory(long id)
        {
            Category Category = GetCategory(id);
            categoryRepository.Remove(Category);
            categoryRepository.SaveChanges();
        }
    }
}
