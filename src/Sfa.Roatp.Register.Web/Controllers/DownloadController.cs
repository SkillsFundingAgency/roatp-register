using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using CsvHelper;
using Sfa.Roatp.Register.Core.Models;
using Sfa.Roatp.Register.Core.Services;

namespace Sfa.Roatp.Register.Web.Controllers
{
    public class DownloadController : Controller
    {
        private readonly IGetProviders _getProviders;

        public DownloadController(IGetProviders getProviders)
        {
            _getProviders = getProviders;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Csv()
        {
            try
            {
                var providers = _getProviders.GetAllProviders();

                foreach (var roatpProvider in providers)
                {
                    if (roatpProvider.StartDate == default(DateTime))
                    {
                        roatpProvider.StartDate = null;
                    }

                    if (roatpProvider.EndDate == default(DateTime))
                    {
                        roatpProvider.EndDate = null;
                    }
                }

                using (var memoryStream = new MemoryStream())
                {
                    using (var streamWriter = new StreamWriter(memoryStream))
                    {
                        using (var csvWriter = new CsvWriter(streamWriter))
                        {
                            csvWriter.WriteRecords(providers);
                            streamWriter.Flush();
                            memoryStream.Position = 0;
                            return File(memoryStream.ToArray(), "text/csv", $"roatp-{DateTime.Today.ToString("yyyy-MM-dd")}.csv");
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