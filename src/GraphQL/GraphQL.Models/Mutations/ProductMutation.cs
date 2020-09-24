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
        private static readonly string _updateProductQuery = "updateProduct";
        private static readonly string _deleteProductQuery = "deleteProduct";
        private static readonly string _productArgumentName = "product";
        private static readonly string _idArgumentName = "id";

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

            // updating product.
            Field<ProductType>(
                _updateProductQuery,
                arguments: new QueryArguments(new List<QueryArgument>
                {
                    new QueryArgument<NonNullGraphType<ProductInputType>>
                    {
                        Name = _productArgumentName
                    }
                }),
                resolve: UpdateProductResolve);

            // deleting product.
            Field<StringGraphType>(
                _deleteProductQuery,
                arguments: new QueryArguments(new List<QueryArgument>
                {
                    new QueryArgument<NonNullGraphType<StringGraphType>>
                    {
                        Name = _idArgumentName
                    }
                }),
                resolve: DeleteProductResolve);
        }

        private string DeleteProductResolve(ResolveFieldContext<object> context)
        {
            var id = context.GetArgument<string>(_idArgumentName);

            _productRepository.Delete(id);

            return id;
        }

        private Product UpdateProductResolve(ResolveFieldContext<object> context)
        {
            var product = context.GetArgument<Product>(_productArgumentName);

            _productRepository.Update(product);

            return product;
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
