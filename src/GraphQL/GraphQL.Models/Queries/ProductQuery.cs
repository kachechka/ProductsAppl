using Domain.Abstraction.Repositories;
using GraphQL.Models.InputTypes;
using GraphQL.Models.Types;
using GraphQL.Types;
using ProductsAppl.Domain.Core.Models;
using System;
using System.Collections.Generic;

namespace GraphQL.Models.Queries
{
    public class ProductQuery : ObjectGraphType
    {
        private static readonly string _productsQuery = "products";
        private static readonly string _productQuery = "product";

        private static readonly string _skipArgumentName = "skip";
        private static readonly string _takeArgumentName = "take";
        private static readonly string _idArgumentName = "id";

        private readonly IPaginationRepository<Product> _productRepository;

        public ProductQuery(IPaginationRepository<Product> productRepository)
        {
            _productRepository = productRepository;

            // getting product list.
            Field<ListGraphType<ProductType>>(
                _productsQuery,
                arguments: new QueryArguments(new List<QueryArgument>
                {
                    new QueryArgument<IntGraphType>
                    {
                        Name = _skipArgumentName
                    },
                    new QueryArgument<IntGraphType>
                    {
                        Name = _takeArgumentName
                    }
                }),
                resolve: ProductsResolve);

            // getting product by id.
            Field<ProductType>(
                _productQuery,
                arguments: new QueryArguments(new List<QueryArgument>
                {
                    new QueryArgument<StringGraphType>
                    {
                        Name = _idArgumentName
                    }
                }),
                resolve: ProductResolve);
        }

        private Product ProductResolve(ResolveFieldContext<object> context)
        {
            var id = context.GetArgument<string>(_idArgumentName);

            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException();
            }

            return _productRepository.Get(p => p.Id.Equals(id));
        }

        private List<Product> ProductsResolve(ResolveFieldContext<object> context)
        {
            var take = context.GetArgument<int?>(_takeArgumentName);
            var skip = context.GetArgument<int?>(_skipArgumentName);

            if (take.HasValue && skip.HasValue)
            {
                return _productRepository.GetAll(skip.Value, take.Value);
            }
            else if (take.HasValue)
            {
                return _productRepository.GetAll(0, take.Value);
            }
            else if (skip.HasValue)
            {
                return _productRepository.GetAll(skip.Value, int.MaxValue);
            }
            return _productRepository.GetAll();
        }
    }
}
