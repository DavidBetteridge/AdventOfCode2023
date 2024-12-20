namespace AOC_2023.Solutions;

public class Day21
{
    public long Part1(string filename, int requiredSteps)
    {
        // Load Graph
        var graph = File.ReadAllLines(filename);
        var rows = graph.Length;
        var cols = graph[0].Length;

        // Find the start position
        var startRow = 0;
        var startCol = 0;
        foreach (var row in graph)
        {
            startCol = row.IndexOf('S');
            if (startCol != -1) break;
            startRow++;
        }

        // Compute shortest paths from S
        var distances = new int[rows * cols];
        Array.Fill(distances, int.MaxValue);
        distances[startCol + (startRow * cols)] = 0;

        var seen = new bool[rows * cols];

        for (var n = 0; n < rows * cols; n++)
        {
            var v = FindSmallest(seen, distances);
            if (distances[v] > requiredSteps) break;
            seen[v] = true;

            var col = v % cols;
            var row = (v - col) / cols;

            // Where can we go from graph[row, col]
            if (distances[v] != int.MaxValue)
            {
                if (row > 0 && graph[row - 1][col] != '#')
                {
                    if ((distances[v] + 1) < distances[col + ((row - 1) * cols)])
                    {
                        distances[col + ((row - 1) * cols)] = distances[v] + 1;
                    }
                }

                if (col > 0 && graph[row][col - 1] != '#')
                {
                    if ((distances[v] + 1) < distances[(col - 1) + (row * cols)])
                    {
                        distances[(col - 1) + (row * cols)] = distances[v] + 1;
                    }
                }

                if (row + 1 < rows && graph[row + 1][col] != '#')
                {
                    if ((distances[v] + 1) < distances[col + ((row + 1) * cols)])
                    {
                        distances[col + ((row + 1) * cols)] = distances[v] + 1;
                    }
                }

                if (col + 1 < cols && graph[row][col + 1] != '#')
                {
                    if ((distances[v] + 1) < distances[(col + 1) + (row * cols)])
                    {
                        distances[(col + 1) + (row * cols)] = distances[v] + 1;
                    }
                }
            }
        }


        return distances.Count(d => d % 2 == 0);
    }

    private int FindSmallest(bool[] seen, int[] distances)
    {
        var smallestCost = int.MaxValue;
        var smallestIndex = -1;

        for (var i = 0; i < distances.Length; i++)
        {
            if (distances[i] <= smallestCost && !seen[i])
            {
                smallestCost = distances[i];
                smallestIndex = i;
            }
        }

        return smallestIndex;
    }


    public long Part2(string filename, int requiredSteps)
    {
        // The garden is 11 x 100
        // 11 is also a factor of 26501365   (2409215)

        // Load Graph
        var graph = File.ReadAllLines(filename);
        var rows = graph.Length;
        var cols = graph[0].Length;

        // Find the start position
        var startRow = 0;
        var startCol = 0;
        foreach (var row in graph)
        {
            startCol = row.IndexOf('S');
            if (startCol != -1) break;
            startRow++;
        }

        var map = graph.Select(row => row.Select(cell => cell != '#').ToArray()).ToArray();
        
        var visited = new HashSet<(int col, int row)>();
        var queue = new Queue<(int col, int row, int steps)>();
        queue.Enqueue((startCol, startRow, 0));

        while (queue.Any())
        {
            var (x, y, stepsTaken) = queue.Dequeue();
            if (stepsTaken == requiredSteps)
            {
                visited.Add((x, y));
            }
            else
            {
                if (map[nmod(y,rows)][nmod(x - 1, cols)])
                    queue.Enqueue((x - 1, y, stepsTaken + 1));
                if (map[nmod(y-1,rows)][nmod(x,cols)])
                    queue.Enqueue((x, y - 1, stepsTaken + 1));
                if (map[nmod(y,rows)][nmod(x+1,cols)])
                    queue.Enqueue((x+1, y, stepsTaken + 1));
                if (map[nmod(y + 1,rows)][nmod(x,cols)])
                    queue.Enqueue((x, y+1, stepsTaken + 1));
            }
        }

        return visited.Count;
    }
    
    int nmod(int x, int m) {
        var r = x%m;
        return r<0 ? r+m : r;
    }
}