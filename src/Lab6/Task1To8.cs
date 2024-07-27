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

    public IEnumerable<Customer> Task1_FindCustomersInSanFrancisco() =>
        _cities.Where(city => city.Name == "Сан-Франциско").SelectMany(city => _customers.Where(customer => customer.CityID == city.ID));

    public int Task2_CountClientsWithoutOrders() =>
        _customers.Where(customer => !CustomerOrders(customer).Any()).Count();

    public IEnumerable<Order> CustomerOrders(Customer customer) =>
        _orders.Where(order => order.CustomerID == customer.ID);

    public IEnumerable<CustomerInfo> Task3_ClientsInfo() =>
        _customers
        .Join(_cities, customer => customer.CityID, cities => cities.ID, (customer, city) => new CustomerInfo(
            CustomerName: customer.Name,
            CityName: city.Name,
            CityCode: city.CityCode,
            OrdersCount: CustomerOrders(customer).Count(),
            LastOrderDate: CustomerOrders(customer).Max(order => order.Date)));

    public record CustomerInfo(string CustomerName, string CityName, int CityCode, int OrdersCount, DateTime LastOrderDate);

    public IEnumerable<Customer> Task4_ClientsWithOrdersMoreThen2() =>
        _customers.Where(customer => CustomerOrders(customer).Count() > 2).OrderBy(customer => customer.Name);

    public IEnumerable<object> Task5_GroupClientsByCity() =>
        _cities.GroupJoin(
            _customers,
            city => city.ID,
            customer => customer.CityID,
            (City, Customers) => new
            {
                City,
                Customers,
            });

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
