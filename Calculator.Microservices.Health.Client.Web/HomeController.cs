using Microsoft.AspNetCore.Mvc;

namespace Calculator.Microservices.Health.Client.Web
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Redirect("/hc-ui");
        }
    }
}
