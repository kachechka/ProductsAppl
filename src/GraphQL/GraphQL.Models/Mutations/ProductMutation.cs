using Domain.Abstraction.Repositories;
using GraphQL.Models.InputTypes;
using GraphQL.Models.Types;
using GraphQL.Types;
using ProductsAppl.Domain.Core.Models;
using System;
using System.Collections.Generic;

namespace GraphQL.Models.Mutations
{
    public class ProductMutation : ObjectGraphType
    {
        private static readonly string _createProductQuery = "createProduct";
        private static readonly string _productArgumentName = "product";

        private readonly IPaginationRepository<Product> _productRepository;

        public ProductMutation(IPaginationRepository<Product> productRepository)
        {
            _productRepository = productRepository;

            // creatting product.
            Field<ProductType>(
                _createProductQuery,
                arguments: new QueryArguments(new List<QueryArgument>
                {
                    new QueryArgument<NonNullGraphType<ProductInputType>>
                    {
                        Name = _productArgumentName
                    }
                }),
                resolve: CreateProductResolve);
        }

        private Product CreateProductResolve(ResolveFieldContext<object> context)
        {
            var product = context.GetArgument<Product>(_productArgumentName);

            if (product is null)
            {
                throw new ArgumentNullException();
            }

            product.Id = Guid.NewGuid().ToString();

            _productRepository.Add(product);

            return product;
        }
    }
}
