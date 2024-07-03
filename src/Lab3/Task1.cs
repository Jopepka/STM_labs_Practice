using System.Globalization;
using System.Numerics;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;

public class ConsoleCalculator
{
    private delegate T Operation<T>(T num1, T num2) where T : INumber<T>;

    static Operation<double> Sum = (double num1, double num2) => num1 + num2;
    static Operation<double> Sub = (double num1, double num2) => num1 - num2;
    static Operation<double> Multiplie = (double num1, double num2) => num1 * num2;
    static Operation<double> Divinity = (double num1, double num2) => num1 / num2;

    const int EXIT_ID = -1;
    const int COUNT_OPERATIONS = 4;

    public static void Run()
    {
        Console.WriteLine("Запущен калькулятор");
        Console.WriteLine(GetOperationMenu());
        while (true)
        {
            Console.WriteLine("Введите номер операции:");
            int operationId = GetValidOperationId();
            if (operationId == EXIT_ID)
                break;

            Console.WriteLine("Введите первое число: ");
            double num1 = GetNumberFromConsole<double>();

            Console.WriteLine("Введите второе число: ");
            double num2 = GetNumberFromConsole<double>();

            Console.WriteLine(ApplyOperation(operationId, num1, num2));
        }
    }

    private static string GetOperationMenu()
    {
        string menu = "Введите номер операции. ";
        menu += $"1. Сумма \n2. Вычитание \n3. Умножение \n4. Деление \n{EXIT_ID}. Выход";
        return menu;
    }

    public static T GetNumberFromConsole<T>() where T : INumberBase<T>
    {
        T num;
        while (!T.TryParse(Console.ReadLine(), null, out num))
            Console.WriteLine("Введено не число. Попробуйте еще раз:");
        return num;
    }

    private static int GetValidOperationId()
    {
        int operationId = GetNumberFromConsole<int>();
        while (!IsValidOperationId(operationId))
        {
            Console.WriteLine("Неверный номер операции. Попробуйте еще раз:");
            operationId = GetNumberFromConsole<int>();
        }
        return operationId;
    }

    private static bool IsValidOperationId(int operationId) =>
        operationId > 0 && operationId <= COUNT_OPERATIONS || operationId == EXIT_ID;

    private static string? ApplyOperation(int operationId, double num1, double num2)
    {
        string operationSign;
        double resOperation;

        switch (operationId)
        {
            case 1:
                operationSign = "+";
                resOperation = Sum(num1, num2);
                break;
            case 2:
                operationSign = "-";
                resOperation = Sub(num1, num2);
                break;
            case 3:
                operationSign = "*";
                resOperation = Multiplie(num1, num2);
                break;
            case 4:
                operationSign = "/";
                resOperation = Divinity(num1, num2);
                break;
            default:
                return "Неверный номер операции";
        }

        return $"{num1} {operationSign} {num2} = {resOperation}";
    }
}