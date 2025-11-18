namespace everybody.codes_2025.Day11;

public class Part3() : BasePart(11,3)
{
    public override string Run()
    {
        var columns = Input().Select(long.Parse).ToList();

        var executedRounds = 0L;
        var total = columns.Sum();
        var average = total / columns.Count;

        foreach (var column in columns)
        {
            if(column < average)
                executedRounds += Math.Abs(column - average);
        }

        return executedRounds.ToString();
    }
}