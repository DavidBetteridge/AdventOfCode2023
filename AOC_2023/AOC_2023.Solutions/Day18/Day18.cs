namespace AOC_2023.Solutions;

public class Day18
{
    private record Bounds
    {
        public int MinRow { get; set; }
        public int MaxRow { get; set; }
        public int MinCol { get; set; }
        public int MaxCol { get; set; }
    }
    
    public long Part1(string filename)
    {
        // U 2 (#7a21e3)
        var commands = File.ReadAllLines(filename);
        var grid = new HashSet<(int col, int row)> { (0, 0) };
        var notDug = new HashSet<(int col, int row)> ();

        var currentX = 0;
        var currentY = 0;
        foreach (var command in commands)
        {
            var bits = command.Split(' ');
            var dir = bits[0][0];
            var qty = int.Parse(bits[1]);

            if (dir == 'R')
                for (var i = 0; i < qty; i++)
                {
                    currentX++;
                    grid.Add((currentX, currentY));
                }
            
            else if (dir == 'L')
                for (var i = qty; i > 0; i--)
                {
                    currentX--;
                    grid.Add((currentX, currentY));
                }
      
            else if (dir == 'D')
                for (var i = 0; i < qty; i++)
                {
                    currentY++;
                    grid.Add((currentX, currentY));
                }
            
            else if (dir == 'U')
                for (var i = qty; i > 0; i--)
                {
                    currentY--;
                    grid.Add((currentX, currentY));
                }
        }

        // Bounds
        var bounds = new Bounds
        {
            MinRow = grid.Min(g => g.row),
            MaxRow = grid.Max(g => g.row),
            MinCol = grid.Min(g => g.col),
            MaxCol = grid.Max(g => g.col)
        };
        Display(bounds, grid, "Empty");

        // Pick the first empty point and flood fill it.
        for (var y = bounds.MinRow; y <= bounds.MaxRow; y++)
        {
            for (var x = bounds.MinCol; x <= bounds.MaxCol; x++)
            {
                if (!grid.Contains((x, y)) && !notDug.Contains((x, y)))
                {
                    FloodFill(grid, notDug, x, y, bounds);
                }
            }
        }
        Display(bounds, grid, "Filled");

        return grid.Count;
    }

    private static void Display(Bounds bounds, HashSet<(int col, int row)> grid, string name)
    {
        using var file = File.CreateText(
            $"/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day18/{name}.txt");
        for (var y = bounds.MinRow; y <= bounds.MaxRow; y++)
        {
            for (var x = bounds.MinCol; x <= bounds.MaxCol; x++)
            {
                if (grid.Contains((x, y)))
                    file.Write('#');
                else
                    file.Write('.');
            }

            file.WriteLine();
        }
    }

    private void FloodFill(HashSet<(int col, int row)> grid,
        HashSet<(int col, int row)> notDug, int x, int y, Bounds bounds)
    {
        
        var seen = new HashSet<(int, int)>();
        var q = new Queue<(int,int)>();
        q.Enqueue((x,y));
        var dig = true;
        while (q.Any())
        {
            var (nextX, nextY) = q.Dequeue();
            if (!grid.Contains((nextX, nextY)) && !seen.Contains((nextX, nextY)))
            {
                // If we have hit an edge,  then the entire shape cannot be dug.
                if (nextX == bounds.MinCol || 
                    nextY == bounds.MinRow || 
                    nextX == bounds.MaxCol || 
                    nextY == bounds.MaxRow)
                    dig = false;
                
                seen.Add((nextX, nextY));
                if (nextX-1 >= bounds.MinCol) q.Enqueue((nextX-1,nextY));
                if (nextX+1 <= bounds.MaxCol) q.Enqueue((nextX+1,nextY));
                if (nextY-1 >= bounds.MinRow) q.Enqueue((nextX,nextY-1));
                if (nextY+1 <= bounds.MaxRow) q.Enqueue((nextX,nextY+1));
            }
        }

        if (dig)
        {
            foreach (var s in seen)
                grid.Add(s);
        }
        else
        {
            foreach (var s in seen)
                notDug.Add(s);
        }
        
    }
}