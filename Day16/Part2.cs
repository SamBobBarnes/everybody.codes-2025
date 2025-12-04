namespace everybody.codes_2025.Day16;

public class Part2() : BasePart(16,2)
{
    public override string Run()
    {
        var wall = Input()[0].Split(",").Select(int.Parse).ToArray();

        var input = new List<long>();

        for (int num = 1; num <= wall.Length; num++)
        {
            if (wall[num - 1] == 0) continue;

            input.Add(num);
            for(int i = num-1; i < wall.Length; i+=num)
            {
                wall[i]--;
            }
        }
        
        // Print(wall);

        return input.Aggregate((a, b) => a*b).ToString();
    }

    private void Print(int[] wall)
    {
        var result = "";
        var max = wall.Max();
        var wallCols = wall.Clone() as int[];
        for (int y = 0; y < max; y++)
        {
            var row = "";
            for (int i = 0; i < wallCols.Length; i++)
            {
                if (wallCols[i] > 0) row += "#";
                else row += " ";
                wallCols[i]--;
            }

            row += "\n";
            result = row + result;
        }
        Console.WriteLine(result);
    }

    private int GCF(int a, int b)
    {
        while (b != 0)
        {
            var remainder = a % b;
            a = b;
            b = remainder;
        }
        return a;
    }
}