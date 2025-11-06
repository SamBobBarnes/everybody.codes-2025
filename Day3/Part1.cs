namespace everybody.codes_2025.Day3;

public class Part1() : BasePart(3,1)
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

        return set.Sum().ToString();
    }
}