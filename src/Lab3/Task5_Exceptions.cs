using System.Collections;

public class DataNotLoaded : InvalidOperationException
{
    public DataNotLoaded(string message) : base(message) { }
}

public class InconsistentLimits : ArgumentException
{
    public readonly IEnumerable intersectionChars;
    public InconsistentLimits(IEnumerable intersectionChars, string message) : base(message)
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
}

public class WrongStartingSymbol : IncorrectString
{
    public readonly char startCharOfLine;

    public WrongStartingSymbol(char startCharOfLine, int lineNumber, string message) : base(lineNumber, message)
    {
        this.startCharOfLine = startCharOfLine;
    }
}

public class TooManyProhibitedLines : Exception
{
    public TooManyProhibitedLines(string message) : base(message) { }
}
