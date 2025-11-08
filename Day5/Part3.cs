using System.Text;

namespace everybody.codes_2025.Day5;

public class Part3() : BasePart(5,3)
{
    public override string Run()
    {
        var input = Input().Select(x => (Id:x.Split(":")[0], Bones:x.Split(":")[1].Split(",").Select(int.Parse).ToArray()));

        var fishbones = new List<Fishbone>();
        foreach (var row in input)
        {
            fishbones.Add(new(row.Id, row.Bones));
        }

        var comparer = new FishboneComparer();
        fishbones.Sort(comparer);
        fishbones.Reverse();

        var checksum = fishbones.Select((x, index) => x.Id * (index + 1)).Sum();

        return checksum.ToString();
    }

    private class FishboneRib(int spine)
    {
        public int Spine { get; set; } = spine;
        public int? LeftRib { get; set; }
        public int? RightRib { get; set; }

        public int Value()
        {
            var sb = new StringBuilder();

            if (LeftRib != null)
            {
                sb.Append(LeftRib);
            }
            sb.Append(Spine);
            if (RightRib != null)
            {
                sb.Append(RightRib);
            }

            return int.Parse(sb.ToString());
        }

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
        public int Id { get; }
        public int[] Rows => _ribs.Select(x => x.Value()).ToArray();

        public Fishbone(string id, int[] bones)
        {
            Id = int.Parse(id);
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
            var value = new StringBuilder($"{Id}: ");
            foreach (var rib in _ribs)
            {
                value.Append(rib.Spine);
            }

            return value.ToString();
        }
    }

    private class FishboneComparer : IComparer<Fishbone>
    {
        public int Compare(Fishbone? x, Fishbone? y)
        {
            if (x == null) return -1;
            if (y == null) return 1;
            if (x.Value() > y.Value()) return 1;
            if (x.Value() < y.Value()) return -1;
            if (x.Value() != y.Value()) throw new ArgumentOutOfRangeException();
            var aRows = x.Rows;
            var bRows = y.Rows;

            for (int i = 0; i < aRows.Length; i++)
            {
                if (aRows[i] == bRows[i]) continue;
                if (aRows[i] > bRows[i]) return 1;
                if (aRows[i] < bRows[i]) return -1;
            }

            if (x.Id > y.Id) return 1;
            return -1;
        }
    }
}