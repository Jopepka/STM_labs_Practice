using System.Text.RegularExpressions;

public class LimitedStringLoader
{
    public readonly string prohibited;
    public readonly string erroneous;
    public readonly int proLimit;

    private bool _isLoaded = false;
    private List<string> _loadedLines;
    public List<string> LoadedLines { get => _isLoaded ? _loadedLines : throw new DataNotLoaded(); }

    public LimitedStringLoader(string prohibited, string erroneous, int proLimit)
    {
        CheckIntersectionStrings(prohibited, erroneous);

        this.prohibited = prohibited.ToUpper();
        this.erroneous = erroneous.ToUpper();
        this.proLimit = proLimit;
    }

    public void Load(string filename)
    {
        using (StreamReader sr = new StreamReader(filename))
        {
            List<string> loadedLines = new List<string>();

            for (int lineNumber = 0, countProhibitedLines = 0; sr.Peek() != -1; lineNumber++)
            {
                string? line = sr.ReadLine();
                if (line is null)
                    continue;

                CheckWrongStartingSymbol(line, lineNumber);
                CheckCorrectLine(line, lineNumber);

                if (IsIgnore(line))
                {
                    CheckLimitProhibitedLines(countProhibitedLines);
                    countProhibitedLines++;
                    continue;
                }

                loadedLines.Append(line);
            }

            _loadedLines = loadedLines;
            _isLoaded = true;
        }
    }

    private void CheckIntersectionStrings(string str1, string str2)
    {
        var intersectItems = str1.Intersect(str2);
        if (intersectItems.Count() > 0)
            throw new InconsistentLimits(intersectItems, $"There are matching characters: {string.Join(", ", intersectItems)}");
    }

    private bool IsIgnore(string line) => prohibited.Contains(line[0]);

    private void CheckLimitProhibitedLines(int countProhibitedLines)
    {
        if (countProhibitedLines == proLimit)
            throw new TooManyProhibitedLines();
    }

    private void CheckWrongStartingSymbol(string line, int lineNumber)
    {
        if (erroneous.Contains(line[0]))
            throw new WrongStartingSymbol(line[0], lineNumber, $"Line {lineNumber}, wrong start sign '{line[0]}'");
    }

    private void CheckCorrectLine(string line, int lineNumber)
    {
        if (Regex.IsMatch(line, @"^[A-Z](\s-?\d+(\.\d+)?)*$"))
            throw new IncorrectString(lineNumber, $"Line {lineNumber}, incorrect string");
    }

}