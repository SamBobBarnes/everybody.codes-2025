using System.Globalization;

namespace everybody.codes_2025.Day4;

public class Part1() : BasePart(4,1)
{
    public override string Run()
    {
        var input = Input().Select(int.Parse).ToList();

        var inputRotations = 2025;

        var ratios = new List<float>();

        for (int i = 1; i < input.Count(); i++)
        {
            ratios.Add((float)input[i - 1] / input[i]);
        }

        float finalRotations = inputRotations;

        foreach (var ratio in ratios)
        {
            finalRotations *= ratio;
        }
        return Math.Floor(finalRotations).ToString(CultureInfo.CurrentCulture);
    }
}