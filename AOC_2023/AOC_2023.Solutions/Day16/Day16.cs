using System.Collections.Concurrent;

namespace AOC_2023.Solutions;

public class Day16
{
    private const byte Up = 0b0001;
    private const byte Down = 0b0010;
    private const byte Left = 0b0100;
    private const byte Right = 0b1000;
    
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
        
        var visited = new Dictionary<int, byte>();
        var beams = new Queue<(int x, int y ,byte dir)>();
        return Solve(cave, (0, 0, Right), visited, beams);
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

        var bag = new ConcurrentBag<int>();
        
        Parallel.For(0, cols, x =>
        {
            var visited = new Dictionary<int, byte>();
            var beams = new Queue<(int x, int y ,byte dir)>();
            bag.Add(Solve(cave, (x, 0, Down), visited, beams));
            visited.Clear();
            bag.Add(Solve(cave, (x, rows-1, Up), visited, beams));
        });

        Parallel.For(0, rows, y =>
        {
            var visited = new Dictionary<int, byte>();
            var beams = new Queue<(int x, int y, byte dir)>();
            bag.Add(Solve(cave, (0, y, Right), visited, beams));
            visited.Clear();
            bag.Add(Solve(cave, (cols - 1, y, Left), visited, beams));
        });
        
        return bag.Max();
    }
    
    private int Solve(byte[][] cave, (int x, int y, byte dir) details,
                      Dictionary<int, byte> visited, 
                      Queue<(int x, int y, byte dir)> beams)
    {
        var rows = cave.Length;
        var cols = cave[0].Length;

        do
        {
            var (newX, newY, dir) = details;
            
            // Bounce the beam until it leave the cave.
            while (true)
            {
                if (newX < 0) break;
                if (newX >= cols) break;
                if (newY < 0) break;
                if (newY >= rows) break;

                if (visited.TryGetValue(newX + (newY * cols), out var visitedDirs))
                {
                    if ((visitedDirs & dir) != 0) break; // Been here before from this direction
                    visited[newX + (newY * cols)] |= dir;
                }
                else
                    visited[newX + (newY * cols)] = dir;


                // What is this cell?
                var c = cave[newY][newX];

                if (dir is Right or Left && c == Vertical)
                {
                    // Split up and down
                    if (newY+1 == cols)
                    {
                        // Can't go down
                        newY--;
                        dir = Up;
                    }
                    else if (newY == 0)
                    {
                        // Can't go up
                        newY++;
                        dir = Down;
                    }
                    else
                    {
                        // We can go both ways
                        beams.Enqueue((newX, newY - 1, Up));
                        newY++;
                        dir = Down;
                    }
                    continue;
                }

                if (dir is Up or Down && c == Horizontal)
                {
                    // Split left and right
                    if (newX+1 == cols)
                    {
                        // Can't go right
                        newX--;
                        dir = Left;
                    }
                    else if (newX == 0)
                    {
                        // Can't go left
                        newX++;
                        dir = Right;
                    }
                    else
                    {
                        // We can go both ways
                        beams.Enqueue((newX - 1, newY, Left));
                        newX++;
                        dir = Right;
                    }
                    continue;
                }

                if (dir == Right && c == Slash)
                {
                    newY--;
                    dir = Up;
                    continue;
                }

                if (dir == Down && c == Slash)
                {
                    newX--;
                    dir = Left;
                    continue;
                }

                if (dir == Left && c == Slash)
                {
                    newY++;
                    dir = Down;
                    continue;
                }

                if (dir == Up && c == Slash)
                {
                    newX++;
                    dir = Right;
                    continue;
                }

                if (dir == Right && c == BackSlash)
                {
                    newY++;
                    dir = Down;
                    continue;
                }

                if (dir == Down && c == BackSlash)
                {
                    newX++;
                    dir = Right;
                    continue;
                }

                if (dir == Left && c == BackSlash)
                {
                    newY--;
                    dir = Up;
                    continue;
                }

                if (dir == Up && c == BackSlash)
                {
                    newX--;
                    dir = Left;
                    continue;
                }

                // Keep walking
                if (dir == Left) newX--;
                if (dir == Right) newX++;
                if (dir == Up) newY--;
                if (dir == Down) newY++;
            }

        } while (beams.TryDequeue(out details));

        return visited.Count;
    }
}