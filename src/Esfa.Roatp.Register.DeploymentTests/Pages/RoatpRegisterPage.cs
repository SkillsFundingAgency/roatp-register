using SFA.Roatp.Api.Client;
using SFA.Roatp.Api.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Esfa.Roatp.Register.DeploymentTests.Pages
{
    public sealed class RoatpRegisterPage
    {
        private readonly string _uri;
        public RoatpRegisterPage(string uri)
        {
            _uri = uri;
        }
        
        public HttpWebResponse ClickCSVLink()
        {
            Console.WriteLine($"CSV Link URL: {_uri}");
            var csvuri = new Uri(_uri + "/download/csv");
         
            // Create a HttpWebrequest object to the desired URL.
            HttpWebRequest csvHttpWebRequest = (HttpWebRequest)WebRequest.Create(csvuri);

            return (HttpWebResponse)csvHttpWebRequest.GetResponse();
        }

        public List<Provider> GetAllProvidersFromApi()
        {
            var roatpapiclient = new RoatpApiClient(_uri);
            return roatpapiclient.FindAll().ToList();
        }
    }
}
