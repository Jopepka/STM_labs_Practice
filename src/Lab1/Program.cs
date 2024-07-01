namespace STM_labs_Practice;

class Program
{
    static void Main(string[] args)
    {
        int countCoins = 0;
        while (true)
        {
            Console.Clear();

            Console.WriteLine("Добро пожаловать в Грустного Хомячка");
            Console.WriteLine("Вводите слово 'хом', чтобы получить 1$!");
            Console.WriteLine("Чтобы вывести $ - закройте программу и они окажутся у вас под подушкой");
            Console.WriteLine($"Баланс: {countCoins}$");

            if (Console.ReadLine() == "хом")
                countCoins++;
        }
    }
}
