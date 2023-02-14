using Northwind.Application.Models.Responses;
using Northwind.Entities;

namespace Northwind.Extensions
{
    public static class SuppliersExtensions
    {
        public static SupplierResponseDto ToSupplierResponseDto(this Supplier supplier)
        {
            return new SupplierResponseDto
            {
                SupplierId = supplier.SupplierId,
                CompanyName = supplier.CompanyName
            };
        }
    }
}