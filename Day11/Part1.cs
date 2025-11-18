namespace everybody.codes_2025.Day11;

public class Part1() : BasePart(11,1)
{
    public override string Run()
    {
        var columns = Input().Select(int.Parse).ToList();

        //First round
        var columnsChanged = -1;
        var allowedRounds = 10;
        var executedRounds = 0;

        while (executedRounds <= allowedRounds && columnsChanged != 0)
        {
            columnsChanged = 0;
            for (int i = 0; i < columns.Count - 1; i++)
            {
                if (columns[i] > columns[i + 1])
                {
                    columns[i]--;
                    columns[i+1]++;
                    columnsChanged++;
                }
            }

            executedRounds++;
        }

        columnsChanged = -1;

        while (executedRounds <= allowedRounds && columnsChanged != 0)
        {
            columnsChanged = 0;
            for (int i = 0; i < columns.Count - 1; i++)
            {
                if (columns[i] < columns[i + 1])
                {
                    columns[i]++;
                    columns[i+1]--;
                    columnsChanged++;
                }
            }

            executedRounds++;
        }

        return CalculateChecksum(columns).ToString();
    }

    private int CalculateChecksum(List<int> columns)
    {
        return columns.Select((c, i) => c * (i + 1)).Sum();
    }
}