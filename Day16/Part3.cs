namespace everybody.codes_2025.Day16;

public class Part3() : BasePart(16,3)
{
    public override string Run()
    {
        var inputWall = Input()[0].Split(",").Select(int.Parse).ToArray();

        var input = new List<long>();

        for (int num = 1; num <= inputWall.Length; num++)
        {
            if (inputWall[num - 1] == 0) continue;

            input.Add(num);
            for(int i = num-1; i < inputWall.Length; i+=num)
            {
                inputWall[i]--;
            }
        }

        UInt128 availableBlocks = 202520252025000;

        var lowerBound = 0L;
        var upperBound = long.MaxValue;
        UInt128 currentBlocks = 0;
        while (lowerBound < upperBound)
        {
            if(lowerBound +1 == upperBound)
            {
                return GetBlocksAtColumn(input, (UInt128)lowerBound) <= availableBlocks
                    ? lowerBound.ToString()
                    : (lowerBound - 1).ToString();
            }
            var mid = lowerBound + (upperBound-lowerBound) / 2 ;
            currentBlocks = GetBlocksAtColumn(input, (UInt128)mid);
            if (currentBlocks < availableBlocks)
            {
                lowerBound = mid + 1;
            }
            else
            {
                upperBound = mid;
            }
        }

        return 0.ToString();
    }

    private UInt128 GetBlocksAtColumn(List<long> input, UInt128 column)
    {
        UInt128 result = 0;
        foreach(var spell in input)
        {
            result += column / (UInt128)spell;
        }

        return result;
    }
}