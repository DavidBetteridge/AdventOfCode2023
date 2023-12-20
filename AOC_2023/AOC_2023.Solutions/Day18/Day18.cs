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

        long boundary = 0;
        foreach (var command in commands)
        {
            var bits = command.Split(' ');
            var dir = bits[0][0];
            var qty = int.Parse(bits[1]);
            boundary += qty;
            current = dir switch
            {
                'R' => current with { X = current.X + qty },
                'L' => current with { X = current.X - qty },
                'D' => current with { Y = current.Y + qty },
                'U' => current with { Y = current.Y - qty },
                _ => current
            };
            points.Add(current);
        }

        var interior = Shoelace(points);
        return PicksTheorem(boundary, interior);
    }

    public long Part2(string filename)
    {
        var commands = File.ReadAllLines(filename);
        var points = new List<Point>();
        var current = new Point();

        long boundary = 0;
        foreach (var command in commands)
        {
            // R 6 (#70c710)
            var bits = command.Split(' ')[^1]; // (#70c710)
            var dirC = bits[7];
            var qty = Convert.ToInt32($"0x{bits[2..7]}", 16);
            boundary += qty;
            current = dirC switch
            {
                '0' => current with { X = current.X + qty },
                '1' => current with { Y = current.Y - qty },
                '2' => current with { X = current.X - qty },
                _ => current with { Y = current.Y + qty },
            };
            points.Add(current);
        }

        var interior = Shoelace(points);
        return PicksTheorem(boundary, interior);
    }
    
    private static long Shoelace(List<Point> points)
    {
        var N = points.Count - 1;
        var r1 = points[N].X * points[0].Y;
        for (var n = 0; n < N; n++)
            r1 += points[n].X * points[n + 1].Y;

        var r2 = points[0].X * points[N].Y;
        for (var n = 0; n < N; n++)
            r2 += points[n + 1].X * points[n].Y;

        return (Math.Abs(r1 - r2) / 2);
    }

    private static long PicksTheorem(long boundary, long interior)
    {
        return interior + (boundary / 2) + 1;   // Had to swap - for +
    }
}