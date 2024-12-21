using Microsoft.AspNetCore.Mvc;
using Store;
using Store.Memory;

namespace BallStore.Controllers
{
    public class BallController : Controller
    {
        private readonly IBallRepository ballRepository;

        public BallController(IBallRepository ballRepository)
        {
            this.ballRepository = ballRepository;
        }
        public IActionResult Index(int id)
        {
            Ball ball = ballRepository.GetById(id);
            return View(ball);
        }
    }
}
