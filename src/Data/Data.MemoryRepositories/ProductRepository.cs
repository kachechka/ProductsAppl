using Domain.Abstraction.Repositories;
using ProductsAppl.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Data.MemoryRepositories
{
    public class ProductRepository : IPaginationRepository<Product>
    {
        private readonly List<Product> _products;

        public ProductRepository()
        {
            _products = new List<Product>(10);

            for (var i = 1; i <= _products.Capacity; ++i)
            {
                _products.Add(new Product
                {
                    Id = i.ToString(),
                    Name = $"Name {i}",
                    Description = $"Description {i}",
                    Price = i,
                    SalePrice = null
                });
            }
        }

        public void Add(Product item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            _products.Add(item);
        }

        public void Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException();
            }

            var index = _products.FindIndex(p => p.Id.Equals(id));

            if (index >= 0)
            {
                _products.RemoveAt(index);
            }
        }

        public Product Get(Func<Product, bool> predicate)
            => _products.FirstOrDefault(predicate);

        public List<Product> GetAll()
            => _products;

        public List<Product> GetAll(Func<Product, bool> predicate)
            => _products
                .Where(predicate)
                .ToList();

        public List<Product> GetAll(int skip, int take)
            => _products
            .Skip(skip)
            .Take(take)
            .ToList();

        public List<Product> GetAll(Func<Product, bool> predicate, int skip, int take)
            => _products
            .Where(predicate)
            .Skip(skip)
            .Take(take)
            .ToList();

        public void Update(Product item)
        {
            var index = _products.FindIndex(p => p.Id.Equals(item.Id));

            if (index >= 0)
            {
                _products[index] = item;
            }
        }
    }
}
