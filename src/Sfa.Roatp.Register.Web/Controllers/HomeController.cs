using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using CsvHelper;
using Sfa.Roatp.Register.Core.Models;
using Sfa.Roatp.Register.Core.Services;

namespace Sfa.Roatp.Register.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGetProviders _getProviders;

        public HomeController(IGetProviders getProviders)
        {
            _getProviders = getProviders;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}