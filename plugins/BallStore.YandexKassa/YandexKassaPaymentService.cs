﻿using Microsoft.AspNetCore.Http;
using BallStore.Contractors;
using Store;
using Store.Contractors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallStore.YandexKassa
{
    public class YandexKassaPaymentService : IPaymentService, IWebContractorsService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public YandexKassaPaymentService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        private HttpRequest Request => httpContextAccessor.HttpContext.Request;
        public string Name => "YandexKassa";
        public string Title => "Оплата банковской картой";
        public Form FirstForm(Order order)
        {
            return Form.CreateFirst(Name)
                       .AddParameter("orderId", order.Id.ToString());
        }
        public Form NextForm(int step, IReadOnlyDictionary<string, string> values)
        {
            if (step != 1)
                throw new InvalidOperationException("Invalid Yandex.Kassa payment step.");
            return Form.CreateLast(Name, step + 1, values);
        }
        public OrderPayment GetPayment(Form form)
        {
            if (form.ServiceName != Name || !form.IsFinal)
                throw new InvalidOperationException("Invalid payment form.");
            return new OrderPayment(Name, "Оплатой картой", form.Parameters);
        }
        public Uri StartSession(IReadOnlyDictionary<string, string> parameters, Uri returnUri)
        {
            var queryString = QueryString.Create(parameters);
            queryString += QueryString.Create("returnUri", returnUri.ToString());
            var builder = new UriBuilder(Request.Scheme, Request.Host.Host)
            {
                Path = "YandexKassa/",
                Query = queryString.ToString(),
            };
            if (Request.Host.Port != null)
                builder.Port = Request.Host.Port.Value;
            return builder.Uri;
        }
    }
}
