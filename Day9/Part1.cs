namespace everybody.codes_2025.Day9;

public class Part1() : BasePart(9,1)
{
    public override string Run()
    {
        var input = Input().Select(x => x.Split(':')[1].ToCharArray()).ToList();

        var a = 0;
        var b = 0;
        for (int i = 0; i < input[0].Length; i++)
        {
            if (input[2][i] == input[0][i]) a++;
            if (input[2][i] == input[1][i]) b++;
        }

        return (a*b).ToString();
    }
}