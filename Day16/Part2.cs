namespace everybody.codes_2025.Day16;

public class Part2() : BasePart(16,2,true)
{
    public override string Run()
    {
        var wall = Input()[0].Split(",").Select(int.Parse).ToArray();

        var input = new List<int>();
        
        var tempWall = new bool[wall.Length];

        for (int i = 1; i <= wall.Max(); i++)
        {
            for (int j = 0; j < wall.Length; j++)
            {
                if (wall[j] >= i) tempWall[j] = true;            
            }

            while (tempWall.Sum(x => x ? 1 : 0) > 0) // finish wall
            {
                var max = 0;
                var maxNum = 0;
                for (int j = wall.Length; j > 0; j--) // foreach num upto length of wall
                {
                    var localMax = 0;
                    for (int k = j - 1; k > wall.Length; k += j) // foreach column 
                        if (tempWall[k])
                            localMax++;
                        else
                            break;
                    if (localMax > max)
                    {
                        max = localMax;
                        maxNum = j;
                    }
                }

                // max found
                if(maxNum > 0)
                {
                    input.Add(maxNum);
                    for (int k = maxNum - 1; k < wall.Length; k += maxNum)
                        tempWall[k] = false; // remove blocks of this num and continue
                }            
            }
        }
        
        
        Print(wall);

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