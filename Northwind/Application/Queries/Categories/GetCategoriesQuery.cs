using MediatR;
using Northwind.Application.Models.Responses;
using System.Collections.Generic;

namespace Northwind.Application.Queries.Categories
{
    public class GetCategoriesQuery : IRequest<List<CategoryDto>>
    {
    }
}