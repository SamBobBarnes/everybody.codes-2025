namespace everybody.codes_2025.Day2;

public class Part2() : BasePart(2,2,"A=[-4591,-68892]")
{
    public override string Run()
    {
        var input = Input()[0].Substring(3).TrimEnd(']').Split(",");
        var a = (X: long.Parse(input[0]), Y: long.Parse(input[1]));
        var extent = Add(a, (1000, 1000));
        var gridSize = 101;
        var total = 0;

        for (long y = 0; y < gridSize; y++)
        {
            for (long x = 0; x < gridSize; x++)
            {
                var pointUnderExamination = (a.X + 10 * x, a.Y + 10 * y);
                total += PerformCycles(pointUnderExamination) ? 1:0;
            }
        }

        return total.ToString();
    }

    private bool PerformCycles((long X, long Y) point)
    {
        var current = (X: 0L, Y: 0L);
        var cycles = 100;
        var upperLimit = 1000000;
        var lowerLimit = -1000000;

        for (long i = 0; i < cycles; i++)
        {
            current = Mulitiply(current, current);
            current = Divide(current, (100000, 100000));
            current = Add(current, point);

            if (current.X > upperLimit || current.X < lowerLimit || current.Y > upperLimit || current.Y < lowerLimit)
                return false;
        }

        return true;
    }

    private (long X, long Y) Add((long X, long Y) a, (long X, long Y) b)
    {
        return (a.X + b.X, a.Y + b.Y);
    }

    private (long X, long Y) Mulitiply((long X, long Y) a, (long X, long Y) b)
    {
        return (a.X * b.X - a.Y * b.Y, a.X * b.Y + a.Y * b.X);
    }

    private (long X, long Y) Divide((long X, long Y) a, (long X, long Y) b)
    {
        return (a.X / b.X, a.Y / b.Y);
    }
}