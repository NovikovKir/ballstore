using BallStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store;
using System.Linq;
using System.Net;

namespace BallStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IBallRepository ballRepository;
        private readonly IOrderRepository orderRepository;

        public OrderController(IBallRepository ballRepository, IOrderRepository orderRepository)
        {
            this.ballRepository = ballRepository;
            this.orderRepository = orderRepository;
        }

        public ActionResult Index() 
        {
            if (HttpContext.Session.TryGetCart(out Cart cart))
            {
                var order = orderRepository.GetById(cart.OrderId);
                OrderModel model = Map(order);

                return View(model);
            }

            return View("Empty");
        }

        private OrderModel Map(Order order)
        {
            var ballIds = order.Items.Select(item => item.BallId);
            var balls = ballRepository.GetAllByIds(ballIds);
            var itemModels = from item in order.Items
                             join ball in balls on item.BallId equals ball.Id
                             select new OrderItemModel
                             {
                                 BallId = ball.Id,
                                 Brand = ball.Brand,
                                 Model = ball.Model,
                                 Price = item.Price,
                                 Count = item.Count,
                             };
            return new OrderModel
            {
                Id = order.Id,
                Items = itemModels.ToArray(),
                TotalCount = order.TotalCount,
                TotalPrice = order.TotalPrice,
            };
        }

        public ActionResult AddItem(int ballId, int count = 1)
        {
            (Order order, Cart cart) = GetOrCreateOrderAndCart();
            
            var ball = ballRepository.GetById(ballId);
                        
            order.AddOrUpdateItem(ball, count);

            SaveOrderAndCart(order, cart);

            return RedirectToAction("Index", "Ball", new { id = ballId });
        }

        [HttpPost]
        public ActionResult UpdateItem(int ballId, int count)
        {
            (Order order, Cart cart) = GetOrCreateOrderAndCart();

            order.GetItem(ballId).Count = count;

            SaveOrderAndCart(order, cart);

            return RedirectToAction("Index", "Ball", new { ballId });
        }
                
        private (Order order, Cart cart) GetOrCreateOrderAndCart()
        {
            Order order;
            if(HttpContext.Session.TryGetCart(out Cart cart))
            {
                order = orderRepository.GetById(cart.OrderId);
            }
            else
            {
                order = orderRepository.Create();
                cart = new Cart(order.Id);
            }

            return (order, cart);
        }

        private void SaveOrderAndCart(Order order, Cart cart)
        {
            orderRepository.Update(order);

            cart.TotalCount = order.TotalCount;
            cart.TotalPrice = order.TotalPrice;
            HttpContext.Session.Set(cart);
        }

        public ActionResult RemoveItem(int id)
        {
            (Order order, Cart cart) = GetOrCreateOrderAndCart();

            order.RemoveItem(id);
            SaveOrderAndCart(order, cart);

            return RedirectToAction("Index", "Ball", new { id });
        }
    }
}
