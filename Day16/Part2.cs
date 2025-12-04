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
}