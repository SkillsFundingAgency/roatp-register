using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using CsvHelper;
using Esfa.Roatp.ApplicationServices.Models.Elastic;
using Esfa.Roatp.ApplicationServices.Services;
using Sfa.Roatp.Register.Web.Models;

namespace Sfa.Roatp.Register.Web.Controllers
{
    public class DownloadController : Controller
    {
        private readonly IGetProviders _getProviders;

        public DownloadController(IGetProviders getProviders)
        {
            _getProviders = getProviders;
        }

        [OutputCache(Duration = 600)]
        // GET: Home
        public ActionResult Index()
        {
            var date = _getProviders.GetDateOfProviderList();
            var viewModel = new DownloadViewModel { Filename = GenerateFilename(date), LastUpdated = date };
            return View(viewModel);
        }

        [OutputCache(Duration = 600)]
        public ActionResult Csv()
        {
            var providers = _getProviders.GetAllProviders().Where(x => x.IsDateValid(DateTime.UtcNow) && x.ProviderType != ProviderType.Unknown);
            var date = _getProviders.GetDateOfProviderList();
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
                        return File(memoryStream.ToArray(), "text/csv", GenerateFilename(date));
                    }
                }
            }
        }

        private static string GenerateFilename(DateTime date)
        {
            return $"roatp-{date.ToString("yyyy-MM-dd")}.csv";
        }
    }
}