using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Store.Domain.Abstract;
using Store.Domain.Entities;
using Store.UI.Models;

namespace Store.UI.Controllers
{
    public class ProductController : Controller
    {
        public IProductRepository repository;
        public int PageSize = 4;

        public ProductController(IProductRepository productRepository)
        {
            this.repository = productRepository;
        }
        public ViewResult List(string category, int page = 1)
        {
            ViewBag.myCount = repository.Products.Count<Product>();

            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = repository.Products
                            .Where(p => category ==null || p.Category == category)
                            .OrderBy(p => p.ProductID)
                            .Skip((page - 1) * PageSize)
                            .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? repository.Products.Count() : repository.Products.Where(e => e.Category == category).Count()
                },

                CurrentCategory = category

            };
            return View(model);
        }
    }
}