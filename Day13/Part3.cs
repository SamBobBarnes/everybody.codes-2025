namespace everybody.codes_2025.Day13;

public class Part3() : BasePart(13,3)
{
    public override string Run()
    {
        var input = Input().Select(x => x.Split('-').Select(int.Parse).ToArray());

        var dial = new List<Range> { new(1,1) };

        var clockwise = true;
        var indexOf1 = 0;
        long length = 1;
        foreach (var range in input)
        {
            if(clockwise)
            {
                dial.Add(new(range[0], range[1]));
                length += range[1] - range[0]+1;
            }
            else
            {
                dial.Insert(0, new(range[1], range[0]));
                length += range[1] - range[0]+1;
                indexOf1++;
            }

            clockwise = !clockwise;
        }

        var beginningOfDial = dial[..indexOf1];
        dial = new List<Range>(dial[indexOf1..]);
        dial.AddRange(beginningOfDial);

        // var positionsToTurn = 20252025;
        var positionsToTurn = 202520252025;

        var finalPosition = (positionsToTurn) % length;

        var currentIndex = 0L;
        foreach (var range in dial)
        {
            var previousIndex = currentIndex;
            currentIndex += range.Length;
            if (currentIndex >= finalPosition)
            {
                var index = finalPosition % previousIndex;
                if (range.IsReverse)
                    return (range.Start - index).ToString();
                else
                    return (range.Start + index).ToString();
            }
        }

        return 0.ToString();
    }

    private class Range(int start, int end)
    {
        public int Start => start;
        public int End => end;
        public int Length => Math.Max(Start, End) - Math.Min(Start, End) + 1;
        public bool IsReverse => End < Start;
        public override string ToString()
        {
            return $"{Start}..{End} : {Length}".ToString();
        }
    }
}