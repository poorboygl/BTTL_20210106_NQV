using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApp.Models;

namespace WebApp.Controllers
{
    public abstract class BaseController : Controller
    {
        protected SiteProvider provider;
        public BaseController(IConfiguration configuration)
        {
            provider = new SiteProvider(configuration);
        }
        protected override void Dispose(bool disposing)
        {
            provider.Dispose();
        }
    }
}
