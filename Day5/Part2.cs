using System.Text;

namespace everybody.codes_2025.Day5;

public class Part2() : BasePart(5,2)
{
    public override string Run()
    {
        var input = Input().Select(x => (Id:x.Split(":")[0], Bones:x.Split(":")[1].Split(",").Select(int.Parse).ToArray()));

        var fishbones = new List<Fishbone>();
        foreach (var row in input)
        {
            fishbones.Add(new(row.Id, row.Bones));
        }

        // foreach (var fishbone in fishbones)
        // {
        //     Console.WriteLine(fishbone.ToString());
        // }

        var max = fishbones.Select(x => x.Value()).Max();
        var min = fishbones.Select(x => x.Value()).Min();

        return (max - min).ToString();
    }

    private class FishboneRib(int spine)
    {
        public int Spine { get; set; } = spine;
        public int? LeftRib { get; set; }
        public int? RightRib { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (LeftRib != null)
            {
                sb.Append(LeftRib);
                sb.Append("-");
            }
            else
            {
                sb.Append("  ");
            }

            sb.Append(Spine);
            if (RightRib != null)
            {
                sb.Append("-");
                sb.Append(RightRib);
            }
            else
            {
                sb.Append("  ");
            }

            return sb.ToString();
        }
    }

    private class Fishbone
    {
        private List<FishboneRib> _ribs = [];
        private string _id;

        public Fishbone(string id, int[] bones)
        {
            _id = id;
            foreach (var bone in bones)
            {
                AddBone(bone);
            }
        }

        public void AddBone(int bone)
        {
            if(_ribs.Count == 0)
            {
                _ribs.Add(new(bone));
                return;
            }

            var placed = false;

            for(int i = 0; i < _ribs.Count; i++)
            {
                var current = _ribs[i];
                if (bone < current.Spine && current.LeftRib == null)
                {
                    current.LeftRib = bone;
                    placed = true;
                }
                else if (bone > current.Spine && current.RightRib == null)
                {
                    current.RightRib = bone;
                    placed = true;
                }

                if (placed) break;
            }

            if (!placed)
            {
                _ribs.Add(new(bone));
            }
        }

        public long Value() {
            var value = new StringBuilder();
            foreach (var rib in _ribs)
            {
                value.Append(rib.Spine);
            }

            return long.Parse(value.ToString());
        }

        public override string ToString()
        {
            // var sb = new StringBuilder();
            var value = new StringBuilder($"{_id}: ");
            foreach (var rib in _ribs)
            {
                // sb.AppendLine(rib.ToString());
                // sb.AppendLine("  |  ");
                value.Append(rib.Spine);
            }

            // sb.AppendLine();
            // sb.AppendLine(value.ToString());

            return value.ToString();
        }
    }
}