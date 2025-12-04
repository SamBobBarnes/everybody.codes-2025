namespace everybody.codes_2025.Day17;

public class Part3() : BasePart(17,3)
{
    public override string Run()
    {
        var volcano0 = new Point3D(0, 0,0);
        var volcano1 = new Point3D(0, 0,1);
        var start = new Point3D(0, 0,0);
        var end = new Point3D(0, 0,1);
        var input = Input().Select(x => x.Select(c =>
        {
            if(c == 'S')
            {
                start = new Point3D(x.ToList().IndexOf(c), Input().ToList().IndexOf(x), 0);
                end = new Point3D(x.ToList().IndexOf(c), Input().ToList().IndexOf(x), 1);
            }
            else if(c == '@')
            {
                volcano0 = new Point3D(x.ToList().IndexOf(c), Input().ToList().IndexOf(x),0);
                volcano1 = new Point3D(x.ToList().IndexOf(c), Input().ToList().IndexOf(x),1);
            }
            else return int.Parse($"{c}");
            return 0;
        }).ToArray()).ToArray();

        int TimeAllowed(int r) => 30 * (r+1);

        for (int r = 0; r <= input[0].Length / 2; r++)
        {
            var distances = new Dictionary<Point3D, int>();
            var prev = new Dictionary<Point3D, Point3D?>();

            for(int z = 0; z <= 1; z++)
                for (int y = 0; y < input.Length; y++)
                    for (int x = 0; x < input[0].Length; x++)
                    {
                        var point = new Point3D(x, y, z);
                        distances[point] = int.MaxValue;
                        prev[point] = null;
                    }
            distances[volcano0] = 0;
            distances[volcano1] = 0;
            distances[start] = 0;

            var total = -1;
            var q = new PriorityQueue<(Point3D point, int timeUsed), int>(new Helpers.ReverseComparer<int>());
            q.Enqueue((start, 0), 0);
            while (q.Count > 0)
            {
                var current = q.Dequeue();
                if(current.point.Equals(end))
                {
                    total = current.timeUsed;
                    // Print(end, prev, input, volcano0,r);
                    break; // run successful
                }
                if (current.timeUsed >= TimeAllowed(r))
                {
                    // Print(current.point, prev, input, volcano0, r);
                    break; // run failed due to time limit
                }
                var vertices = new List<Point3D>();

                var leftPoint = new Point3D(current.point.X - 1, current.point.Y,current.point.Z);
                if(current.point.Y > volcano0.Y && current.point.X == volcano0.X+1 && leftPoint.X == volcano0.X)
                    leftPoint = new Point3D(current.point.X - 1, current.point.Y,current.point.Z == 0 ? 1: 0);  // crosses layer boundary

                var rightPoint = new Point3D(current.point.X + 1, current.point.Y,current.point.Z);
                if(current.point.Y > volcano0.Y && current.point.X == volcano0.X && rightPoint.X == volcano0.X+1)
                    rightPoint = new Point3D(current.point.X + 1, current.point.Y,current.point.Z == 0 ? 1: 0); // crosses layer boundary
                var upPoint = new Point3D(current.point.X, current.point.Y - 1,current.point.Z);
                var downPoint = new Point3D(current.point.X, current.point.Y + 1,current.point.Z);

                if(leftPoint.X > -1)
                    vertices.Add(leftPoint);
                if(rightPoint.X < input[0].Length)
                    vertices.Add(rightPoint);
                if(upPoint.Y > -1)
                    vertices.Add(upPoint);
                if (downPoint.Y < input.Length)
                    vertices.Add(downPoint);

                foreach(var vertex in vertices)
                {
                    if(IsInLava(volcano0, vertex, r))
                        continue; // in lava
                    var value = GetValue(vertex, input) + distances[current.point];
                    if(value < distances[vertex])
                    {
                        distances[vertex] = value;
                        prev[vertex] = current.point;
                        q.Enqueue((vertex, value), value);
                    }


                }
            }
            if(total > -1)
                return (total * r).ToString();
        }

        return 0.ToString();
    }

    private int GetValue(Point point, int[][] map)
    {
        return map[point.Y][point.X];
    }

    private bool IsInLava(Point volcano, Point point, int radius)
    {
        var calc = (volcano.X - point.X) * (volcano.X - point.X) + (volcano.Y - point.Y) * (volcano.Y - point.Y);
        return calc <= radius * radius;
    }

    private void Print(Point3D end, Dictionary<Point3D, Point3D?> prev, int[][] map, Point volcano, int radius)
    {
        var path = new List<Point>();
        var current = end;

        while(current != null)
        {
            path.Add(new(current));
            current = prev[current];
        }

        for(int y = 0; y < map.Length; y++)
        {
            for(int x = 0; x < map[0].Length; x++)
            {
                var point = new Point(x, y);
                if(path.Contains(point))
                {
                    Console.ForegroundColor = IsInLava(volcano, point, radius)
                        ? ConsoleColor.DarkYellow
                        : ConsoleColor.Red;
                    Console.Write(map[y][x]);
                    Console.ResetColor();
                }
                else if(point.Equals(volcano))
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write('@');
                    Console.ResetColor();
                }
                else if(IsInLava(volcano, point, radius))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write('.');
                    Console.ResetColor();
                }
                else
                    Console.Write(map[y][x]);
            }
            Console.WriteLine();
        }
    }
}