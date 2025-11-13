namespace everybody.codes_2025.Day8;

public class Part2() : BasePart(8,2)
{
    public override string Run()
    {
        var input = Input()[0].Split(',').Select(int.Parse).ToList();

        var nails = 256;

        var lines = new List<(int A, int B)>();

        var total = 0;

        for (int i = 0; i < input.Count-1; i++)
        {
            var a = Math.Min(input[i], input[i + 1]);
            var b = Math.Max(input[i], input[i + 1]);

            foreach (var line in lines)
                total += AreCrossing((a, b), line) ? 1 : 0;

            lines.Add((a, b));
        }

        return total.ToString();
    }

    private bool AreCrossing((int A, int B) x,(int A, int B) y)
    {
        return (x.A < y.A && x.B > y.A && x.B < y.B) || (x.A > y.A && x.A < y.B && x.B > y.B);
    }
}