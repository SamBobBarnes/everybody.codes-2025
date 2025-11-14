namespace everybody.codes_2025.Day9;

public class Part3() : BasePart(9,3)
{
    public override string Run()
    {
        var ducks = Input().Select(x => (index:x.Split(':')[0],dna:x.Split(':')[1].ToCharArray())).Select(x => (x.index,dna:x.dna.Select(s => s switch
        {
            'A' => Chars.A,
            'C' => Chars.C,
            'G' => Chars.G,
            'T' => Chars.T,
            _ => throw new IndexOutOfRangeException()
        }).ToArray())).Select(s => new Duck(s.index,s.dna)).ToList();

        var pairs = new List<ParentPair>();

        for (int i = 0; i < ducks.Count-1; i++)
        {
            for (int j = i + 1; j < ducks.Count; j++)
                pairs.Add(new(ducks[i], ducks[j]));
        }

        foreach (var pair in pairs)
        {
            foreach (var duck in ducks)
            {
                if (duck.TestForParents(pair))
                {
                    duck.SetParents(pair);
                }
            }
        }

        var children = ducks.Where(d => d.Children.Count == 0);
        var max = 0;
        var maxMembers = 0;
        var seen = new List<int>();
        foreach (var child in children)
        {
            var total = 0;
            var totalMembers = 0;
            var q = new Queue<Duck>();
            q.Enqueue(child);

            while (q.Count > 0)
            {
                var current = q.Dequeue();

                if (seen.Contains(current.Index)) continue;
                seen.Add(current.Index);
                total += current.Index;
                totalMembers += 1;

                if(current.ParentA != null) q.Enqueue(current.ParentA);
                if(current.ParentB != null) q.Enqueue(current.ParentB);
                foreach(var subChild in current.Children)
                    q.Enqueue(subChild);
            }

            if (totalMembers > maxMembers)
            {
                max = total;
                maxMembers = totalMembers;
            }
        }


        return max.ToString();
    }

    private class Duck(string index, Chars[] dna)
    {
        public int Index => int.Parse(index);
        public Chars[] Dna => dna;
        public Duck ParentA { get; set; }
        public Duck ParentB { get; set; }
        public List<Duck> Children = new List<Duck>();

        public void SetParents(ParentPair pair)
        {
            ParentA = pair.A;
            ParentB = pair.B;

            ParentA.Children.Add(this);
            ParentB.Children.Add(this);
        }

        public bool TestForParents(ParentPair p)
        {
            if (p.A.Dna == dna || p.B.Dna == dna)
            {
                return false;
            }
            for (int i = 0; i < dna.Length; i++)
            {
                if (dna[i] != p.A.Dna[i] && dna[i] != p.B.Dna[i])
                    return false;
            }

            return true;
        }

        public override string ToString()
        {
            return index;
        }
    }

    private record ParentPair(Duck A, Duck B);

    private enum Chars
    {
        A,
        C,
        G,
        T
    }
}