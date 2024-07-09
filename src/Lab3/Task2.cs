using System.Data.Common;
using System.Reflection.Metadata.Ecma335;

public class Task2
{
    private static string[] nameDays = ["Понедельник", "Вторник", "Среда", "Четверг",
                                        "Пятница", "Суббота", "Воскресенье"];

    private static int id = -1;
    public static Func<string> GetDayOWeekFunc() => () => id == nameDays.Length - 1 ? nameDays[id = 0] : nameDays[++id];
}