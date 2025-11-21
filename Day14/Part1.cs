using System.Text;

namespace everybody.codes_2025.Day14;

public class Part1() : BasePart(14,1)
{
    public override string Run()
    {
        var input = Input().Select(x => x.ToCharArray().Select(y => y == '#').ToArray()).ToArray();
        var width = input[0].Length;
        var height = input.Length;

        var tiles = new bool[height, width];

        for (int y = 0; y < height; y++)
        for (int x = 0; x < width; x++)
            tiles[y, x] = input[y][x];

        var rounds = 10;
        var total = 0;

        // Print(tiles, width, height);

        for (int i = 0; i < rounds; i++)
        {
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
                total++;
            }

            tiles = newTiles;
            // Print(tiles, width, height);
        }

        return total.ToString();
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