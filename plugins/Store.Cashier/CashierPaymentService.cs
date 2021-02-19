using Store.Contractors;
using Store.Web.Contractors;
using System.Collections.Generic;

namespace Store.Cashier
{
    public class CashierPaymentService : IPaymentService, IWebContractorService
    {
        public string UniqueCode => "Cashier";

        public string Title => "Оплата банковской картой";

        public string GetUri => "/Cashier/";

        public Form CreateForm(Order order)
        {
            return new Form(UniqueCode, order.Id, 1, true, new Field[0]);
        }

        public OrderPayment GetPayment(Form form)
        {
            return new OrderPayment(UniqueCode, "Оплата картой", new Dictionary<string, string>());
        }

        public Form MoveNextForm(int orderId, int step, IReadOnlyDictionary<string, string> values)
        {
            return new Form(UniqueCode, orderId, 2, true, new Field[0]);
        }
    }
}
