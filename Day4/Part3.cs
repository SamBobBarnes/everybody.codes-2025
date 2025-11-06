namespace everybody.codes_2025.Day4;

public class Part3() : BasePart(4,3)
{
    public override string Run()
    {
        var input = Input();

        var gearSets = new List<(int A, int B)>();

        foreach (var row in input)
        {
            var gears = row.Split("|");
            if (gears.Length > 1)
                gearSets.Add((int.Parse(gears[0]), int.Parse(gears[1])));
            else
                gearSets.Add((int.Parse(gears[0]), -1));
        }


        decimal finalRatio = 1;

        for (int i = 1; i < gearSets.Count(); i++)
        {
            if (gearSets[i-1].B >= 0)
            {
                finalRatio *= (decimal)gearSets[i - 1].B / gearSets[i].A;
            }
            else
            {
                finalRatio *=(decimal)gearSets[i - 1].A / gearSets[i].A;
            }
        }

        var rotations = 100;

        var finalRotations = rotations * finalRatio;
        return Math.Floor(finalRotations).ToString();
    }
}