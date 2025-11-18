namespace everybody.codes_2025.Day11;

public class Part2() : BasePart(11,2)
{
    public override string Run()
    {
        var columns = Input().Select(int.Parse).ToList();

        //First round
        var columnsChanged = -1;
        var executedRounds = 0;

        while (columnsChanged != 0)
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
        executedRounds--;

        columnsChanged = -1;

        while (columnsChanged != 0)
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
        executedRounds--;

        return executedRounds.ToString();
    }
}