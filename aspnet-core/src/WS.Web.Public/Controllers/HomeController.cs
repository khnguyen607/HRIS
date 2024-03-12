using Microsoft.AspNetCore.Mvc;
using WS.Web.Controllers;

namespace WS.Web.Public.Controllers
{
    public class HomeController : WSControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}