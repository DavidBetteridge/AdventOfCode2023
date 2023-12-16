namespace AOC_2023.Solutions;

public class Day16
{
    public long Part1(string filename)
    {
        var cave = File.ReadAllLines(filename);
        var rows = cave.Length;
        var cols = cave[0].Length;
        var visited = new bool[rows * cols];
        var result = 0;
        var beams = new Queue<(int, int, char, char)>();
        beams.Enqueue((0, 0, 'R', ' '));


        do
        {
            // Bounce the beam until it leave the cave.
            var (x, y, xDir, yDir) = beams.Dequeue();


            visited[x * cols + y] = true;

            var newX = x;
            if (xDir == 'L') newX--;
            if (newX < 0) continue;
            if (xDir == 'R') newX++;
            if (newX + 1 >= cols) continue;

            var newY = y;
            if (yDir == 'U') newY--;
            if (newY < 0) continue;
            if (yDir == 'D') newY++;
            if (newY + 1 >= rows) continue;

            var c = cave[newY][newX];

            if (xDir != ' ' && c == '|')
            {
                // Split
                beams.Enqueue((newX, newY - 1, ' ', 'U'));
                beams.Enqueue((newX, newY + 1, ' ', 'D'));
                break;
            }

            if (yDir != ' ' && c == '-')
            {
                // Split
                beams.Enqueue((newX - 1, newY, 'L', ' '));
                beams.Enqueue((newX + 1, newY, 'R', ' '));
                break;
            }

            if (xDir == 'R' && c == '/')
            {
                beams.Enqueue((newX, newY - 1, ' ', 'U'));
                break;
            }

            if (yDir == 'D' && c == '/')
            {
                beams.Enqueue((newX - 1, newY, 'L', ' '));
                break;
            }

            if (xDir == 'L' && c == '/')
            {
                beams.Enqueue((newX, newY + 1, ' ', 'D'));
                break;
            }

            if (yDir == 'U' && c == '/')
            {
                beams.Enqueue((newX + 1, newY, 'R', ' '));
                break;
            }

            ////
            if (xDir == 'R' && c == '\\')
            {
                beams.Enqueue((newX, newY + 1, ' ', 'D'));
                break;
            }

            if (yDir == 'D' && c == '\\')
            {
                beams.Enqueue((newX + 1, newY, 'R', ' '));
                break;
            }

            if (xDir == 'L' && c == '\\')
            {
                beams.Enqueue((newX, newY - 1, ' ', 'U'));
                break;
            }

            if (yDir == 'U' && c == '\\')
            {
                beams.Enqueue((newX - 1, newY, 'L', ' '));
                break;
            }
            
            
            
        } while (beams.Any());


        return result;
    }
}