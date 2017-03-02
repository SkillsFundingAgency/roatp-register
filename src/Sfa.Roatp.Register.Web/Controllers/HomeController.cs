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

        public ActionResult Registry()
        {
            return View();
        }

        public ActionResult GetExportFileContentResult()
        {
            try
            {
                var providers = _getProviders.GetAllProviders();

                using (var memoryStream = new MemoryStream())
                {
                    using (var streamWriter = new StreamWriter(memoryStream))
                    {
                        using (var csvWriter = new CsvWriter(streamWriter))
                        {
                            csvWriter.WriteRecords(providers);
                            streamWriter.Flush();
                            memoryStream.Position = 0;
                            return File(memoryStream.ToArray(), "text/csv", "patata.csv");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}