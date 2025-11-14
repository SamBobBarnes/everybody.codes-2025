namespace everybody.codes_2025.Day9;

public class Part2() : BasePart(9,2)
{
    public override string Run()
    {
        var input = Input().Select(x => x.Split(':')[1].ToCharArray()).ToList();

        var scaleLength = input[0].Length;
        List<Chars[]> scalesList = input.Select(x => x.Select(s => s switch
        {
            'A' => Chars.A,
            'C' => Chars.C,
            'G' => Chars.G,
            'T' => Chars.T,
            _ => throw new IndexOutOfRangeException()
        }).ToArray()).ToList();

        var pairs = new List<ParentPair>();

        for (int i = 0; i < scalesList.Count-1; i++)
        {
            for (int j = i + 1; j < scalesList.Count; j++)
                pairs.Add(new(scalesList[i], scalesList[j]));
        }

        var total = 0;

        foreach (var pair in pairs)
        {
            foreach (var scales in scalesList)
            {
                if (TestForParents(pair, scales))
                {
                    total += GetMatchCount(pair.A, scales) * GetMatchCount(pair.B, scales);
                }
            }
        }

        return total.ToString();
    }

    private record ParentPair(Chars[] A, Chars[] B);

    private bool TestForParents(ParentPair p, Chars[] child)
    {
        if (p.A == child || p.B == child)
        {
            return false;
        }
        for (int i = 0; i < child.Length; i++)
        {
            if (child[i] != p.A[i] && child[i] != p.B[i])
                return false;
        }

        return true;
    }

    private int GetMatchCount(Chars[] a, Chars[] b)
    {
        var total = 0;
        for (int j = 0; j < a.Length; j++)
        {
            if (a[j] == b[j]) total++;
        }

        return total;
    }

    private enum Chars
    {
        A,
        C,
        G,
        T
    }
}