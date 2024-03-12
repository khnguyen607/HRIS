using Microsoft.AspNetCore.Mvc;
using WS.Web.Controllers;

namespace WS.Web.Public.Controllers
{
    public class AboutController : WSControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}