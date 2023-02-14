using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.Application.Models.Responses;
using Northwind.Data;
using Northwind.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Northwind.Application.Queries.Suppliers
{
    public class GetSuppliersQueryHandler : IRequestHandler<GetSuppliersQuery, List<SupplierResponseDto>>
    {
        private readonly NorthwindContext _dbContext;

        public GetSuppliersQueryHandler(NorthwindContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<SupplierResponseDto>> Handle(GetSuppliersQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Suppliers
                .Select(s => s.ToSupplierResponseDto())
                .ToListAsync(cancellationToken);
        }
    }
}