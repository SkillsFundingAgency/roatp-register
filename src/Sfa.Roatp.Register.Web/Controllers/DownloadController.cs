using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Web.Mvc;
using CsvHelper;
using Sfa.Roatp.Register.Core.Models;
using Sfa.Roatp.Register.Core.Services;
using Sfa.Roatp.Register.Web.Models;
using SFA.ROATP.Types;

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

        [OutputCache(Duration = 600)]
        public ActionResult Csv()
        {
            var providers = _getProviders.GetAllProviders().Where(x => x.IsDateValid(DateTime.UtcNow));
            var result = providers.Select(CsvProviderMapper.Map);

            using (var memoryStream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(memoryStream))
                {
                    using (var csvWriter = new CsvWriter(streamWriter))
                    {
                        csvWriter.WriteRecords(result);
                        streamWriter.Flush();
                        memoryStream.Position = 0;
                        return File(memoryStream.ToArray(), "text/csv", $"roatp-{DateTime.UtcNow.ToString("yyyy-MM-dd")}.csv");
                    }
                }
            }
        }
    }
}