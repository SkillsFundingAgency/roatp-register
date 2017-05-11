using System.Text;
using System.Web.Mvc;
using Microsoft.Azure;

namespace Sfa.Roatp.Register.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        // GET: Home
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Download");
        }
        
        public ActionResult Api()
        {
            return View();
        }

        [OutputCache(Duration = 86400)]
        public ContentResult RobotsText()
        {
            var builder = new StringBuilder();

            builder.AppendLine("User-agent: *");

            if (!bool.Parse(CloudConfigurationManager.GetSetting("FeatureToggle.RobotsAllowFeature")??"false"))
            {
                builder.AppendLine("Disallow: /");
            }

            return Content(builder.ToString(), "text/plain", Encoding.UTF8);
        }
    }
}