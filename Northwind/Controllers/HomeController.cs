﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Northwind.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Northwind.Data;

namespace Northwind.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NorthwindContext _dbContext;

        public HomeController(ILogger<HomeController> logger, NorthwindContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var result = _dbContext.Products.ToList();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
