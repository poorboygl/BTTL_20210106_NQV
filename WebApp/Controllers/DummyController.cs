using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class DummyController :Controller
    {
        IConfiguration configuration;
        public DummyController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public IActionResult Index()
        {
            string connectionString = configuration.GetConnectionString("BikeStore");
            return View("Index", connectionString);
        }
        
    }
}
