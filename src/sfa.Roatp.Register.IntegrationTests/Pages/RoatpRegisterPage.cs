using sfa.Roatp.Register.IntegrationTests.Driver;
using System;
using System.Net;

namespace sfa.Roatp.Register.IntegrationTests.Pages
{
    public sealed class RoatpRegisterPage : Base
    {
        private Uri csvuri;
        public RoatpRegisterPage(IRoatpWebDriver driver) : base(driver)
        {
        }

        public HttpWebResponse ClickCSVLink(string uri)
        {
            csvuri = new Uri(uri + "/download/csv");
         
            // Create a HttpWebrequest object to the desired URL.
            HttpWebRequest csvHttpWebRequest = (HttpWebRequest)WebRequest.Create(csvuri);

            return (HttpWebResponse)csvHttpWebRequest.GetResponse();

        }
    }
}
