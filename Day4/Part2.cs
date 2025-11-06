namespace everybody.codes_2025.Day4;

public class Part2() : BasePart(4,2)
{
    public override string Run()
    {
        var input = Input().Select(int.Parse).ToList();

        decimal outputRotations = 10000000000000;

        var ratios = new List<decimal>();

        for (int i = 1; i < input.Count(); i++)
        {
            ratios.Add((decimal)input[i - 1] / input[i]);
        }

        decimal finalRatio = 1;

        foreach (var ratio in ratios)
        {
            finalRatio *= ratio;
        }

        var finalRotations = outputRotations / finalRatio;
        return Math.Ceiling(finalRotations).ToString();
    }
}