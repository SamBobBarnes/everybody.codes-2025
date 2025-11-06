namespace everybody.codes_2025.Day3;

public class Part3() : BasePart(3,3)
{
    public override string Run()
    {
        var input = Input()[0].Split(",").Select(int.Parse).ToList();
        input.Sort();
        var q = new Queue<int>(input);
        var leftover = new List<int>();
        var sets = new List<List<int>>{new(){q.Dequeue()}};
        var index = 0;
        var repeat = true;

        while (repeat)
        {
            while (q.Count > 0)
            {
                var box = q.Dequeue();
                if (sets[index].Count > 0 && sets[index].Last() < box)
                    sets[index].Add(box);
                else
                    leftover.Add(box);
            }

            if (leftover.Count > 0)
            {
                q = new Queue<int>(leftover);
                leftover = new List<int>();
                sets.Add([q.Dequeue()]);
                index++;
            }
            else
            {
                repeat = false;
            }
        }

        return sets.Count.ToString();
    }
}