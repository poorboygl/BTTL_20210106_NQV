using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AjaxController : Controller
    {
        CategoryRepository repository;
        public AjaxController(IConfiguration configuration)
        {
            repository = new CategoryRepository(configuration);
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Categories()
        {
            return Json(repository.GetCategories());    
        }
        [HttpPost]
        public IActionResult CategoryByID(int id)
        {
            return Json(repository.GetCategorybyID(id));
        }
    }
}
