using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BallStore.YandexKassa.Areas.YandexKassa.Models;

namespace BallStore.YandexKassa.Areas.YandexKassa.Controllers
{
    [Area("YandexKassa")]
    public class HomeController : Controller
    {
        public ActionResult Index(int orderId, string returnUri)
        {
            var model = new ExampleModel
            {
                OrderId = orderId,
                ReturnUri = returnUri,
            };
            return View(model);
        }

        public ActionResult Callback(int orderId, string returnUri)
        {
            var model = new ExampleModel
            {
                OrderId = orderId,
                ReturnUri = returnUri,
            };
            return View(model);
        }
    }
}
