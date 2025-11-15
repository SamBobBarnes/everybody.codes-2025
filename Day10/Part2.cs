using System.Text;

namespace everybody.codes_2025.Day10;

public class Part2() : BasePart(10,2)
{
    public override string Run()
    {
        var input = Input().Select(x=> x.ToCharArray()).ToList();

        var width = input[0].Length;
        var height = input.Count;

        Point? dragon = null;
        List<bool[]> sheep = new List<bool[]>();
        List<bool[]> bushes = new List<bool[]>();

        for (int y = 0; y < height; y++)
        {
            var sheepRow = new bool[width];
            var bushRow = new bool[width];
            for (int x = 0; x < width; x++)
            {
                if (input[y][x] == 'D')
                    dragon = new(x, y);
                if (input[y][x] == 'S')
                    sheepRow[x] = true;
                if (input[y][x] == '#')
                    bushRow[x] = true;
            }

            sheep.Add(sheepRow);
            bushes.Add(bushRow);
        }

        // Console.WriteLine(Print(sheep,'S'));
        // Console.WriteLine(Print(bushes,'#'));

        var moves = 20;

        var q = new Queue<(Point p, int moves)>();
        q.Enqueue((dragon!,0));

        var total = 0;
        var currentRound = 1;
        var currentRoundDragons = new List<Point>();

        while (q.Count > 0)
        {
            var current = q.Dequeue();

            if (currentRoundDragons.Contains(current.p)) continue;

            if (current.moves > currentRound)
            {
                SheepsTurn(sheep, width, bushes, ref currentRoundDragons, ref total, ref currentRound);
            }

            currentRoundDragons.Add(current.p);

            var p = current.p;
            if (sheep[p.Y][p.X] && !bushes[p.Y][p.X])
            {
                sheep[p.Y][p.X] = false;
                total++;
            }

            if (current.moves == moves) continue;

            foreach(var next in GetDragonMoves(p, width, height))
                q.Enqueue((next,current.moves+1));
        }

        SheepsTurn(sheep, width, bushes, ref currentRoundDragons, ref total, ref currentRound);

        return total.ToString();
    }

    private void SheepsTurn(List<bool[]> sheep, int width, List<bool[]> bushes, ref List<Point> currentRoundDragons, ref int total,
        ref int currentRound)
    {
        sheep.Insert(0,new bool[width]);

        foreach (var d in currentRoundDragons)
        {
            if (sheep[d.Y][d.X] && !bushes[d.Y][d.X])
            {
                sheep[d.Y][d.X] = false;
                total++;
            }
        }

        currentRound++;
        currentRoundDragons = new List<Point>();
    }

    private string Print(List<bool[]> input, char item)
    {
        var height = input.Count;
        var width = input[0].Length;
        var sb = new StringBuilder();
        for(int y = 0; y < height; y++)
        {
            var row = "";
            for (int x = 0; x < width; x++)
            {
                if (input[y][x])
                    row += item;
                else row += ".";
            }

            sb.AppendLine(row);
        }

        return sb.ToString();
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