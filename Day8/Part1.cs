namespace everybody.codes_2025.Day8;

public class Part1() : BasePart(8,1)
{
    public override string Run()
    {
        var input = Input()[0].Split(',').Select(int.Parse).ToList();

        var nails = 32;

        var total = 0;

        for (int i = 0; i < input.Count-1; i++)
        {
            var a = Math.Max(input[i], input[i + 1]);
            var b = Math.Min(input[i], input[i + 1]);
            if (a - b == nails / 2) total++;
        }

        return total.ToString();
    }
}