using MediatR;
using Microsoft.AspNetCore.Mvc;
using Northwind.Application.Commands.Categories;
using Northwind.Application.Models.Requests;
using Northwind.Application.Models.Responses;
using Northwind.Application.Queries.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northwind.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _mediator
                .Send(new GetCategoriesQuery()));
        }

        public async Task<IActionResult> Details(int id)
        {
            var category = await _mediator
                .Send(new GetCategoryByIdQuery(id));

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,Description,Picture")] AddCategoryRequestDto categoryDto)
        {
            if (ModelState.IsValid)
            {
                await _mediator
                    .Send(new AddCategoryCommand(categoryDto));
                return RedirectToAction(nameof(Index));
            }

            return View(categoryDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await _mediator
                .Send(new GetCategoryByIdQuery(id));

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName,Description,Picture")] CategoryDto categoryDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _mediator
                        .Send(new EditCategoryCommand(id, categoryDto));
                }
                catch (KeyNotFoundException e)
                {
                    return NotFound(e.Message);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(categoryDto);
        }
    }
}
