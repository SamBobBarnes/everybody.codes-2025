namespace everybody.codes_2025.Day13;

public class Part1() : BasePart(13,1)
{
    public override string Run()
    {
        var input = Input().Select(int.Parse).ToList();

        var dial = new List<int> { 1 };

        var clockwise = true;
        var indexOf1 = 0;
        foreach (var num in input)
        {
            if(clockwise)
                dial.Add(num);
            else
            {
                dial.Insert(0, num);
                indexOf1++;
            }

            clockwise = !clockwise;
        }

        var positions = dial.Count;
        var positionsToTurn = 2025;

        var finalPosition = (positionsToTurn + indexOf1) % positions;


        return dial[finalPosition].ToString();
    }
}