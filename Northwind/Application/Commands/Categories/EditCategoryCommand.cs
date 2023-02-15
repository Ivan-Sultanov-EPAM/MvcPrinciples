using MediatR;
using Northwind.Application.Models.Responses;

namespace Northwind.Application.Commands.Categories
{
    public class EditCategoryCommand : IRequest
    {
        public int CategoryId { get; }
        public string CategoryName { get; }
        public string Description { get; }

        public EditCategoryCommand(int categoryId, CategoryDto request)
        {
            CategoryId = categoryId;
            CategoryName = request.CategoryName;
            Description = request.Description;
        }
    }
}