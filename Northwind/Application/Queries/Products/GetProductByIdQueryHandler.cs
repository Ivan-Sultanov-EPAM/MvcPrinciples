using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.Application.Models.Responses;
using Northwind.Data;
using Northwind.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace Northwind.Application.Queries.Products
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly NorthwindContext _dbContext;

        public GetProductByIdQueryHandler(NorthwindContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(p => p.ProductId == request.ProductId, cancellationToken);

            return product?.ToProductResponseDto();
        }
    }
}