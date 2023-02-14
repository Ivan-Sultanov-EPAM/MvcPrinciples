using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.Application.Models.Responses;
using Northwind.Data;
using Northwind.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Northwind.Application.Queries.Products
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductDto>>
    {
        private readonly NorthwindContext _dbContext;

        public GetProductsQueryHandler(NorthwindContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Where(p => request.CategoryId == null || p.CategoryId == request.CategoryId)
                .OrderBy(p => p.ProductName)
                .Skip(request.PageSize * request.PageNumber)
                .Take(request.PageSize)
                .Select(p => p.ToProductResponseDto())
                .ToListAsync(cancellationToken);
        }
    }
}