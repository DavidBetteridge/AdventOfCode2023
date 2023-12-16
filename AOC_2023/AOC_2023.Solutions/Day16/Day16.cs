namespace AOC_2023.Solutions;

public class Day16
{
    private const byte Up = 0b00;
    private const byte Down = 0b01;
    private const byte Left = 0b10;
    private const byte Right = 0b11;
    
    private const byte Dot = 0b000;
    private const byte Vertical = 0b001;
    private const byte Horizontal = 0b010;
    private const byte Slash = 0b011;  // /
    private const byte BackSlash = 0b100;  // \
    
    public long Part1(string filename)
    {
        var cave = File.ReadAllLines(filename)
            .Select(line => line.Select(c => c switch
                        {
                            '-' => Horizontal,
                            '|' => Vertical,
                            '/' => Slash,
                            '\\' => BackSlash,
                            _ => Dot
                        }            
                        ).ToArray()).ToArray();
        
        var visited = new HashSet<(int x, int y)>();
        var repeats = new HashSet<(int x, int y ,byte dir)>();
        var beams = new Queue<(int x, int y ,byte dir)>();
        return Solve(cave, 0, 0, Right, visited, repeats, beams);
    }

    public long Part2(string filename)
    {
        var cave = File.ReadAllLines(filename)
            .Select(line => line.Select(c => c switch
                {
                    '-' => Horizontal,
                    '|' => Vertical,
                    '/' => Slash,
                    '\\' => BackSlash,
                    _ => Dot
                }            
            ).ToArray()).ToArray();
        var rows = cave.Length;
        var cols = cave[0].Length;
        var result = 0;
        var visited = new HashSet<(int x, int y)>();
        var repeats = new HashSet<(int x, int y ,byte dir)>();
        var beams = new Queue<(int x, int y ,byte dir)>();
        
        for (var x = 0; x < cols; x++)
        {
            result = Math.Max(result, Solve(cave, x, 0, Down, visited, repeats, beams));
            result = Math.Max(result, Solve(cave, x, rows-1, Up, visited, repeats, beams));
        }
        
        for (var y = 0; y < rows; y++)
        {
            result = Math.Max(result, Solve(cave, 0, y, Right, visited, repeats, beams));
            result = Math.Max(result, Solve(cave, cols-1, y, Left, visited, repeats, beams));
        }
        
        return result;
    }
    
    private int Solve(byte[][] cave, int startX, int startY, byte initialDir,
                      HashSet<(int x, int y)> visited, 
                      HashSet<(int x, int y, byte dir)> repeats, 
                      Queue<(int x, int y, byte dir)> beams)
    {
        visited.Clear();
        repeats.Clear();
        
        var rows = cave.Length;
        var cols = cave[0].Length;

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
            
            if (dir is Right or Left && c == Vertical)
            {
                // Split
                beams.Enqueue((newX, newY - 1, Up));
                beams.Enqueue((newX, newY + 1, Down));
                continue;
            }

            if (dir is Up or Down && c == Horizontal)
            {
                // Split
                beams.Enqueue((newX - 1, newY, Left));
                beams.Enqueue((newX + 1, newY, Right));
                continue;
            }

            if (dir == Right && c == Slash)
            {
                beams.Enqueue((newX, newY - 1, Up));
                continue;
            }

            if (dir == Down && c == Slash)
            {
                beams.Enqueue((newX - 1, newY, Left));
                continue;
            }

            if (dir == Left && c == Slash)
            {
                beams.Enqueue((newX, newY + 1, Down));
                continue;
            }

            if (dir == Up && c == Slash)
            {
                beams.Enqueue((newX + 1, newY, Right));
                continue;
            }

            if (dir == Right && c == BackSlash)
            {
                beams.Enqueue((newX, newY + 1, Down));
                continue;
            }

            if (dir == Down && c == BackSlash)
            {
                beams.Enqueue((newX + 1, newY, Right));
                continue;
            }

            if (dir == Left && c == BackSlash)
            {
                beams.Enqueue((newX, newY - 1, Up));
                continue;
            }

            if (dir == Up && c == BackSlash)
            {
                beams.Enqueue((newX - 1, newY, Left));
                continue;
            }
            
            // Keep walking
            if (dir == Left) beams.Enqueue((newX - 1, newY, dir));
            if (dir == Right) beams.Enqueue((newX + 1, newY, dir));
            if (dir == Up) beams.Enqueue((newX, newY - 1, dir));
            if (dir == Down) beams.Enqueue((newX, newY + 1, dir));
        }
       
        return visited.Count;
    }
}