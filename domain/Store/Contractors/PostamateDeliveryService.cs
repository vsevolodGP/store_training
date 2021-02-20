using System;
using System.Collections.Generic;
using System.Linq;

namespace Store.Contractors
{
    public class PostamateDeliveryService : IDeliveryService
    {
        private static IReadOnlyDictionary<string, string> cities = new Dictionary<string, string>
        {
            { "1", "Киев" },
            { "2", "Харьков" }                       
        };

        private static IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> postamates = new Dictionary<string, IReadOnlyDictionary<string, string>>
        {
            {
                "1",

                new Dictionary<string, string>
                {
                    { "1", "Почтовая площадь" },
                    { "2", "Харьковская" },
                    { "3", "Минская" }
                }
            },
            {
                "2",

                new Dictionary<string, string>
                {
                    { "4", "Исторический музей" },
                    { "5", "Героев труда" },
                    { "6", "ХТЗ" }
                }
            }
        };

        public string Name => "Postamate";

        public string Title => "Доставка через почтоматы в Украине.";

        public Form FirstForm(Order order)
        {
            return Form.CreateFirst(Name)
                       .AddOption("orderId", order.Id.ToString())
                       .AddField(new SelectionField("Город", "city", "1", cities));
        }

        public Form NextForm(int step, IReadOnlyDictionary<string, string> values)
        {
            if (step == 1)
            {
                if (values["city"] == "1")
                {
                    return Form.CreateNext(Name, 2, values)
                               .AddField(new SelectionField("Почтомат", "postamate", "1", postamates["1"]));
                }
                else if (values["city"] == "2")
                {
                    return Form.CreateNext(Name, 2, values)
                               .AddField(new SelectionField("Почтомат", "postamate", "4", postamates["2"]));
                }
                else
                    throw new InvalidOperationException("Invalid postamate city.");
            }
            else if (step == 2)
            {
                return Form.CreateLast(Name, 3, values);
            } 
            else
                throw new InvalidOperationException("Invalid postamate step.");
        }

        public OrderDelivery GetDelivery(Form form)
        {
            if (form.ServiceName != Name || !form.IsFinal)
                throw new InvalidOperationException("Invalid form.");

            var cityId = form.Options["city"];
            var cityName = cities[cityId];
            var postamateId = form.Options["postamate"];
            var postamateName = postamates[cityId][postamateId];

            var options = new Dictionary<string, string>
            {
                { nameof(cityId), cityId },
                { nameof(cityName), cityName },
                { nameof(postamateId), postamateId },
                { nameof(postamateName), postamateName }
            };

            var description = $"Город: {cityName}\nПочтомат: {postamateName}";

            return new OrderDelivery(Name, description, 50m, options);
        }
    }
}
