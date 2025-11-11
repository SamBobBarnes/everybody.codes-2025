namespace everybody.codes_2025.Day6;

public class Part1() : BasePart(6,1)
{
    public override string Run()
    {
        var input = InputChars().Where(x => x == 'A' || x == 'a');

        var total = 0;
        var mentors = 0;

        foreach (var person in input)
        {
            if (person == 'A') mentors++;
            if (person == 'a') total += mentors;
        }

        return total.ToString();
    }
}