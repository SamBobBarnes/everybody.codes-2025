using System.Text;

namespace everybody.codes_2025.Day5;

public class Part1() : BasePart(5,1)
{
    public override string Run()
    {
        var input = Input()[0].Split(":")[1].Split(",").Select(int.Parse);

        var fishbone = new Fishbone();
        foreach (var num in input)
        {
            fishbone.AddBone(num);
        }

        return fishbone.ToString();
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

        public override string ToString()
        {
            var sb = new StringBuilder();
            var value = new StringBuilder();
            foreach (var rib in _ribs)
            {
                sb.AppendLine(rib.ToString());
                sb.AppendLine("  |  ");
                value.Append(rib.Spine);
            }

            sb.AppendLine();
            sb.AppendLine(value.ToString());

            return sb.ToString();
        }
    }
}