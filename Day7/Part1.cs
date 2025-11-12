namespace everybody.codes_2025.Day7;

public class Part1() : BasePart(7,1)
{
    public override string Run()
    {
        var input = Input();

        var names = input[0].Split(',');

        var rules = input[2..].Select(x =>
        {
            var initial = x[0];
            var next = x[4..].Split(',').Select(y=>y[0]).ToList();

            return (Current: initial, Next: next);
        }).ToDictionary();

        var validName = "";

        foreach (var name in names)
        {
            var valid = true;
            for (int i = 0; i < name.Length - 1; i++)
            {
                var rule = rules[name[i]];
                if (!rule.Contains(name[i + 1]))
                {
                    valid = false;
                    break;
                }
            }

            if (valid)
            {
                validName = name;
                break;
            }
        }

        return validName;
    }
}