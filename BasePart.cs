namespace everybody.codes_2025;

public abstract class BasePart
{
    private readonly int _day;
    private readonly bool _test;

    protected BasePart(int day,int part, bool test = false): this("", day, part, test){}

    protected BasePart(string title, int day, int part, bool test = false)
    {
        _day = day;
        _test = test;

        Console.WriteLine($"Running day {_day} part {part}{(_test ? " example" : "")}");
        if (!string.IsNullOrEmpty(title)) Console.WriteLine(title);
        // ReSharper disable once VirtualMemberCallInConstructor
        Console.WriteLine();
        Console.WriteLine(Run());
        Console.WriteLine();
    }

    protected string[] Input()
    {
        string filename = $"day{_day,2:D2}";
        if (_test)
        {
            filename += "_test";
        }
        filename += ".txt";
        return Helpers.LoadInputFile(filename).Split("\n");
    }

    protected char[] InputChars()
    {
        string filename = $"day{_day,2:D2}";
        if (_test)
        {
            filename += "_test";
        }
        filename += ".txt";
        return Helpers.LoadInputFile(filename).ToCharArray();
    }

    public abstract string Run();
}