using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;


namespace WebApp.Controllers
{
    public class ProductController : Controller
    {
        ProductRepository repository;
        BrandRepository brandRepository;
        CategoryRepository categoryRepository;
        int size = 20;
        public ProductController(IConfiguration configuration)
        {
            repository = new ProductRepository(configuration);
            brandRepository = new BrandRepository(configuration);
            categoryRepository = new CategoryRepository(configuration);
        }
        public IActionResult SearchLoadMore(string q)
        {
            int total;
            List<Product> list = repository.SearchProducts(q, 0, size, out total);
            ViewBag.n = (total - 1) / size + 1;
            return View(list);
        }
        [HttpPost]
        public IActionResult SearchLoadMore(int p, string q)
        {
            int total;
            List<Product> list = repository.SearchProducts(q, (p - 1) * size, size, out total);
            return Json(list);
        }
        public IActionResult Search(string q, int id =1)
        {
            int total;
            List<Product> list = repository.SearchProducts(q, (id - 1) * size, size, out total);
            ViewBag.n=(total - 1) / size + 1;
            return View(list);
        }
        public IActionResult Lazy()
        {
            int total;
            List<Product> list = repository.GetProducts(0, size, out total);
            ViewBag.n = (total - 1) / size + 1;
            return View(list);
        }
        public IActionResult LoadMore()
        {
            int total;
            List<Product> list = repository.GetProducts(0, size, out total);
            ViewBag.n = (total - 1) / size + 1;
            return View(list);
        }
        [HttpPost]
        public IActionResult LoadMore(int id)
        {

            return Json(repository.GetProducts((id - 1) * size, size));
        }
        public IActionResult Index(int id =1)
        {
            int total;
            List<Product> list = repository.GetProducts((id-1)*size, size, out total);
            ViewBag.pages=(total-1)/size+1;
            ViewBag.p = id;
            return View(list);
        }
        public IActionResult Create()
        {
            ViewBag.brands = brandRepository.GetBrands();
            ViewBag.categories = categoryRepository.GetCategories();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product obj)
        {
            try
            {
                repository.Add(obj);
                return Redirect("/product");
            }
            catch(Exception ex)
            {

                Console.WriteLine(ex.Message);
                return Redirect("/product/error");
            }
        }

        public IActionResult Edit(int id)
        {
            ViewBag.brands = brandRepository.GetBrands();
            ViewBag.categories = categoryRepository.GetCategories();
            return View(repository.GetProductByID(id));
        }
        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            try
            {
                repository.Edit(obj);
                return Redirect("/product");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return Redirect("/product/error");
            }
        }
    }
}
