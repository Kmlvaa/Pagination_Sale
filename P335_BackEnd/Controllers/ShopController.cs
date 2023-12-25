using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P335_BackEnd.Data;
using P335_BackEnd.Entities;
using P335_BackEnd.Models;
using P335_BackEnd.Services;

namespace P335_BackEnd.Controllers
{
    public class ShopController : Controller
    {
        private AppDbContext _dbContext;
        private readonly ProductService _productService;

        public ShopController(AppDbContext dbContext, ProductService productService)
        {
            _dbContext = dbContext;
            _productService = productService;
        }

        public IActionResult Index(int page, string order)
        {
            if (page <= 0) page = 1;

            int productsPerPage = 3;
            var productCount = _dbContext.ShopProducts.Count();

            int totalPageCount = (int)Math.Ceiling(((decimal)productCount / productsPerPage));

            var latest = _productService.GetLatestProducts();
            var saleOff = _productService.GetSaleOFfProducts();

            var sorted = _dbContext.ShopProducts.AsQueryable();
            ViewData["OrderById"] = string.IsNullOrEmpty(order) ? "desc" : "asc";

            switch (order)
            {
                case "desc":
                    sorted = sorted.OrderByDescending(x => x.Id);
                    break;
                case "asc":
                    sorted = sorted.OrderBy(x => x.Id);
                    break;
                default:
                    sorted = sorted.OrderByDescending(x => x.Id);
                    break;
            }
            var model = new ShopIndexVM
            {
                ShopProducts = _dbContext.ShopProducts
                    .Skip((page - 1) * productsPerPage)
                    .Take(productsPerPage)
                    .ToList(),
                TotalPageCount = totalPageCount,
                CurrentPage = page,
                LatestProducts = latest,
                SaleOffProducts = saleOff
            };
            ViewBag.Order = order;
            return View(model);
        }
    }
}
    
