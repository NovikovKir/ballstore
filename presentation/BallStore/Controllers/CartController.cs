using BallStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store;

namespace BallStore.Controllers
{
    public class CartController : Controller
    {
        private readonly IBallRepository ballRepository;

        public CartController(IBallRepository ballRepository)
        {
            this.ballRepository = ballRepository;
        }
        public ActionResult Add(int id)
        {
            var ball = ballRepository.GetById(id);
            Cart cart;
            if(!HttpContext.Session.TryGetCart(out cart))
            {
                cart = new Cart();
            }

            if (cart.Items.ContainsKey(id))
            {
                cart.Items[id]++;
            }
            else
            {
                cart.Items[id] = 1;
            }

            cart.Amount += ball.Price;

            HttpContext.Session.Set(cart);

            return RedirectToAction("Index", "Ball", new {id});
        }
    }
}
