using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Northwind.Application.Commands.Products;
using Northwind.Application.Models.Requests;
using Northwind.Application.Models.Responses;
using Northwind.Application.Queries.Categories;
using Northwind.Application.Queries.Products;
using Northwind.Application.Queries.Suppliers;
using Northwind.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northwind.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppSettings _appSettings;
        private readonly IMediator _mediator;

        public ProductsController(AppSettings appSettings, IMediator mediator)
        {
            _appSettings = appSettings;
            _mediator = mediator;
        }

        public async Task<ActionResult<IEnumerable<ProductDto>>> Index()
        {
            return View(await _mediator
                .Send(new GetProductsQuery(0, _appSettings.MaxProductsToShow, null)));
        }

        public async Task<IActionResult> Details(int id)
        {
            var products = await _mediator
                .Send(new GetProductByIdQuery(id));

            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _mediator.Send(new GetCategoriesQuery());
            var suppliers = await _mediator.Send(new GetSuppliersQuery());
            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName");
            ViewData["SupplierId"] = new SelectList(suppliers, "SupplierId", "CompanyName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,SupplierId,CategoryId,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] AddProductRequestDto productDto)
        {
            var categories = await _mediator.Send(new GetCategoriesQuery());
            var suppliers = await _mediator.Send(new GetSuppliersQuery());

            if (ModelState.IsValid)
            {
                await _mediator
                    .Send(new AddProductCommand(productDto));
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName", productDto.CategoryId);
            ViewData["SupplierId"] = new SelectList(suppliers, "SupplierId", "CompanyName", productDto.SupplierId);
            return View(productDto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var categories = await _mediator.Send(new GetCategoriesQuery());
            var suppliers = await _mediator.Send(new GetSuppliersQuery());

            var product = await _mediator
                .Send(new GetProductByIdQuery(id));

            if (product == null)
            {
                return NotFound();
            }

            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(suppliers, "SupplierId", "CompanyName", product.SupplierId);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,SupplierId,CategoryId,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _mediator
                        .Send(new EditProductCommand(id, productDto));
                }
                catch (KeyNotFoundException e)
                {
                    return NotFound(e.Message);
                }

                return RedirectToAction(nameof(Index));
            }

            var categories = await _mediator.Send(new GetCategoriesQuery());
            var suppliers = await _mediator.Send(new GetSuppliersQuery());

            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName", productDto.CategoryId);
            ViewData["SupplierId"] = new SelectList(suppliers, "SupplierId", "CompanyName", productDto.SupplierId);
            return View(productDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _mediator
                .Send(new GetProductByIdQuery(id));

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _mediator
                    .Send(new DeleteProductCommand(id));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
