namespace AOC_2023.Solutions;



public class Day21_Part2
{
    private class DistanceCache
    {
        public static int TopLeft = 0;
        public static int TopRight = 1;
        public static int LowerLeft = 2;
        public static int LowerRight = 3;

        private readonly int[][] _maps = new int[4][];

        private readonly Dictionary<(int, long), int>[] _cache = new Dictionary<(int, long), int>[4];
        
        public DistanceCache(int[] distancesTopLeft, int[] distancesTopRight, int[] distancesLowerLeft, int[] distancesLowerRight)
        {
            _maps[TopLeft] = distancesTopLeft;
            _maps[TopRight] = distancesTopRight;
            _maps[LowerLeft] = distancesLowerLeft;
            _maps[LowerRight] = distancesLowerRight;
            _cache[TopLeft] = new Dictionary<(int, long), int>();
            _cache[TopRight] = new Dictionary<(int, long), int>();
            _cache[LowerLeft] = new Dictionary<(int, long), int>();
            _cache[LowerRight] = new Dictionary<(int, long), int>();
        }

        public ulong Lookup(int cacheName, int toggle, long remainingSteps)
        {
            if (!_cache[cacheName].TryGetValue((toggle, remainingSteps), out var cnt))
            {
                cnt = _maps[cacheName].Count(d => d % 2 == toggle && d <= remainingSteps);
                _cache[cacheName].Add((toggle, remainingSteps), cnt);
            }

            return (ulong)cnt;
        }
    }
    
