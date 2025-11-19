namespace everybody.codes_2025.Day12;

public class Part1() : BasePart(12,1)
{
    public override string Run()
    {
        var input = Input().Select(x => x.ToCharArray().Select(c => $"{c}").Select(int.Parse).ToArray()).ToArray();
        var height = input.Length;
        var width = input[0].Length;

        var total = 0;
        var q = new Queue<Point>();
        var visited = new List<Point>();
        q.Enqueue(new(0,0));

        while (q.Count > 0)
        {
            var current = q.Dequeue();
            var value = input[current.Y][current.X];
            if (visited.Contains(current)) continue;

            visited.Add(current);
            total++;

            var neighbors = new List<Point>();

            if (current.Y > 0 && input[current.Y - 1][current.X] <= value && !visited.Contains(new(current.X, current.Y - 1)))
                neighbors.Add(new(current.X, current.Y - 1));
            if (current.Y < height-1 && input[current.Y + 1][current.X] <= value && !visited.Contains(new(current.X, current.Y + 1)))
                neighbors.Add(new(current.X, current.Y + 1));
            if (current.X > 0 && input[current.Y][current.X-1] <= value && !visited.Contains(new(current.X-1, current.Y)))
                neighbors.Add(new(current.X-1, current.Y));
            if (current.X < width-1 && input[current.Y][current.X+1] <= value && !visited.Contains(new(current.X+1, current.Y)))
                neighbors.Add(new(current.X+1, current.Y));

            foreach(var neighbor in neighbors)
                q.Enqueue(neighbor);
        }

        return total.ToString();
    }
}