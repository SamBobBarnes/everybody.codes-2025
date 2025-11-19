using System.Diagnostics;

namespace everybody.codes_2025.Day12;

public class Part3() : BasePart(12,3)
{
    public override string Run()
    {
        var input = Input().Select(x => x.ToCharArray().Select(c => $"{c}").Select(int.Parse).ToArray()).ToArray();
        var height = input.Length;
        var width = input[0].Length;

        var largest = 0;
        var largestBarrels = new List<Point>();
        for(int y = 0; y < height; y++)
            for(int x = 0; x < width; x++)
                if(input[y][x] > largest)
                    largest = input[y][x];

        for(int i = 1; i <= largest; i++)
        for(int y = 0; y < height; y++)
        for(int x = 0; x < width; x++)
            if(input[y][x] == i)
                largestBarrels.Add(new Point(x,y));


        var totals = new List<(int Value, Point Start, List<Point> Visited, int StartValue)>();
        var timer = new Stopwatch();
        timer.Start();
        foreach (var barrel in largestBarrels)
        {
            if (totals.Count % 2000 == 0)
            {
                Console.WriteLine($"Processing barrel {totals.Count} / {largestBarrels.Count}, Time elapsed: {timer.Elapsed}");
                timer.Restart();
            }
            var total = FindBarrelsToExplode(barrel, input, height, width,totals,new List<Point>(), out var visited);

            totals.Add((total, barrel, visited, input[barrel.Y][barrel.X]));
        }

        totals.Sort((a,b) => b.Value.CompareTo(a.Value));
        totals = [totals[0]];

        largestBarrels = largestBarrels.Where(b => !totals[0].Visited.Contains(b)).ToList();

        foreach (var barrel in largestBarrels)
        {
            if (totals.Count % 2000 == 0)
            {
                Console.WriteLine($"Processing barrel {totals.Count} / {largestBarrels.Count}, Time elapsed: {timer.Elapsed}");
                timer.Restart();
            }
            var total = FindBarrelsToExplode(barrel, input, height, width,totals,totals[0].Visited, out var visited);

            totals.Add((total, barrel, visited, input[barrel.Y][barrel.X]));
        }

        totals.Sort((a,b) => b.Value.CompareTo(a.Value));
        totals = [totals[0], totals[1]];

        largestBarrels = largestBarrels.Where(b => !totals[1].Visited.Contains(b)).ToList();

        foreach (var barrel in largestBarrels)
        {
            if (totals.Count % 2000 == 0)
            {
                Console.WriteLine($"Processing barrel {totals.Count} / {largestBarrels.Count}, Time elapsed: {timer.Elapsed}");
                timer.Restart();
            }
            var total = FindBarrelsToExplode(barrel, input, height, width,totals,totals[1].Visited, out var visited);

            totals.Add((total, barrel, visited, input[barrel.Y][barrel.X]));
        }

        totals.Sort((a,b) => b.Value.CompareTo(a.Value));

        return totals[..3].Sum(x=> x.Value).ToString();
    }

    private static int FindBarrelsToExplode(Point barrel, int[][] input, int height, int width, List<(int Value, Point Start, List<Point> Visited, int StartValue)> totals, List<Point> previouslyVisited, out List<Point> visited)
    {
        var total = 0;
        var q = new Queue<Point>();
        visited = new List<Point>(previouslyVisited);
        q.Enqueue(barrel);

        while (q.Count > 0)
        {
            var current = q.Dequeue();
            var value = input[current.Y][current.X];
            if (visited.Contains(current)) continue;

            if (totals.Any(t => t.Start.Equals(current)))
            {
                var currentTotal = totals.First(t => t.Start.Equals(current));
                var list = visited;
                var visitedToAdd = currentTotal.Visited.Where(v => !list.Contains(v)).ToList();
                visited.AddRange(visitedToAdd);
                total += visitedToAdd.Count;
                continue;
            }

            visited.Add(current);
            total++;

            var neighbors = new List<Point>();

            if (current.Y > 0 && input[current.Y - 1][current.X] <= value &&
                !visited.Contains(new(current.X, current.Y - 1)))
                neighbors.Add(new(current.X, current.Y - 1));
            if (current.Y < height - 1 && input[current.Y + 1][current.X] <= value &&
                !visited.Contains(new(current.X, current.Y + 1)))
                neighbors.Add(new(current.X, current.Y + 1));
            if (current.X > 0 && input[current.Y][current.X - 1] <= value &&
                !visited.Contains(new(current.X - 1, current.Y)))
                neighbors.Add(new(current.X - 1, current.Y));
            if (current.X < width - 1 && input[current.Y][current.X + 1] <= value &&
                !visited.Contains(new(current.X + 1, current.Y)))
                neighbors.Add(new(current.X + 1, current.Y));

            foreach (var neighbor in neighbors)
                q.Enqueue(neighbor);
        }

        return total;
    }
}