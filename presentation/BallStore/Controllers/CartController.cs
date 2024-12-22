using BallStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store;

namespace BallStore.Controllers
{
    public class CartController : Controller
    {
        private readonly IBallRepository ballRepository;
        private readonly IOrderRepository orderRepository;

        public CartController(IBallRepository ballRepository, IOrderRepository orderRepository)
        {
            this.ballRepository = ballRepository;
            this.orderRepository = orderRepository;
        }
        public ActionResult Add(int id)
        {
            Order order;
            Cart cart;
            if(HttpContext.Session.TryGetCart(out cart))
            {
                order = orderRepository.GetById(cart.OrderId);
            }
            else
            {
                order = orderRepository.Create();
                cart = new Cart(order.Id);
            }

            var ball = ballRepository.GetById(id);
            order.AddItem(ball, 1);
            orderRepository.Update(order);

            cart.TotalCount = order.TotalCount;
            cart.TotalPrice = order.TotalPrice;
            HttpContext.Session.Set(cart);

            return RedirectToAction("Index", "Ball", new {id});
        }
    }
}
