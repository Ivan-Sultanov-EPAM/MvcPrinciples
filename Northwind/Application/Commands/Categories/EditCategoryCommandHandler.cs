using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.Data;
using Northwind.Extensions;

namespace Northwind.Application.Commands.Categories
{
    public class EditCategoryCommandHandler : IRequestHandler<EditCategoryCommand, Unit>
    {
        private readonly NorthwindContext _dbContext;

        public EditCategoryCommandHandler(NorthwindContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _dbContext.Categories
                .FirstOrDefaultAsync(c => c.CategoryId == request.CategoryId, cancellationToken);

            if (category == null)
                throw new KeyNotFoundException($"Category with Id = {request.CategoryId} does not exist");

            category.UpdateCategory(request);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}