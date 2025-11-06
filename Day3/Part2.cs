namespace everybody.codes_2025.Day3;

public class Part2() : BasePart(3,2)
{
    public override string Run()
    {
        var input = Input()[0].Split(",").Select(int.Parse).ToList();
        input.Sort();

        var set = new List<int>{input[0]};
        foreach (var box in input)
        {
            if (set.Count > 0 && set.Last() < box)
                set.Add(box);
        }

        return set[..20].Sum().ToString();
    }
}