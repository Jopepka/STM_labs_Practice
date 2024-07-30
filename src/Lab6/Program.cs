class Program
{
    static void Main()
    {
        Console.WriteLine("Task PLINQ");
        // У меня выдает при последовательном ~250-340мс, при параллельном ~150-180мс
        new PlinqExample().Start();
    }

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
            new Order { ID = 5, CustomerID = 4, Price = 75, Date = new DateTime(2023, 4, 10) },
        };

    static List<City> cities = new List<City>
        {
            new City { ID = 1, Name = "Лос-Анджелес", CityCode = 213 },
            new City { ID = 2, Name = "Нью-Йорк", CityCode = 212 },
            new City { ID = 3, Name = "Дзержинск", CityCode = 312 },
        };
}
