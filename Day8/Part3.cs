namespace everybody.codes_2025.Day8;

public class Part3() : BasePart(8,3)
{
    public override string Run()
    {
        var input = Input()[0].Split(',').Select(int.Parse).ToList();

        var nails = 256;
        var lines = new List<(int A, int B)>();

        var max = 0;

        for (int i = 0; i < input.Count-1; i++)
        {
            var a = Math.Min(input[i], input[i + 1]);
            var b = Math.Max(input[i], input[i + 1]);

            lines.Add((a, b));
        }

        for (int i = 1; i <= nails; i++)
        for (int j = 1; j <= nails; j++)
        {
            if (i == j) continue;

            var total = 0;
            var a = Math.Min(i,j);
            var b = Math.Max(i,j);

            if (a-b == 1) continue;

            foreach (var line in lines)
            {
                if (AreCrossing((a, b), line))
                    total++;
            }

            if (total > max) max = total;
        }

        return max.ToString();
    }

    private bool AreCrossing((int A, int B) x,(int A, int B) y)
    {
        return (x.A < y.A && x.B > y.A && x.B < y.B) || (x.A > y.A && x.A < y.B && x.B > y.B) || (x.A == y.A && x.B == y.B);
    }
}