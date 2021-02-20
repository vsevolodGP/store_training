using Microsoft.AspNetCore.Http;
using Store.Contractors;
using Store.Web.Contractors;
using System;
using System.Collections.Generic;

namespace Store.Cashier
{
    public class CashierPaymentService : IPaymentService, IWebContractorService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CashierPaymentService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        private HttpRequest Request => httpContextAccessor.HttpContext.Request;

        public string Name => "Cashier";

        public string Title => "Оплата банковской картой";

        public Form FirstForm(Order order)
        {
            return Form.CreateFirst(Name)
                       .AddOption("orderId", order.Id.ToString());
        }

        public Form NextForm(int step, IReadOnlyDictionary<string, string> values)
        {
            if (step != 1)
                throw new InvalidOperationException("Invalid Cashier payment step.");

            return Form.CreateLast(Name, step + 1, values);
        }

        public OrderPayment GetPayment(Form form)
        {
            if (form.ServiceName != Name || !form.IsFinal)
                throw new InvalidOperationException("Invalid payment form");

            return new OrderPayment(Name, "Оплата картой", form.Options);
        }

        public Uri StartSession(IReadOnlyDictionary<string, string> options, Uri returnUri)
        {
            var queryString = QueryString.Create(options);
            queryString += QueryString.Create("returnUri", returnUri.ToString());

            var builder = new UriBuilder(Request.Scheme, Request.Host.Host)
            {
                Path = "Cashier/",
                Query = queryString.ToString()
            };

            if (Request.Host.Port != null)
                builder.Port = Request.Host.Port.Value;

            return builder.Uri;
        }
            
    }
}
