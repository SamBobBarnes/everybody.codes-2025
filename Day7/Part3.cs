namespace everybody.codes_2025.Day7;

public class Part3() : BasePart(7,3)
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

        var validNames = new List<string>();

        foreach(var name in names)
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
                validNames.Add(name);
            }
        }

        var namesToRemove = new List<string>();

        foreach (var name in validNames)
        {
            foreach (var name2 in validNames)
            {
                if (name == name2) continue;
                if (name.Contains(name2))
                    namesToRemove.Add(name);
            }
        }

        foreach (var name in namesToRemove)
            validNames.Remove(name);

        var total = 0;

        var memoRules = new Dictionary<char, List<string>>();

        foreach (var rule in rules)
        {
            if (rule.Key >= 'A' && rule.Key <= 'Z') continue;

            memoRules.Add(rule.Key, new());
            Recurse(rule.Key,rule.Key.ToString(),rules,memoRules);
            memoRules[rule.Key] = memoRules[rule.Key].Distinct().ToList();
        }

        foreach (var name in validNames)
        {
            var length = name.Length-1;
            var maxLength = 11 - length;
            var minLength = 7 - length;
            var lastChar = name[^1];
            var validEndings = memoRules[lastChar].Where(x => x.Length <= maxLength && x.Length >= minLength);
            total += validEndings.Count();
        }

        return total.ToString();
    }

    private void Recurse(char start, string current, Dictionary<char,List<char>> rules, Dictionary<char, List<string>> memoRules)
    {
        memoRules[start].Add(current);
        var length = current.Length;
        if (length == 9) return;

        rules.TryGetValue(current[length - 1], out var currentRule);
        if (currentRule == null) return;
        foreach (var ruleChar in currentRule)
        {
            Recurse(start,current + ruleChar,rules,memoRules);
        }
    }
}