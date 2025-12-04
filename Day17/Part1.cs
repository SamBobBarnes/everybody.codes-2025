namespace everybody.codes_2025.Day17;

public class Part1() : BasePart(17,1)
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

        var total = 0;
        for(int y = 0; y < input.Length; y++)
            for(int x = 0; x < input[y].Length; x++)
            {
                var calc = (volcano.X - x) * (volcano.X - x) + (volcano.Y - y) * (volcano.Y - y);
                if(calc <= 100)
                    total += input[y][x];
            }

        return total.ToString();
    }
}