    public ulong Part2(string filename, int requiredSteps)
    {
        // Load Graph
        var graph = File.ReadAllLines(filename);
        var rows = graph.Length;
        var cols = graph[0].Length;
        if (rows != cols)
            throw new Exception("Not square");
        var size = rows;
        
        // Find the start position
        var startRow = 0;
        var startCol = 0;
        foreach (var row in graph)
        {
            startCol = row.IndexOf('S');
            if (startCol != -1) break;
            startRow++;
        }

        // How many squares are reachable in odd and even steps
        var distancesStartingInTheMiddle = CountPossibleSquares(requiredSteps, rows, cols, startCol, startRow, graph);
        
        
        // How many sub-grids can be reached
        var gridsUp = (requiredSteps / rows);
        var gridsLeft = (requiredSteps / cols);
        ulong totalReached = 0L;
        if (gridsUp > 1)
        {
            // How many steps are needed to reach a corner of the grid
            var topLeft = distancesStartingInTheMiddle[0];
            var topRight = distancesStartingInTheMiddle[cols - 1];
            var lowerLeft = distancesStartingInTheMiddle[cols * (rows - 1)];
            var lowerRight = distancesStartingInTheMiddle[(cols * rows) - 1];

            // What are the distances from each corner
            var distancesTopLeft = CountPossibleSquares(requiredSteps, rows, cols, 0, 0, graph);
            var distancesTopRight = CountPossibleSquares(requiredSteps, rows, cols, cols - 1, 0, graph);
            var distancesLowerLeft = CountPossibleSquares(requiredSteps, rows, cols, 0, rows - 1, graph);
            var distancesLowerRight = CountPossibleSquares(requiredSteps, rows, cols, cols - 1, rows - 1, graph);

            var cache = new DistanceCache(distancesTopLeft, distancesTopRight, distancesLowerLeft, distancesLowerRight);
            
            //Display("distancesStartingInTheMiddle", distancesStartingInTheMiddle, rows, cols, graph);
            //Display("distancesTopLeft", distancesTopLeft, rows, cols, graph);
            // Display("distancesTopRight", distancesTopRight, rows, cols, graph);
            // Display("distancesLowerLeft", distancesLowerLeft, rows, cols, graph);
           //  Display("distancesLowerRight", distancesLowerRight, rows, cols, graph);

            var toggle = 0;
            
            var fullOddGrid = distancesStartingInTheMiddle.Count(d => d % 2 == 1 && d != int.MaxValue);
            var fullEvenGrid = distancesStartingInTheMiddle.Count(d => d % 2 == 0 && d != int.MaxValue);
            totalReached = Count(requiredSteps, distancesLowerRight, topLeft, size, fullEvenGrid, fullOddGrid);
            totalReached += Count(requiredSteps, distancesLowerLeft, topRight, size, fullEvenGrid, fullOddGrid);
            totalReached += Count(requiredSteps, distancesTopRight, lowerLeft, size, fullEvenGrid, fullOddGrid);
            totalReached += Count(requiredSteps, distancesTopLeft, lowerRight, size, fullEvenGrid, fullOddGrid);

         
            // Walk to the right.  Starting at the TopRight / LowerRight
            var walked = topRight; // Take the shortest path to the top-right corner.
            walked += 1; // Then take a single step to the right, so be in the top-left corner
            while (walked < requiredSteps)
            {
                toggle = walked % 2 == 0 ? 0 : 1;
                var remaining = requiredSteps - walked;
                if (remaining >= cols*2)
                    totalReached += (ulong)distancesTopLeft.Count(d => d % 2 == toggle && d <= remaining);
                else
                {
                    var fromAbove = distancesTopLeft.Select((d,i) => new {d,i})
                        .Where(d => d.d % 2 == toggle && d.d <= remaining)
                        .Select(d=> d.i)
                        .ToHashSet();
                    
                    var fromBelow = distancesLowerLeft.Select((d,i) => new {d,i})
                        .Where(d => d.d % 2 == toggle && d.d <= ((remaining + topRight) - lowerRight))
                        .Select(d=> d.i)
                        .ToHashSet();
                    
                    totalReached += (ulong)fromAbove.Concat(fromBelow).ToHashSet().Count();
                }
                
                walked += cols;
            }

            // Walk to the left.  Starting at TopLeft / LowerLeft
            walked = topLeft; // Take the shortest path to the top-left corner.
            walked += 1; // Then take a single step to the left, so be in the top-right corner
            while (walked < requiredSteps)
            {
                toggle = walked % 2 == 0 ? 0 : 1;
                var remaining = requiredSteps - walked;
                if (remaining >= cols*2)
                    totalReached += (ulong)distancesTopRight.Count(d => d % 2 == toggle && d <= remaining);
                else
                {
                    var fromAbove = distancesTopRight.Select((d,i) => new {d,i})
                        .Where(d => d.d % 2 == toggle && d.d <= remaining)
                        .Select(d=> d.i)
                        .ToHashSet();
                    
                    var fromBelow = distancesLowerRight.Select((d,i) => new {d,i})
                        .Where(d => d.d % 2 == toggle && d.d <= remaining + topLeft - lowerLeft )
                        .Select(d=> d.i)
                        .ToHashSet();
                    
                    totalReached += (ulong)fromAbove.Concat(fromBelow).ToHashSet().Count();
                }
                
                walked += cols;
            }

            // Walk up.  Starting TopLeft / TopRight
            walked = topLeft; // Take the shortest path to the top-left corner.
            walked += 1; // Then take a single step up, so be in the lower-left corner
            while (walked < requiredSteps)
            {
                toggle = walked % 2 == 0 ? 0 : 1;
             
                var remaining = requiredSteps - walked;
                if (remaining >= cols*2)
                    totalReached += (ulong)distancesLowerLeft.Count(d => d % 2 == toggle && d <= remaining);
                else
                {
                    var fromAbove = distancesLowerLeft.Select((d,i) => new {d,i})
                        .Where(d => d.d % 2 == toggle && d.d <= remaining)
                        .Select(d=> d.i)
                        .ToHashSet();
                    
                    var fromBelow = distancesLowerRight.Select((d,i) => new {d,i})
                        .Where(d => d.d % 2 == toggle && d.d <= ((remaining + topLeft) - topRight))
                        .Select(d=> d.i)
                        .ToHashSet();
                    
                    totalReached += (ulong)fromAbove.Concat(fromBelow).ToHashSet().Count();
                }
                
                walked += rows;
            }

            // Walk down.  Starting at tLowerLeft / LowerRight
            walked = lowerLeft; // Take the shortest path to the top-left corner.
            walked += 1; // Then take a single step down, so be in the top-left corner
            while (walked < requiredSteps)
            {
                toggle = walked % 2 == 0 ? 0 : 1;
               
               var remaining = requiredSteps - walked;
               if (remaining >= cols*2)
                   totalReached += (ulong)distancesTopLeft.Count(d => d % 2 == toggle && d <= remaining);
               else
               {
                   var fromAbove = distancesTopLeft.Select((d,i) => new {d,i})
                       .Where(d => d.d % 2 == toggle && d.d <= remaining)
                       .Select(d=> d.i)
                       .ToHashSet();
                    
                   var fromBelow = distancesTopRight.Select((d,i) => new {d,i})
                       .Where(d => d.d % 2 == toggle && d.d <= ((remaining + lowerLeft) - lowerRight))
                       .Select(d=> d.i)
                       .ToHashSet();
                    
                   totalReached += (ulong)fromAbove.Concat(fromBelow).ToHashSet().Count();
               }
               
               
                walked += rows;
            }
        }

        // Include the middle square
        totalReached += (ulong)distancesStartingInTheMiddle.Count(d => d % 2 == 0 && d != int.MaxValue);
        
        return totalReached;
    }

