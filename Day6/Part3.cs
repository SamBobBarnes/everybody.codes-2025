namespace everybody.codes_2025.Day6;

public class Part3() : BasePart(6,3)
{
    public override string Run()
    {
        var input = InputChars();

        var mentorIndices = new Dictionary<char, List<int>>
        {
            {'A',new()},
            {'B',new()},
            {'C',new()}
        };
        var noviceIndices = new Dictionary<char, List<int>>
        {
            {'a',new()},
            {'b',new()},
            {'c',new()}
        };

        for (int i = 0; i < input.Length; i++)
        {
            switch (input[i])
            {
                case 'A':
                    mentorIndices['A'].Add(i);
                    break;
                case 'B':
                    mentorIndices['B'].Add(i);
                    break;
                case 'C':
                    mentorIndices['C'].Add(i);
                    break;
                case 'a':
                    noviceIndices['a'].Add(i);
                    break;
                case 'b':
                    noviceIndices['b'].Add(i);
                    break;
                case 'c':
                    noviceIndices['c'].Add(i);
                    break;
            }
        }

        var total = 0;
        var repetitions = 1000;
        var distanceLimit = 1000;
        var length = input.Length;
        
        var noviceMentorPairs = new List<(char Novice, char Mentor)>
        {
            ('a','A'),
            ('b','B'),
            ('c','C'),
        };
         
        foreach(var pair in noviceMentorPairs)
        {
            var localTotal = 0;
            var startList = mentorIndices[pair.Mentor].Select(x => x -= length).ToList();
            var endList = mentorIndices[pair.Mentor].Select(x => x += length).ToList();
            var fullList = new List<int>();
            fullList.AddRange(startList);
            fullList.AddRange(endList);
            fullList.AddRange(mentorIndices[pair.Mentor]);
            foreach (var noviceIndex in noviceIndices[pair.Novice])
            {
                localTotal += fullList.Count(x => x >= noviceIndex - distanceLimit && x <= noviceIndex + distanceLimit);
            }

            localTotal *= repetitions-2;
            var fullStartList = new List<int>();
            fullStartList.AddRange(startList);
            fullStartList.AddRange(mentorIndices[pair.Mentor]);
            var fullEndList = new List<int>();
            fullEndList.AddRange(endList);
            fullEndList.AddRange(mentorIndices[pair.Mentor]);
            foreach (var noviceIndex in noviceIndices[pair.Novice])
            {
                localTotal += fullStartList.Count(x => x >= noviceIndex - distanceLimit && x <= noviceIndex + distanceLimit);
            }
            foreach (var noviceIndex in noviceIndices[pair.Novice])
            {
                localTotal += fullEndList.Count(x => x >= noviceIndex - distanceLimit && x <= noviceIndex + distanceLimit);
            }

            total += localTotal;
        }


        return total.ToString();
    }
}