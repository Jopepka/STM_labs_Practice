public class Task1To8
{
    readonly List<Customer> _customers;
    readonly List<Order> _orders;
    readonly List<City> _cities;

    public Task1To8(List<Customer> customers, List<Order> orders, List<City> cities)
    {
        _customers = customers;
        _orders = orders;
        _cities = cities;
    }

    public IEnumerable<Customer> Task1_CustomersInLossAngeles() =>
        _customers.Where(customer => customer.City?.Name == "Лос-Анджелес");

    public int Task2_CountClientsWithoutOrders() =>
        _customers.Where(customer => !CustomerOrders(customer).Any()).Count();

    public IEnumerable<Order> CustomerOrders(Customer customer) =>
        _orders.Where(order => order.Customer?.ID == customer.ID);

    public IEnumerable<CustomerInfo> Task3_ClientsInfo() =>
        _customers.Select(customer =>
            new CustomerInfo(
                CustomerName: customer.Name,
                CityName: customer.City?.Name,
                CityCode: customer.City?.CityCode,
                OrdersCount: CustomerOrders(customer).Count(),
                LastOrderDate: CustomerOrders(customer).Max(order => order?.Date)));

    public record CustomerInfo(string? CustomerName, string? CityName, int? CityCode, int? OrdersCount, DateTime? LastOrderDate)
    {
        public override string ToString() =>
            $"{CustomerName} in {CityName} ({CityCode}), count orders: {OrdersCount}, LasOrder: {LastOrderDate}";
    }

    public IEnumerable<Customer> Task4_ClientsWithOrdersMoreThen2() =>
        _customers.Where(customer => CustomerOrders(customer).Count() > 2).OrderBy(customer => customer.Name);

    public IEnumerable<object> Task5_GroupClientsByCity() =>
        _customers.GroupBy(customer => customer.City).Select(item => new PopulationCity(item.Key, item.ToArray()));
    public class PopulationCity(City City, IEnumerable<Customer> Customers)
    {
        public override string ToString() => $"City: {City}\nCustomers:\n\t{string.Join("\n\t", Customers)}";
    }

    public IEnumerable<Customer> Task6_CustomersWithFewerOrdersThanCityAverage() =>
        _customers
            .GroupBy(customer => customer.City)
            .Select(item => new
            {
                city = item.Key,
                customers = item.ToArray(),
                averageCity = item.ToArray().Average(customer => CustomerOrders(customer).Count())
            })
            .SelectMany(item => item.customers.Where(customer => CustomerOrders(customer).Count() < item.averageCity));

    public City Task7_CityWithMaxAmountMoney() =>
        _cities
            .GroupJoin(_customers, city => city, customer => customer.City, (city, customers) => new
            {
                city,
                countManey = customers.Sum(customer => CustomerOrders(customer).Sum(order => order.Price))
            })
            .MaxBy(item => item.countManey).city;

    public IEnumerable<object> Task8_3CustomersWithMinOrdersSum() =>
        _customers
            .GroupJoin(_orders, customer => customer, order => order.Customer, (customer, orders) => new
            {
                customer,
                TotalSum = orders.Sum(order => order.Price),
                OrdersCount = orders.Count()
            })
            .OrderBy(item => item.TotalSum)
            .ThenBy(item => item.OrdersCount)
            .Take(3);
}
