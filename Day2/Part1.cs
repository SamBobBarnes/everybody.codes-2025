namespace everybody.codes_2025.Day2;

public class Part1() : BasePart(2, 1)
{
    public override string Run()
    {
        var input = Input()[0].Substring(3).TrimEnd(']').Split(",");
        var a = (X: int.Parse(input[0]), Y: int.Parse(input[1]));

        var current = (X: 0, Y: 0);

        var cycles = 3;

        for (int i = 0; i < cycles; i++)
        {
            current = Mulitiply(current, current);
            current = Divide(current, (10, 10));
            current = Add(current, a);
        }

        return $"[{current.X},{current.Y}]";
    }

    private (int X, int Y) Add((int X, int Y) a, (int X, int Y) b)
    {
        return (a.X + b.X, a.Y + b.Y);
    }

    private (int X, int Y) Mulitiply((int X, int Y) a, (int X, int Y) b)
    {
        return (a.X * b.X - a.Y * b.Y, a.X * b.Y + a.Y * b.X);
    }

    private (int X, int Y) Divide((int X, int Y) a, (int X, int Y) b)
    {
        //[X1,Y1] / [X2,Y2] = [X1 / X2, Y1 / Y2]
        return (a.X / b.X, a.Y / b.Y);
    }
}