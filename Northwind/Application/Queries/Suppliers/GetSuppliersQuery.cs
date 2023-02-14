using MediatR;
using Northwind.Application.Models.Responses;
using System.Collections.Generic;

namespace Northwind.Application.Queries.Suppliers
{
    public class GetSuppliersQuery : IRequest<List<SupplierResponseDto>>
    {
    }
}