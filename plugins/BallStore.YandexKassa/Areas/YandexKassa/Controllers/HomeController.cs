using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BallStore.YandexKassa.Area.YandexKassa.Controllers
{
    [Area("YandexKassa")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Callback()
        {
            return View();
        }
    }
}
