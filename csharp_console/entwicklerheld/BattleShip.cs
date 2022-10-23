namespace csharp_console.entwicklerheld
{
    internal class BattleShip
    {
        public static bool ValidateBattlefield(int[,] field)
        {
            var ships = new Ships();
            for (var r = 0; r < 10; r++)
            {
                for (var c = 0; c < 10; c++)
                {
                    if (field[r, c] == 0 || ships.CanIgnore((r, c))) continue;
                    var point = new List<(int, int)>() { (r, c) };
                    var boat = FindBoat(point, field, 0);
                    if (!ships.AddBoat(boat)) 
                        return false;
                }
            }
            return ships.AreValid();
        }

        public static (int,int)[] FindBoat(List<(int, int)> results, int[,] field, int index)
        {
            if (index >= results.Count)
                return results.ToArray();

            (int r, int c) = results.ElementAt(index);
            var newValues =
                new (int, int)[] { (r - 1, c), (r + 1, c), (r, c - 1), (r, c + 1) }
                .Where(x => x.Item1 >= 0 && x.Item2 >= 0)
                .Where(x => field[x.Item1, x.Item2] == 1)
                .ToList();

            results.AddRange(newValues);
            results = results.Distinct().ToList();

            return FindBoat(results, field, index + 1);
        }
    }


    public class Ships
    {
        public List<(int, int)[]> four = new();
        public List<(int, int)[]> three = new();
        public List<(int, int)[]> two = new();
        public List<(int, int)[]> one = new();
        public List<(int, int)> points = new();


        public bool AddBoat((int, int)[] boat)
        { 
            if (boat.Length == 1 && one.Count < 4) {
                points.AddRange(boat);
                one.Add(boat);
                return true;
            }
            if (boat.Length == 2 && two.Count < 3)
            {
                points.AddRange(boat);
                two.Add(boat);
                return true;
            }
            if (boat.Length == 3 && three.Count < 2)
            {
                points.AddRange(boat);
                three.Add(boat);
                return true;
            }
            if (boat.Length == 4 && four.Count < 1)
            {
                points.AddRange(boat);
                four.Add(boat);
                return true;
            }
            return false;
        }

        public bool IsLine(List<(int, int)[]> boat) 
        {
            foreach (var b in boat)
            {
                if (!b.All(x => x.Item1 == b[0].Item1)
                    && !b.All(x => x.Item2 == b[0].Item2))
                    return false;
            }
            return true;
        }

        public bool AreValid()
            => four.Count == 1 && IsLine(four)
            && three.Count == 2 && IsLine(three)
            && two.Count == 3 && IsLine(two)
            && one.Count == 4;

        public bool CanIgnore((int r, int c) value) => points.Contains(value);
    }
}