    private static ulong Count(int requiredSteps, int[] distances, int distanceToStart, int size, int fullEvenGrid, int fullOddGrid)
    {
        
        var maxOdd = distances.MaxBy(d => d % 2 == 1 && d != int.MaxValue);
        var maxEven = distances.MaxBy(d => d % 2 == 0 && d != int.MaxValue);
        var numberOfPartials = maxOdd / size;
        
        var walked = distanceToStart + 2;

        var remaining = requiredSteps - walked;
        var fullGrids = (remaining / size)-1;
        
        var partial = remaining - (fullGrids * size);
        var toggle = partial % 2;
        var partialCount = (ulong)distances.Count(d => d % 2 == toggle && d <= partial);
        var totalReached = partialCount * (ulong)(1+fullGrids);

        partial -= size; 
        toggle = partial % 2;
        partialCount = (ulong)distances.Count(d => d % 2 == toggle && d <= partial);
        totalReached += partialCount * (ulong)(2+fullGrids);
        
        var e = 0;
        for (var i = 1; i <= fullGrids; i += 2)
            e += i;
        
        var o = 0;
        for (var i = 0; i <= fullGrids; i += 2)
            o += i;
        
        totalReached+=((ulong)e * (ulong)fullEvenGrid) + ((ulong)o * (ulong)fullOddGrid);
        return totalReached;
    }

    private void Display(string caption, int[] map, int rows, int cols, string[] graph)
    {
        Console.WriteLine(); 
        Console.WriteLine(); 
        Console.WriteLine(caption); 
        for (var row = 0; row < rows; row++)
        {
            for (var col = 0; col < cols; col++)
            {
                if (graph[row][col] == '#')
                    Console.Write("-");
                else if (map[row * cols + col] == int.MaxValue)
                    Console.Write("?");
                else
                {
                    if (map[row * cols + col] <= 15)
                        Console.Write(map[row * cols + col].ToString("x"));
                    else
                        Console.Write((char)(map[row * cols + col] - 10 + 'a'));
                }
                    
            }
            Console.WriteLine();            
        }
    }


    private int[] CountPossibleSquares(int requiredSteps, int rows, int cols, int startCol, int startRow, string[] graph)
    {
        // Compute the shortest paths from S
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

        return distances;

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
}