namespace everybody.codes_2025.Day19;

public class Part3() : BasePart(19,3)
{
    public override string Run()
    {
        var input = Input().Select(x => x.Split(',').Select(int.Parse).ToArray()).ToArray();

        var walls = new List<Point>();
        var passages = new List<Point>();
        var top = input.Max(x => (x[1] + x[2])*2);
        var extent = input.Max(x => x[0]);
        foreach (var wall in input)
        {
            passages.AddRange(Enumerable.Range(wall[1], wall[2]).Select(y => new Point(wall[0], y)));
            walls.AddRange(Enumerable.Range(0, top + 1).Select(y => new Point(wall[0], y)));
        }

        walls = walls.Except(passages).ToList();

        var q = new PriorityQueue<Point, int>(new Helpers.ReverseComparer<int>());
        var endPoints = passages.Where(p => p.X == extent).ToList();

        var distances = new Dictionary<Point, int>();

        for(int y = 0; y <= top; y++)
        for (int x = 0; x <= extent; x++)
        {
            if(walls.Contains(new Point(x, y)))
                continue;
            distances.Add(new Point(x, y), int.MaxValue);
        }

        var start = new Point(0, 0);
        distances[start] = 0;

        q.Enqueue(start, 0);

        while (q.Count > 0)
        {
            var current = q.Dequeue();

            var neighbors = new List<Point>
            {
                new Point(current.X + 1, current.Y+1),
                new Point(current.X + 1, current.Y-1),
            };

            foreach(var neighbor in neighbors)
            {
                if(neighbor.Y < 0 || neighbor.Y > top || neighbor.X > extent || walls.Contains(neighbor))
                    continue;

                var alt = distances[current] + (current.Y > neighbor.Y ? 0 : 1);
                if (alt < distances[neighbor])
                {
                    distances[neighbor] = alt;
                    q.Enqueue(neighbor, alt);
                }
            }

        }

        return endPoints.Select(p => distances[p]).Min().ToString();
    }
}