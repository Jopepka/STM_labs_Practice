class Program
{
    static void Main()
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
        WriteEnumerableToConsole(task1To8.Task8_3CustomersWithMinOrdersSum());

      Console.WriteLine("Task PLINQ");
        // У меня выдает при последовательном ~250-340мс, при параллельном ~150-180мс
        new PlinqExample().Start();
    }

    static void WriteEnumerableToConsole<T>(IEnumerable<T> values) =>
        Console.WriteLine(string.Join("\n", values));

    static List<City> cities = new List<City>
        {
            new City { ID = 1, Name = "Лос-Анджелес", CityCode = 213 },
            new City { ID = 2, Name = "Нью-Йорк", CityCode = 212 },
            new City { ID = 3, Name = "Дзержинск", CityCode = 312 },
        };

    static List<Customer> customers = new List<Customer>
        {
            new Customer { ID = 1, Name = "Michael Jackson", City = cities[0] },
            new Customer { ID = 2, Name = "John Cena", City = cities[1] },
            new Customer { ID = 3, Name = "Лава Яша", City = cities[2] },
            new Customer { ID = 4, Name = "Joe Biden", City = cities[0] },
            new Customer { ID = 5, Name = "Leonardo DiCaprio", City = cities[1] },
        };

    static List<Order> orders = new List<Order>
        {
            new Order { ID = 1, Customer = customers[0], Price = 100, Date = new DateTime(2023, 5, 1) },
            new Order { ID = 2, Customer = customers[1], Price = 150, Date = new DateTime(2023, 6, 15) },
            new Order { ID = 3, Customer = customers[2], Price = 200, Date = new DateTime(2023, 7, 20) },
            new Order { ID = 4, Customer = customers[3], Price = 50, Date = new DateTime(2023, 3, 30) },
            new Order { ID = 5, Customer = customers[0], Price = 50, Date = new DateTime(2023, 4, 10) },
            new Order { ID = 6, Customer = customers[0], Price = 50, Date = new DateTime(2023, 4, 10) },
            new Order { ID = 7, Customer = customers[0], Price = 50, Date = new DateTime(2023, 4, 10) },
        };

}
