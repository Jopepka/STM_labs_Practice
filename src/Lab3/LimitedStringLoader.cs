using System.Text.RegularExpressions;

public class LimitedStringLoader
{
    public readonly string prohibited;
    public readonly string erroneous;
    public readonly int proLimit;

    private List<string>? _loadedLines;

    public List<string> LoadedLines
    {
        get => _loadedLines ?? throw new DataNotLoaded();
    }

    public LimitedStringLoader(string prohibited, string erroneous, int proLimit)
    {
        CheckNotNull(prohibited, nameof(prohibited));
        CheckNotNull(erroneous, nameof(erroneous));
        CheckNotIntersection(prohibited, erroneous);
        CheckNotNegative(proLimit);

        this.prohibited = prohibited.ToUpper();
        this.erroneous = erroneous.ToUpper();
        this.proLimit = proLimit;
    }

    private void CheckNotNull(object argument, string argumentName)
    {
        if (argument is null)
            throw new ArgumentNullException(argumentName);
    }

    private void CheckNotNegative(int number)
    {
        if (number < 0)
            throw new ArgumentException();
    }

    private void CheckNotIntersection(string str1, string str2)
    {
        var intersectItems = str1.Intersect(str2);
        if (intersectItems.Count() > 0)
            throw new InconsistentLimits(intersectItems);
    }

    public void Load(string filename)
    {
        CheckFileExist(filename);
        ProcessingFile(filename);
    }

    private void ProcessingFile(string filename)
    {
        using StreamReader sr = new StreamReader(filename);

        _loadedLines = null;
        List<string> loadedLines = new List<string>();

        for (int lineNumber = 1, countProhibitedLines = 0; sr.Peek() != -1; lineNumber++)
        {
            string? line = sr.ReadLine();

            if (IsEmptyLine(line))
                continue;

            CheckCorrectLine(line, lineNumber);
            CheckValidStartingSymbol(line, lineNumber);

            if (IsIgnore(line))
            {
                CheckLimitProhibitedLines(countProhibitedLines);
                countProhibitedLines++;
                continue;
            }

            loadedLines.Add(line);
        }

        _loadedLines = loadedLines;
    }

    private void CheckFileExist(string filename)
    {
        if (!File.Exists(filename))
            throw new FileNotFoundException($"file {Path.GetFullPath(filename)} not found");
    }

    private bool IsEmptyLine(string? line) => line is null || line == "";

    // Проверяет строку на формат:
    // прописная_латинская_буква список_вещественных_чисел, где элементы списка чисел разделяются пробелами
    private void CheckCorrectLine(string line, int lineNumber)
    {
        if (!Regex.IsMatch(line, @"^[A-Z](\s-?\d+(\.\d+)?)*$"))
            throw new IncorrectString(lineNumber);
    }

    private void CheckValidStartingSymbol(string line, int lineNumber)
    {
        if (erroneous.Contains(line[0]))
            throw new WrongStartingSymbol(line[0], lineNumber);
    }

    private bool IsIgnore(string line) => prohibited.Contains(line[0]);

    private void CheckLimitProhibitedLines(int countProhibitedLines)
    {
        if (countProhibitedLines > proLimit)
            throw new TooManyProhibitedLines();
    }

}