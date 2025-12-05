namespace everybody.codes_2025.Day18;

public class Part2() : BasePart(18,2)
{
    public override string Run()
    {
        var input = Input().ToList();

        var nodes = new Dictionary<int, Node>();
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
            nodes[node.Id] = node;

            if(index < 0)
                break;
            input = input[(index + 1)..];
        } while (true);

        var last = nodes.MaxBy(n => n.Key).Value;

        long Recurse(Node node, int[] o)
        {
            var branches = node.Branches;
            var total = 0L;
            foreach(var branch in branches)
            {
                if(branch.End != null)
                {
                    var power = Recurse(branch.End, o);
                    total += power * branch.Thickness;
                }
                else
                {
                    total += o[branch.NodeId - 1];
                }
            }

            return total >= node.Thickness ? total : 0;
        }


        var results = options.Select( o => Recurse(last, o)).ToList();

        return results.Sum().ToString();
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

        public override string ToString()
        {
            return $"{Id}: {Thickness}";
        }
    }

    class Branch(int nodeId, int thickness, Node? end)
    {
        public int NodeId = nodeId;
        public Node? End = end;
        public int Thickness = thickness;

        public override string ToString()
        {
            return $"{NodeId}: {Thickness} - {(End != null ? "n" + End.Id : "free")}";
        }
    }
}