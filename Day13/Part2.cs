namespace everybody.codes_2025.Day13;

public class Part2() : BasePart(13,2)
{
    public override string Run()
    {
        var input = Input().Select(x => x.Split('-').Select(int.Parse).ToArray());

        var dial = new List<int> { 1 };

        var clockwise = true;
        var indexOf1 = 0;
        foreach (var range in input)
        {
            if(clockwise)
                dial.AddRange(Enumerable.Range(range[0],range[1]-range[0]+1));
            else
            {
                foreach(var num in Enumerable.Range(range[0],range[1]-range[0]+1))
                {
                    dial.Insert(0, num);
                    indexOf1++;
                }
            }

            clockwise = !clockwise;
        }

        var positions = dial.Count;
        var positionsToTurn = 20252025;

        var finalPosition = (positionsToTurn + indexOf1) % positions;


        return dial[finalPosition].ToString();
    }
}