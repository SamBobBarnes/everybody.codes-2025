namespace everybody.codes_2025.Day18;

public class Part3() : BasePart(18,3)
{
    public override string Run()
    {
        var input = Input().ToList();

        var nodes = new Dictionary<int, Node>();
        var negativeBranches = new bool[81];
        for(int i = 0; i < 81; i++)
        {
            negativeBranches[i] = true;
        }
        var options = new List<int[]>();
        var index = input.IndexOf("");

        do
        {
            index = input.IndexOf("");
            if(index == 0)
            {
                input = input[1..];

                options.AddRange(input.Select(x => x.Split(' ').Select(int.Parse).ToArray()).ToList());

                break;
            }

            var lines = index > 0 ? input[0..index] : input;

            var node = new Node(lines.ToArray(), nodes);
            if (node.Branches.Any(b => b.End is { Id: <= 81 }))
            {
                foreach(var branch in node.Branches)
                {
                    if(branch.End is { Id: <= 81 } && branch.Thickness < 0)
                    {
                        negativeBranches[branch.End.Id - 1] = false;
                    }
                }
            }
            nodes[node.Id] = node;

            if(index < 0)
                break;
            input = input[(index + 1)..];
        } while (true);

        var last = nodes.MaxBy(n => n.Key).Value;

        var maxValue = last.GetPower(negativeBranches.Select(b => b ? 1 : 0).ToArray());

        var results = options.Select( o => last.GetPower(o)).Where(x => x > 0).ToList();

        return results.Sum(x => maxValue - x).ToString();
    }

    class Node
    {
        public readonly int Id;
        public readonly int Thickness;
        public readonly List<Branch> Branches = new();
        public Node(string[] values, Dictionary<int, Node> nodes)
        {
            var parts = values[0].Split(' ');
            Id = int.Parse(parts[1]);
            Thickness = int.Parse(parts[4].TrimEnd(':'));

            foreach(var line in values[1..])
            {
                var branchParts = line.Split(' ');
                if (branchParts[1] == "free")
                {
                    var thickness = int.Parse(branchParts[5]);
                    Branches.Add(new(Id, thickness, null));
                }
                else
                {
                    var thickness = int.Parse(branchParts[7]);
                    var end = nodes[int.Parse(branchParts[4])];
                    var branch = new Branch(Id, thickness, end);
                    Branches.Add(branch);
                }
            }
        }

        public long GetPower(int[] o)
        {
            var total = 0L;
            foreach(var branch in Branches)
            {
                total += branch.GetPower(o);
            }

            return total >= Thickness ? total : 0;
        }

        public override string ToString()
        {
            return $"{Id}: {Thickness}";
        }
    }

    class Branch(int nodeId, int thickness, Node? end)
    {
        public readonly int NodeId = nodeId;
        public readonly Node? End = end;
        public readonly int Thickness = thickness;


        public long GetPower(int[] o)
        {
            if(End != null)
            {
                var power = End.GetPower(o);
                return power * Thickness;
            }
            return o[NodeId - 1] * Thickness;
        }

        public override string ToString()
        {
            return $"{NodeId}: {Thickness} - {(End != null ? "n" + End.Id : "free")}";
        }
    }
}