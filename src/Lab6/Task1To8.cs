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
        _cities.Where(city => city.Name == "Лос-Анджелес").SelectMany(city => _customers.Where(customer => customer.CityID == city.ID));

    public int Task2_CountClientsWithoutOrders() =>
        _customers.Where(customer => !CustomerOrders(customer).Any()).Count();

    public IEnumerable<Order> CustomerOrders(Customer customer) =>
        _orders.Where(order => order.CustomerID == customer.ID);

    public IEnumerable<CustomerInfo> Task3_ClientsInfo()
    {
        var item1 = _customers
        .Select(customer => new { customer, city = _cities.Find(city => city.ID == customer.CityID) }).ToArray();

        var item2 = item1.Select(item =>
            new CustomerInfo(
                CustomerName: item.customer.Name,
                CityName: item.city.Name,
                CityCode: item.city.CityCode,
                OrdersCount: CustomerOrders(item.customer).Count(),
                LastOrderDate: CustomerOrders(item.customer).Max(order => order?.Date)));

        return item2;
    }

    public record CustomerInfo(string? CustomerName, string? CityName, int? CityCode, int? OrdersCount, DateTime? LastOrderDate)
    {
        public override string ToString() =>
            $"{CustomerName} in {CityName} ({CityCode}), count orders: {OrdersCount}, LasOrder: {LastOrderDate}";
    }

    public IEnumerable<Customer> Task4_ClientsWithOrdersMoreThen2() =>
        _customers.Where(customer => CustomerOrders(customer).Count() > 2).OrderBy(customer => customer.Name);

    public IEnumerable<object> Task5_GroupClientsByCity() =>
        _cities.GroupJoin(
            _customers,
            city => city.ID,
            customer => customer.CityID,
            (City, Customers) => new PopulationCity(City, Customers));
    public class PopulationCity(City City, IEnumerable<Customer> Customers)
    {
        public override string ToString() => $"City: {City}\nCustomers:\n\t{string.Join("\n\t", Customers)}";
    }

    public IEnumerable<Customer> Task6_CustomersWithFewerOrdersThanCityAverage() =>
        _cities
            .GroupJoin(_customers, city => city.ID, customer => customer.CityID, (city, customers) => new { city, customers })
            .SelectMany(item => item.customers.Where(customer => CustomerOrders(customer).Count() < item.customers.Average(customer => CustomerOrders(customer).Count())));



    public City Task7_CityWithMaxAmountMoney() =>
        _cities
            .GroupJoin(_customers, city => city.ID, customer => customer.CityID, (city, customers) => new
            {
                city,
                countManey = customers.Sum(customer => CustomerOrders(customer).Sum(order => order.Price))
            })
            .MaxBy(item => item.countManey).city;

    public IEnumerable<object> Task8_CustomersWithMinOrdersSum() =>
        _customers
            .GroupJoin(_orders, customer => customer.ID, order => order.CustomerID, (customer, orders) => new
            {
                customer,
                TotalSum = orders.Sum(order => order.Price),
                OrdersCount = orders.Count()
            })
            .OrderBy(item => item.TotalSum)
            .ThenBy(item => item.OrdersCount)
            .Take(3);
}
