namespace everybody.codes_2025.Day10;

public class Part2() : BasePart(10,2,true)
{
    public override string Run()
    {
        var input = Input().Select(x=> x.ToCharArray()).ToList();

        var width = input[0].Length;
        var height = input.Count;

        Point? dragon = null;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (input[y][x] == 'D')
                    dragon = new(x, y);
            }
        }

        var moves = 4;

        var q = new Queue<(Point p, int moves)>();
        q.Enqueue((dragon!,0));

        var visited = new List<Point>();
        var total = 0;
        var points = new List<Point>();

        while (q.Count > 0)
        {
            var current = q.Dequeue();

            if (visited.Contains(current.p)) continue;
            visited.Add(current.p);

            var p = current.p;
            if (input[p.Y][p.X] == 'S')
            {
                total++;
                points.Add(current.p);
            }

            if (current.moves == moves) continue;

            foreach(var next in GetDragonMoves(p, width, height))
                q.Enqueue((next,current.moves+1));
        }

        for(int y = 0; y < height; y++)
        {
            var row = "";
            for (int x = 0; x < width; x++)
            {
                if (points.Contains(new(x, y)))
                    row += "X";
                else row += ".";
            }

            Console.WriteLine(row);
        }

        return total.ToString();
    }

    private List<Point> GetDragonMoves(Point current, int width, int height)
    {
        var points = new List<Point>();

        if (current.X >= 1)
        {
            /* w5h5
             * X....
             * .....
             * .D...
             * .....
             * X....
             */

            if (current.Y >= 2)
                points.Add(new(current.X - 1, current.Y - 2));
            if (current.Y <= height - 3)
                points.Add(new(current.X - 1, current.Y + 2));
        }

        if (current.X <= width - 2)
        {
            /* w5h5
             * ....X
             * .....
             * ...D.
             * .....
             * ....X
             */

            if (current.Y >= 2)
                points.Add(new(current.X + 1, current.Y - 2));
            if (current.Y <= height - 3)
                points.Add(new(current.X + 1, current.Y + 2));
        }

        if (current.X >= 2)
        {
            if (current.Y >= 1)
                points.Add(new(current.X - 2, current.Y - 1));
            if (current.Y <= height - 2)
                points.Add(new(current.X - 2, current.Y + 1));
        }

        if (current.X <= width - 3)
        {
            if (current.Y >= 1)
                points.Add(new(current.X + 2, current.Y - 1));
            if (current.Y <= height - 2)
                points.Add(new(current.X + 2, current.Y + 1));
        }

        return points;
    }
}