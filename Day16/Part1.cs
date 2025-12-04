namespace everybody.codes_2025.Day16;

public class Part1() : BasePart(16,1)
{
    public override string Run()
    {
        var input = Input()[0].Split(",").Select(int.Parse).ToArray();

        var wallLength = 90;
        var wall = new int[wallLength];

        foreach (var num in input)
            for (int i = num-1; i < wallLength; i += num)
                wall[i]++;
        
        Print(wall);

        return wall.Sum().ToString();
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
}