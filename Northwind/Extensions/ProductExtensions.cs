using Northwind.Application.Commands.Products;
using Northwind.Application.Models.Responses;
using Northwind.Entities;

namespace Northwind.Extensions
{
    public static class ProductExtensions
    {
        public static ProductDto ToProductResponseDto(this Product product)
        {
            return new ProductDto
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                CompanyName = product.Supplier.CompanyName,
                CategoryName = product.Category.CategoryName,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                UnitsOnOrder = product.UnitsOnOrder,
                ReorderLevel = product.ReorderLevel,
                Discontinued = product.Discontinued,
                SupplierId = product.SupplierId,
                CategoryId = product.CategoryId
            };
        }

        public static void UpdateProduct(this Product product, EditProductCommand request)
        {
            product.ProductName = request.ProductName;
            product.SupplierId = request.SupplierId;
            product.CategoryId = request.CategoryId;
            product.QuantityPerUnit = request.QuantityPerUnit;
            product.UnitPrice = request.UnitPrice;
            product.UnitsInStock = request.UnitsInStock;
            product.UnitsOnOrder = request.UnitsOnOrder;
            product.ReorderLevel = request.ReorderLevel;
            product.Discontinued = request.Discontinued;
        }

        public static Product ToProduct(this AddProductCommand product)
        {
            return new Product
            {
                ProductName = product.ProductName,
                SupplierId = product.SupplierId,
                CategoryId = product.CategoryId,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                UnitsOnOrder = product.UnitsOnOrder,
                ReorderLevel = product.ReorderLevel,
                Discontinued = product.Discontinued
            };
        }
    }
}