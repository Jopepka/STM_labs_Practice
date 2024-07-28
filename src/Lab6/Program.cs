namespace Lab6;

class Program
{
    static void Main(string[] args)
    {
        var task1To8 = new Task1To8(customers, orders, cities);

        Console.WriteLine("Task 1. CustomersInLossAngeles");
        WriteEnumerableToConsole(task1To8.Task1_CustomersInLossAngeles());

        Console.WriteLine("\nTask 2. CountClientsWithoutOrders");
        Console.WriteLine(task1To8.Task2_CountClientsWithoutOrders());

        Console.WriteLine("\nTask 3. ClientsInfo");
        WriteEnumerableToConsole(task1To8.Task3_ClientsInfo());

        Console.WriteLine("\nTask 4. ClientsWithOrdersMoreThen2");
        WriteEnumerableToConsole(task1To8.Task4_ClientsWithOrdersMoreThen2());

        Console.WriteLine("\nTask 5. GroupClientsByCity");
        WriteEnumerableToConsole(task1To8.Task5_GroupClientsByCity());

        Console.WriteLine("\nTask 6. CustomersWithFewerOrdersThanCityAverage");
        WriteEnumerableToConsole(task1To8.Task6_CustomersWithFewerOrdersThanCityAverage());

        Console.WriteLine("\nTask 7. CityWithMaxAmountMoney");
        Console.WriteLine(task1To8.Task7_CityWithMaxAmountMoney().Name);

        Console.WriteLine("\nTask 8. CustomersWithMinOrdersSum");
        WriteEnumerableToConsole(task1To8.Task8_CustomersWithMinOrdersSum());
    }

    static void WriteEnumerableToConsole<T>(IEnumerable<T> values) =>
        Console.WriteLine(string.Join("\n", values));

    static List<Customer> customers = new List<Customer>
        {
            new Customer { ID = 1, Name = "Michael Jackson", CityID = 1 },
            new Customer { ID = 2, Name = "John Cena", CityID = 2 },
            new Customer { ID = 3, Name = "Лава Яша", CityID = 3 },
            new Customer { ID = 4, Name = "Joe Biden", CityID = 1 },
            new Customer { ID = 5, Name = "Leonardo DiCaprio", CityID = 2 },
        };

    static List<Order> orders = new List<Order>
        {
            new Order { ID = 1, CustomerID = 1, Price = 100, Date = new DateTime(2023, 5, 1) },
            new Order { ID = 2, CustomerID = 1, Price = 150, Date = new DateTime(2023, 6, 15) },
            new Order { ID = 3, CustomerID = 2, Price = 200, Date = new DateTime(2023, 7, 20) },
            new Order { ID = 4, CustomerID = 3, Price = 50, Date = new DateTime(2023, 3, 30) },
            new Order { ID = 5, CustomerID = 1, Price = 50, Date = new DateTime(2023, 4, 10) },
            new Order { ID = 6, CustomerID = 1, Price = 50, Date = new DateTime(2023, 4, 10) },
            new Order { ID = 7, CustomerID = 1, Price = 50, Date = new DateTime(2023, 4, 10) },
        };

    static List<City> cities = new List<City>
        {
            new City { ID = 1, Name = "Лос-Анджелес", CityCode = 213 },
            new City { ID = 2, Name = "Нью-Йорк", CityCode = 212 },
            new City { ID = 3, Name = "Дзержинск", CityCode = 312 },
        };
}
