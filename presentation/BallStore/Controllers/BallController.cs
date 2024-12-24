using Microsoft.AspNetCore.Mvc;
using Store;
using Store.Memory;
using Store.Web.App;

namespace BallStore.Controllers
{
    public class BallController : Controller
    {
        private readonly BallService ballService;

        public BallController(BallService ballService)
        {
            this.ballService = ballService;
        }

        public IActionResult Index(int id)
        {
            var model = ballService.GetById(id);    

            return View(model);
        }
    }
}
