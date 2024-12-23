﻿using BallStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store;
using Store.Messages;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace BallStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IBallRepository ballRepository;
        private readonly IOrderRepository orderRepository;
        private readonly INotificationService notificationService;

        public OrderController(IBallRepository ballRepository, IOrderRepository orderRepository, INotificationService notificationService)
        {
            this.ballRepository = ballRepository;
            this.orderRepository = orderRepository;
            this.notificationService = notificationService;
        }

        [HttpGet]
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

        [HttpPost]
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

            return RedirectToAction("Index", "Order");
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

        [HttpPost]
        public ActionResult RemoveItem(int ballId)
        {
            (Order order, Cart cart) = GetOrCreateOrderAndCart();

            order.RemoveItem(ballId);
            SaveOrderAndCart(order, cart);

            return RedirectToAction("Index", "Order");
        }

        [HttpPost]
        public IActionResult SendConfirmationCode(int id, string cellPhone)
        {
            var order = orderRepository.GetById(id);
            var model = Map(order);
            if (!IsValidCellPhone(cellPhone))
            {
                model.Errors["cellPhone"] = "Номер телефона не соответствует формату +79876543210";
                return View("Index", model);
            }
            int code = 1111; // random.Next(1000, 10000)
            HttpContext.Session.SetInt32(cellPhone, code);
            notificationService.SendConfirmationCode(cellPhone, code);
            return View("Confirmation",
                        new ConfirmationModel
                        {
                            OrderId = id,
                            CellPhone = cellPhone
                        });
        }

        [HttpPost]
        public IActionResult StartDelivery(int id, string cellPhone, int code)
        {
            int? storedCode = HttpContext.Session.GetInt32(cellPhone);
            if (storedCode == null)
            {
                return View("Confirmation",
                            new ConfirmationModel
                            {
                                OrderId = id,
                                CellPhone = cellPhone,
                                Errors = new Dictionary<string, string>
                                {
                                    { "code", "Пустой код, повторите отправку" }
                                },
                            }); ;
            }
            if (storedCode != code)
            {
                return View("Confirmation",
                            new ConfirmationModel
                            {
                                OrderId = id,
                                CellPhone = cellPhone,
                                Errors = new Dictionary<string, string>
                                {
                                    { "code", "Отличается от отправленного" }
                                },
                            }); ;
            }
            //
            return View();
        }
        private bool IsValidCellPhone(string cellPhone)
        {
            if (cellPhone == null)
                return false;
            cellPhone = cellPhone.Replace(" ", "")
                                 .Replace("-", "");
            return Regex.IsMatch(cellPhone, @"^\+?\d{11}$");
        }


    }
}