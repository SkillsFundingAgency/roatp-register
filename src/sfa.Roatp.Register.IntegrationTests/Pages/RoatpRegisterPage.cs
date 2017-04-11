using sfa.Roatp.Register.IntegrationTests.Driver;
using System;
using System.Net;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace sfa.Roatp.Register.IntegrationTests.Pages
{
    public sealed class RoatpRegisterPage : PageBase
    {

        public RoatpRegisterPage(IRoatpWebDriver driver) : base(driver)
        {
        }

        [FindsBy(How = How.CssSelector, Using ="a")]
        private IList<IWebElement> links { get; set; }


        public HttpWebResponse ClickCSVLink(string uri)
        {
            var csvuri = new Uri(uri + "/download/csv");
         
            // Create a HttpWebrequest object to the desired URL.
            HttpWebRequest csvHttpWebRequest = (HttpWebRequest)WebRequest.Create(csvuri);

            return (HttpWebResponse)csvHttpWebRequest.GetResponse();
        }

        public List<string> ArePageLinksWorking()
        {
            var hrefs = links.Where(x => !string.IsNullOrEmpty(x.GetAttribute("href"))).Select(y => y.GetAttribute("href")).ToList();
            var brokenHrefs = new List<string>();

            foreach (var href in hrefs)
            {
                var webReq = (HttpWebRequest)WebRequest.Create(new Uri(href));
                var webResponse = (HttpWebResponse)webReq.GetResponse();

                if (webResponse.StatusCode != HttpStatusCode.OK)
                {
                    brokenHrefs.Add(href);
                }
            }

            return brokenHrefs;
        }
    }
}
