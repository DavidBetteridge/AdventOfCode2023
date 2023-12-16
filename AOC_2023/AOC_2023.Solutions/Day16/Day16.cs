namespace AOC_2023.Solutions;

public class Day16
{
    public long Part1(string filename)
    {
        var cave = File.ReadAllLines(filename);
        return Solve(cave, 0, 0, 'R');
    }

    public long Part2(string filename)
    {
        var cave = File.ReadAllLines(filename);
        var rows = cave.Length;
        var cols = cave[0].Length;
        var result = 0;
        
        for (var x = 0; x < cols; x++)
        {
            result = Math.Max(result, Solve(cave, x, 0, 'D'));
            result = Math.Max(result, Solve(cave, x, rows-1, 'U'));
        }
        
        for (var y = 0; y < rows; y++)
        {
            result = Math.Max(result, Solve(cave, 0, y, 'R'));
            result = Math.Max(result, Solve(cave, cols-1, y, 'L'));
        }
        
        return result;
    }
    
    private int Solve(string[] cave, int startX, int startY, char initialDir)
    {
        var rows = cave.Length;
        var cols = cave[0].Length;
        var visited = new HashSet<(int x, int y)>();
        var repeats = new HashSet<(int x, int y ,char dir)>();
        var beams = new Queue<(int x, int y ,char dir)>();
        beams.Enqueue((startX, startY, initialDir));

        while (beams.Count>0)
        {
            // Bounce the beam until it leave the cave.
            var (newX, newY, dir) = beams.Dequeue();
            if (newX < 0) continue;
            if (newX >= cols) continue;
            if (newY < 0) continue;
            if (newY >= rows) continue;
            if (repeats.Contains((newX, newY, dir))) continue;
            
            // Mark this cell as visited
            visited.Add((newX, newY));
            repeats.Add((newX, newY, dir));
            
            // What is this cell?
            var c = cave[newY][newX];
            
            if (dir is 'R' or 'L' && c == '|')
            {
                // Split
                beams.Enqueue((newX, newY - 1, 'U'));
                beams.Enqueue((newX, newY + 1, 'D'));
                continue;
            }

            if (dir is 'U' or 'D' && c == '-')
            {
                // Split
                beams.Enqueue((newX - 1, newY, 'L'));
                beams.Enqueue((newX + 1, newY, 'R'));
                continue;
            }

            if (dir == 'R' && c == '/')
            {
                beams.Enqueue((newX, newY - 1, 'U'));
                continue;
            }

            if (dir == 'D' && c == '/')
            {
                beams.Enqueue((newX - 1, newY, 'L'));
                continue;
            }

            if (dir == 'L' && c == '/')
            {
                beams.Enqueue((newX, newY + 1, 'D'));
                continue;
            }

            if (dir == 'U' && c == '/')
            {
                beams.Enqueue((newX + 1, newY, 'R'));
                continue;
            }

            if (dir == 'R' && c == '\\')
            {
                beams.Enqueue((newX, newY + 1, 'D'));
                continue;
            }

            if (dir == 'D' && c == '\\')
            {
                beams.Enqueue((newX + 1, newY, 'R'));
                continue;
            }

            if (dir == 'L' && c == '\\')
            {
                beams.Enqueue((newX, newY - 1, 'U'));
                continue;
            }

            if (dir == 'U' && c == '\\')
            {
                beams.Enqueue((newX - 1, newY, 'L'));
                continue;
            }
            
            // Keep walking
            if (dir == 'L') beams.Enqueue((newX - 1, newY, dir));
            if (dir == 'R') beams.Enqueue((newX + 1, newY, dir));
            if (dir == 'U') beams.Enqueue((newX, newY - 1, dir));
            if (dir == 'D') beams.Enqueue((newX, newY + 1, dir));
        }

        for (var row = 0; row < rows; row++)
        {
            for (var col = 0; col < cols; col++)
            {   
                if (visited.Contains((col, row)))
                    Console.Write('#');
                else
                    Console.Write('.');
            }
            Console.WriteLine();
        }
       
        return visited.Count;
    }
}