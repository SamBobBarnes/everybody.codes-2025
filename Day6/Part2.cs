namespace everybody.codes_2025.Day6;

public class Part2() : BasePart(6,2)
{
    public override string Run()
    {
        var input = InputChars();

        var total = new Dictionary<char,int>
        {
            {'a',0},
            {'b',0},
            {'c',0}
        };
        var mentors = new Dictionary<char,int>
        {
            {'A',0},
            {'B',0},
            {'C',0}
        };

        foreach (var person in input)
        {
            switch (person)
            {
                case 'A':
                case 'B':
                case 'C':
                    mentors[person]++;
                    break;
                case 'a':
                    total['a'] += mentors['A'];
                    break;
                case 'b':
                    total['b'] += mentors['B'];
                    break;
                case 'c':
                    total['c'] += mentors['C'];
                    break;
            }
        }

        return (total['a'] + total['b'] + total['c']).ToString();
    }
}