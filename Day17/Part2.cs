namespace everybody.codes_2025.Day17;

public class Part2() : BasePart(17,2)
{
    public override string Run()
    {
        var volcano = new Point(0, 0);
        var input = Input().Select(x => x.Select(c =>
        {
            if (c != '@') return int.Parse($"{c}");

            volcano = new Point(x.ToList().IndexOf(c), Input().ToList().IndexOf(x));
            return 0;
        }).ToArray()).ToArray();

        var totals = new Dictionary<int,int>();
        for(int y = 0; y < input.Length; y++)
        for(int x = 0; x < input[y].Length; x++)
        {
            for(int r = 1; r <= input[y].Length/2; r++)
            {
                totals.TryAdd(r, 0);
                var calc = (volcano.X - x) * (volcano.X - x) + (volcano.Y - y) * (volcano.Y - y);
                if (calc <= r*r)
                {
                    totals[r] += input[y][x];
                    break;
                }
            }
        }

        var max = 0;
        var bestR = 0;
        foreach (var x in totals)
        {
            if(x.Value > max)
            {
                max = x.Value;
                bestR = x.Key;
            }
        }

        return (max * bestR).ToString();
    }
}