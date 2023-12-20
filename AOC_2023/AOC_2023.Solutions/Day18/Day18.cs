namespace AOC_2023.Solutions;

public class Day18
{
    private record Point
    {
        public long X { get; set; }
        public long Y { get; set; }
    }

    public long Part1(string filename)
    {
        var commands = File.ReadAllLines(filename);
        var points = new List<Point>();
        var current = new Point();
        points.Add(current);

        var result = 1;
        foreach (var command in commands)
        {
            var bits = command.Split(' ');
            var dir = bits[0][0];
            var qty = int.Parse(bits[1]);

            current = dir switch
            {
                'R' => current with { X = current.X + qty },
                'L' => current with { X = current.X - qty },
                'D' => current with { Y = current.Y + qty },
                'U' => current with { Y = current.Y - qty },
                _ => current
            };
            points.Add(current);

            // Offset because we need to take the border into account.
            // Can also use Picks for this.
            result += dir switch
            {
                'L' => qty,
                'U' => qty,
                _ => 0
            };
        }

        var N = points.Count - 1;
        var r1 = points[N].X * points[0].Y;
        for (var n = 0; n < N; n++)
            r1 += points[n].X * points[n + 1].Y;

        var r2 = points[0].X * points[N].Y;
        for (var n = 0; n < N; n++)
            r2 += points[n + 1].X * points[n].Y;

        return result + (Math.Abs(r1 - r2) / 2);
    }

    public double Part2(string filename)
    {
        var commands = File.ReadAllLines(filename);
        var points = new List<Point>();
        var current = new Point();
        points.Add(current);

        double result = 1L;
        foreach (var command in commands)
        {
            // R 6 (#70c710)
            var bits = command.Split(' ')[^1]; // (#70c710)
            var dirC = bits[7];
            var qty = Convert.ToInt32($"0x{bits[2..7]}", 16);

            var dir = dirC switch
            {
                '0' => 'R',
                '1' => 'D',
                '2' => 'L',
                _ => 'U'
            };

            current = dir switch
            {
                'R' => current with { X = current.X + qty },
                'L' => current with { X = current.X - qty },
                'D' => current with { Y = current.Y + qty },
                'U' => current with { Y = current.Y - qty },
                _ => current
            };
            points.Add(current);

            // Offset because we need to take the border into account.
            // Can also use Picks for this.
            result += dir switch
            {
                'L' => qty,
                'U' => qty,
                _ => 0
            };
        }

        int n = points.Count - 1;

        double a = 0;
        for (int i = 0; i < n; i++)
        {
            a += ((points[i].X * points[i + 1].Y) - (points[i + 1].X * points[i].Y)) / 2.0;
        }

        return result + (a);
    }
}