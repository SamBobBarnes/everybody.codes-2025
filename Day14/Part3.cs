using System.Text;

namespace everybody.codes_2025.Day14;

public class Part3() : BasePart(14,3)
{
    public override string Run()
    {
        var input = Input().Select(x => x.ToCharArray().Select(y => y == '#').ToArray()).ToArray();
        var width = 34;
        var height = 34;

        var tilesToMatch = new bool[input.Length, input[0].Length];

        for (int y = 0; y < input.Length; y++)
        for (int x = 0; x < input[0].Length; x++)
            tilesToMatch[y, x] = input[y][x];

        var history = new List<bool[,]>();

        var tiles = new bool[34,34];

        var rounds = 1000000000;
        var totals = new List<(int round, int total)>();
        var repetitionIndex = -1;
        var currentIndex = -1;

        // Print(tiles, width, height);

        for (int i = 0; i < rounds; i++)
        {
            history.Add(tiles);
            var localTotal = 0;
            var newTiles = new bool[height, width];

            for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
            {
                var neighbors = new []
                {
                    y != 0 && x != 0 && tiles[y-1,x-1],
                    y != 0 && x != width - 1 && tiles[y - 1,x + 1],
                    y != height-1 && x != width - 1 && tiles[y + 1,x + 1],
                    y != height-1 && x != 0 && tiles[y + 1,x - 1],
                };
                var sum = neighbors.Sum(b => b ? 1 : 0);

                if ((sum % 2 != 0 || tiles[y, x]) && (sum % 2 == 0 || !tiles[y, x])) continue; // even inactive || odd active
                newTiles[y, x] = true;
                localTotal++;
            }

            tiles = newTiles;
            if(IsMatch(tiles, tilesToMatch,input[0].Length, input.Length))
                totals.Add((i+1, localTotal));

            var match = false;
            var index = 0;
            while (!match && index < history.Count)
            {
                var previous = history[index];
                match = IsMatch(tiles, previous);
                index++;
            }

            if (match)
            {
                currentIndex = i;
                repetitionIndex = index - 1;
                break;
            }
            // Print(tiles, width, height);
        }

        var cycleLength = currentIndex - repetitionIndex+1;
        var remainingRounds = (rounds - repetitionIndex) / cycleLength;
        var totalRounds = remainingRounds * cycleLength;
        var remainderRounds = rounds - totalRounds;
        var totalsInCycle = totals.Where(x => x.round > repetitionIndex).ToList();
        var totalsInRemainder = totalsInCycle.Where(x => x.round <= remainderRounds).ToList();
        var total = totalsInCycle.Sum(x => x.total) * (remainingRounds) + totalsInRemainder.Sum(x => x.total);

        return total.ToString();
    }

    private bool IsMatch(bool[,] tiles, bool[,] match,int width = 34, int height = 34)
    {
        var startX = (34 - width)/2;
        var startY = (34 - height)/2;

        for (int y = 0; y < width; y++)
        for (int x = 0; x < height; x++)
        {
            if (tiles[startY + y, startX + x] != match[y,x]) return false;
        }

        return true;
    }

    private void Print(bool[,] tiles, int width, int height)
    {
        var sb = new StringBuilder();
        for (int y = 0; y < height; y++)
        {
            var row = "";
            for (int x = 0; x < width; x++)
            {
                row += tiles[y, x] ? '#' : '.';
            }

            sb.AppendLine(row);
        }

        Console.WriteLine(sb.ToString());
    }
}