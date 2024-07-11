using System.Collections;

public class DataNotLoaded : InvalidOperationException
{
    public DataNotLoaded(string message) : base(message) { }
}

public class InconsistentLimits : ArgumentException
{
    public readonly IEnumerable intersectionChars;
    public InconsistentLimits(IEnumerable intersectionChars) : base($"There are matching characters: {string.Join(", ", intersectionChars)}")
    {
        this.intersectionChars = intersectionChars;
    }
}

public class IncorrectString : Exception
{
    public readonly int lineNumber;

    public IncorrectString(int lineNumber, string message) : base(message)
    {
        this.lineNumber = lineNumber;
    }

    public IncorrectString(int lineNumber) : this(lineNumber, $"Line {lineNumber}, incorrect string") { }

}

public class WrongStartingSymbol : IncorrectString
{
    public readonly char startCharOfLine;

    public WrongStartingSymbol(char startCharOfLine, int lineNumber) : base(lineNumber, $"Line {lineNumber}, wrong start sign '{startCharOfLine}'")
    {
        this.startCharOfLine = startCharOfLine;
    }
}

public class TooManyProhibitedLines : Exception
{
    public TooManyProhibitedLines() : base($"The number of missing lines has been exceeded") { }
}
