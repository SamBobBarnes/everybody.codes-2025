namespace everybody.codes_2025.Day18;

public class Part1() : BasePart(18,1)
{
    public override string Run()
    {
        var input = Input().ToList();

        var nodes = new Dictionary<int, Node>();
        var index = input.IndexOf("");

        do
        {
            index = input.IndexOf("");
            var lines = index > 0 ? input[0..index] : input;

            var node = new Node(lines.ToArray(), nodes);
            nodes[node.Id] = node;

            if(index < 0)
                break;
            input = input[(index + 1)..];
        } while (index > 0);

        var last = nodes.MaxBy(n => n.Key).Value;

        long Recurse(Node node)
        {
            var branches = node.Branches;
            var total = 0L;
            foreach(var branch in branches)
            {
                if(branch.End != null)
                {
                    var power = Recurse(branch.End);
                    total += power * branch.Thickness;
                }
                else
                {
                    total += 1;
                }
            }

            return total >= node.Thickness ? total : 0;
        }

        var result = Recurse(last);

        return result.ToString();
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
                    Branches.Add(new(thickness, null));
                }
                else
                {
                    var thickness = int.Parse(branchParts[7]);
                    var end = nodes[int.Parse(branchParts[4])];
                    var branch = new Branch(thickness, end);
                    Branches.Add(branch);
                }
            }
        }

        public override string ToString()
        {
            return $"{Id}: {Thickness}";
        }
    }

    class Branch(int thickness, Node? end)
    {
        public Node? End = end;
        public int Thickness = thickness;

        public override string ToString()
        {
            return $"{Thickness} - {(End != null ? "n" + End.Id : "free")}";
        }
    }
}