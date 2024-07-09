public class LimitedStringLoader
{
    public readonly string prohibited; // игнорируемые символы
    public readonly string erroneous; // ошибочные символы
    public readonly int proLimit;

    private bool _isLoaded = false;
    private List<string> _loadedFileLines;
    // Если пользователь пытается обратиться к загруженному списку (через свойство) до вызова метода Load, 
    // то должно генерироваться исключение DataNotLoaded
    public List<string> LoadedFileLines { get => _isLoaded ? _loadedFileLines : throw new Exception(); }

    public LimitedStringLoader(string prohibited, string erroneous, int proLimit)
    {
        CheckIntersectionStrings(prohibited, erroneous);

        this.prohibited = prohibited.ToUpper();
        this.erroneous = erroneous.ToUpper();
        this.proLimit = proLimit;
    }

    //должно генерироваться исключение InconsistentLimits, содержащее информацию о символах в пересечении.
    private void CheckIntersectionStrings(string str1, string str2)
    {
        if (str1.Intersect(str2).Count() > 0)
            throw new Exception();
    }

    public void Load(string filename)
    {
        // CheckIsFileExist(filename);
        List<string> lines = new List<string>();
        using (StreamReader sr = new StreamReader(filename))
        {
            while (sr.Peek() != -1)
            {
                string line = sr.ReadLine() ?? "";
                char sign = line[0];

                if (prohibited.Contains(sign))
                    continue;

                if (erroneous.Contains(sign))
                    throw new Exception(); // WrongStartingSymbol

                if (sign < 'A' || sign > 'Z')
                    throw new Exception(); // IncorrectString

                lines.Append(line);
            }
        }
    }

    private void CheckIsFileExist(string filename)
    {
        if (File.Exists(filename))
            throw new FileNotFoundException();
    }

}