using System;
using System.Collections.Generic;
using System.Text;
using Data;
using Repo;

namespace Services
{
    public class ProductService : IProductService
    {
        private IRepository<Product> productRepository;

        public ProductService(IRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }


        public void DeleteProduct(long id)
        {
            Product product = GetProduct(id);
            productRepository.Remove(product);
            productRepository.SaveChanges();
        }

        public Product GetProduct(long id)
        {
            return productRepository.Get(id);
        }

        public IEnumerable<Product> GetProducts()
        {
            return productRepository.GetAll();
        }

        public void InsertProduct(Product product)
        {
            productRepository.Insert(product);
        }

        public void UpdateProduct(Product product)
        {
            productRepository.Update(product);
        }
    }
}
