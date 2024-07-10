using System.Numerics;

public class ConsoleCalculator
{
    public static void Run()
    {
        while (true)
        {
            try
            {
                CalculateAction();
            }
            catch (NotImplementedException)
            {
                break;
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine($"Делить на ноль нельзя!. Попробуй еще раз");
            }
        }
    }

    private static void CalculateAction()
    {
        Console.WriteLine(GetOperationMenu());

        Console.WriteLine("Введите первое число: ");
        double num1 = GetNumberFromConsole<double>();

        Console.WriteLine("Введите второе число: ");
        double num2 = GetNumberFromConsole<double>();

        Console.WriteLine("Введите символ операции:");
        string operationSign = Console.ReadLine();

        Console.WriteLine(ApplyOperation(operationSign, num1, num2));
    }

    private static string GetOperationMenu()
    {
        string menu = "Операции. ";
        menu += $"\n+. Сумма \n-. Вычитание \n*. Умножение \n/. Деление \nДля выхода введите любой любой другой символ";
        return menu;
    }

    public static T GetNumberFromConsole<T>() where T : INumberBase<T>
    {
        T num;
        while (!T.TryParse(Console.ReadLine(), null, out num))
            Console.WriteLine("Введено не число. Попробуйте еще раз:");
        return num;
    }

    private static Func<double, double, double> GetOperation(string operationSign) => operationSign switch
    {
        "+" => (num1, num2) => num1 + num2,
        "-" => (num1, num2) => num1 - num2,
        "*" => (num1, num2) => num1 * num2,
        "/" => (num1, num2) => num2 == 0 ? throw new DivideByZeroException() : num1 / num2,
        _ => throw new NotImplementedException($"Operation '{operationSign}' is not impelemented"),
    };

    private static string ApplyOperation(string operationSign, double num1, double num2) =>
        $"{num1} {operationSign} {num2} = {GetOperation(operationSign).Invoke(num1, num2)}";
}
