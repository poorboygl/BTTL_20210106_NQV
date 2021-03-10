using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class BrandController : BaseController
    {
        //BrandRepository repository;
       
        public BrandController(IConfiguration configuration ) :base(configuration)
        {
            //repository = new BrandRepository(configuration);
        }
        public IActionResult Index()
        {
            return View(provider.Brand.GetBrands());
        }
        public IActionResult Detail(int id)
        {
            return Json(provider.Brand.GetBrandById(id));
        }
        [HttpPost]
        public IActionResult Create(Brand obj)
        {
            provider.Brand.Save(obj);
            return Redirect("/brand");
        }
    }
}
