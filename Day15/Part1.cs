using System.Text;

namespace everybody.codes_2025.Day15;

public class Part1() : BasePart(15,1)
{
    public override string Run()
    {
        var input = Input()[0].Split(',').Select(x => (Direction:x.Substring(0, 1), Length: int.Parse(x.Substring(1))));

        var map = new List<Point>();
        var width = 1;
        var height = 1;
        var currentDirection = Direction.Up;
        var current = new Point(0, 0);
        foreach (var wall in input)
        {
            if(wall.Direction == "L")
                switch (currentDirection)
                {
                    case Direction.Down:
                        currentDirection = Direction.Right;
                        break;
                    case Direction.Right:
                        currentDirection = Direction.Up;
                        break;
                    case Direction.Left:
                        currentDirection = Direction.Down;
                        break;
                    case Direction.Up:
                        currentDirection = Direction.Left;
                        break;
                }
            else
                switch (currentDirection)
                {
                    case Direction.Down:
                        currentDirection = Direction.Left;
                        break;
                    case Direction.Right:
                        currentDirection = Direction.Down;
                        break;
                    case Direction.Left:
                        currentDirection = Direction.Up;
                        break;
                    case Direction.Up:
                        currentDirection = Direction.Right;
                        break;
                }

            switch (currentDirection)
            {
                case Direction.Up:
                    if(current.Y < wall.Length)
                    {
                        map = map.Select(p => new Point(p.X, p.Y+ wall.Length)).ToList();
                        height += wall.Length - current.Y;
                        current = new(current.X, current.Y+ wall.Length);
                    }

                    map.AddRange(Enumerable.Range(1, wall.Length).Select(i => new Point(current.X,current.Y-i)));
                    break;
                case Direction.Down:
                    if (current.Y + wall.Length >= height)
                        height = current.Y + wall.Length + 1;

                    map.AddRange(Enumerable.Range(1, wall.Length).Select(i => new Point(current.X,current.Y+i)));
                    break;
                case Direction.Left:
                    if(current.X < wall.Length)
                    {
                        map = map.Select(p => new Point(p.X + wall.Length, p.Y)).ToList();
                        width += wall.Length - current.X;
                        current = new(current.X + wall.Length, current.Y);
                    }

                    map.AddRange(Enumerable.Range(1, wall.Length).Select(i => new Point(current.X-i,current.Y)));
                    break;
                case Direction.Right:
                    if (current.X + wall.Length >= width)
                        width = current.X + wall.Length + 1;

                    map.AddRange(Enumerable.Range(1, wall.Length).Select(i => new Point(current.X+i,current.Y)));
                    break;
            }

            current = map.Last();

            // Print(map,width,height);
        }

        // Print(map,width,height);

        //Dijkstra

        var q = new PriorityQueue<(Point point, int priority),int>(new ReverseComparer<int>());
        q.Enqueue((map[0],0),0);
        var end = map.Last();
        var visited = new List<Point>()
        {
            q.Peek().point
        };
        var total = 0;

        map.Remove(end);
        while (q.Count > 0)
        {
            var c = q.Dequeue();
            if (c.point.Equals(end))
            {
                total = c.priority + 1;
                break;
            }

            var p = c.point;
            if (!map.Contains(new(p.X, p.Y - 1)) && !visited.Contains(new(p.X, p.Y - 1)))
            {
                q.Enqueue((new(p.X, p.Y - 1), c.priority + 1), c.priority + 1);
                visited.Add(new(p.X, p.Y - 1));
            }
            if (!map.Contains(new(p.X, p.Y + 1)) && !visited.Contains(new(p.X, p.Y + 1)))
            {
                q.Enqueue((new(p.X, p.Y + 1), c.priority + 1), c.priority + 1);
                visited.Add(new(p.X, p.Y + 1));
            }
            if (!map.Contains(new(p.X - 1, p.Y)) && !visited.Contains(new(p.X - 1, p.Y)))
            {
                q.Enqueue((new(p.X - 1, p.Y), c.priority + 1), c.priority + 1);
                visited.Add(new(p.X - 1, p.Y));
            }
            if (!map.Contains(new(p.X + 1, p.Y)) && !visited.Contains(new(p.X + 1, p.Y)))
            {
                q.Enqueue((new(p.X + 1, p.Y), c.priority + 1), c.priority + 1);
                visited.Add(new(p.X + 1, p.Y));
            }
        }

        return total.ToString();
    }

    private void Print(List<Point> map, int width, int height)
    {
        var sb = new StringBuilder();
        foreach(var y in Enumerable.Range(0,height))
        {
            var row = "";
            foreach (var x in Enumerable.Range(0, width))
                if (map.Contains(new(x, y)))
                    row += "#";
                else
                    row += ".";
            sb.AppendLine(row);
        }

        Console.WriteLine(sb.ToString());
    }

    public class ReverseComparer<T> : IComparer<T> where T : IComparable<T>
    {
        public int Compare(T x, T y)
        {
            // Reverse the comparison: y.CompareTo(x) instead of x.CompareTo(y)
            return x.CompareTo(y);
        }
    }
}