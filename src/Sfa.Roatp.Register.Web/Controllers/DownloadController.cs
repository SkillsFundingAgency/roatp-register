using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using CsvHelper;
using Esfa.Roatp.ApplicationServices.Models.Elastic;
using Esfa.Roatp.ApplicationServices.Services;
using Sfa.Roatp.Register.Web.Models;
using SFA.Roatp.Api.Client;

namespace Sfa.Roatp.Register.Web.Controllers
{
    public class DownloadController : Controller
    {
        private readonly IGetProviders _getProviders;
       private readonly IRoatpServiceApiClient _roatpApiClient;

        public DownloadController(IGetProviders getProviders, IRoatpServiceApiClient roatpApiClient)
        {
            _getProviders = getProviders;
            _roatpApiClient = roatpApiClient;
        }

      

        [OutputCache(Duration = 600)]
        // GET: Home
        public ActionResult Index()
        {

            var date = _roatpApiClient.GetLatestNonOnboardingOrganisationChangeDate().Result;

            var viewModel = new DownloadViewModel { Filename = GenerateFilename(date.Value), LastUpdated = date.Value };
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
            return $"roatp-{date.ToString("yyyy-MM-dd HH-mm-ss")}.csv";
        }
    }
}