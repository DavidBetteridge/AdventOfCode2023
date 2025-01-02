namespace AOC_2023.Solutions;

public class Day21_Part2
{
    public long Part2(string filename, int requiredSteps)
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

        // How many squares are reachable in odd and even steps
        var distancesStartingInTheMiddle = CountPossibleSquares(requiredSteps, rows, cols, startCol, startRow, graph);
        
        
        // How many sub-grids can be reached
        var gridsUp = (requiredSteps / rows);
        var gridsLeft = (requiredSteps / cols);
        var totalReached = 0L;
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

            //Display("distancesStartingInTheMiddle", distancesStartingInTheMiddle, rows, cols, graph);
            // Display("distancesTopLeft", distancesTopLeft, rows, cols, graph);
            // Display("distancesTopRight", distancesTopRight, rows, cols, graph);
            // Display("distancesLowerLeft", distancesLowerLeft, rows, cols, graph);
            // Display("distancesLowerRight", distancesLowerRight, rows, cols, graph);

            var toggle = 0;
            
            
            /////////////////////
            // Top Left Grid
            /////////////////////
            var available = requiredSteps - topLeft - 2;
            var grids = available / (cols + rows);
            var evens = (grids * grids) / 2;
            var odds = (grids * grids) - evens;

            totalReached += odds * distancesLowerRight.Count(d => d % 2 == 1 && d != int.MaxValue);
            totalReached += evens * distancesLowerRight.Count(d => d % 2 == 0 && d != int.MaxValue);
            
            for (var x = -gridsLeft; x < -grids; x++)
            {
                for (var y = (-grids); y < 0; y++)
                {
                    var distanceWalked = ((Math.Abs(x) - 1) * cols) + ((Math.Abs(y) - 1) * rows);
                    distanceWalked += topLeft + 2;
                    toggle = distanceWalked % 2 == 0 ? 0 : 1;
                    var remainingSteps = requiredSteps - distanceWalked;
                    totalReached += distancesLowerRight.Count(d => d % 2 == toggle && d <= remainingSteps);
                }
            }

            for (var x = (-gridsLeft); x < 0; x++)
            {
                for (var y = -gridsUp; y < -grids; y++)
                {
                    var distanceWalked = ((Math.Abs(x) - 1) * cols) + ((Math.Abs(y) - 1) * rows);
                    distanceWalked += topLeft + 2;
                    toggle = distanceWalked % 2 == 0 ? 0 : 1;
                    var remainingSteps = requiredSteps - distanceWalked;
                    totalReached += distancesLowerRight.Count(d => d % 2 == toggle && d <= remainingSteps);
                }
            }
            
            
            /////////////////////
            // Top Right Grid
            /////////////////////
            available = requiredSteps - topRight - 2;
            grids = available / (cols + rows);
            evens = (grids * grids) / 2;
            odds = (grids * grids) - evens;

            totalReached += odds * distancesLowerLeft.Count(d => d % 2 == 1 && d != int.MaxValue);
            totalReached += evens * distancesLowerLeft.Count(d => d % 2 == 0 && d != int.MaxValue);
            
            //Right edge
            for (var x = grids+1; x <= gridsLeft; x++)
            {
                for (var y = -grids; y < 0; y++)
                {
                    var distanceWalked = ((x - 1) * cols) + ((Math.Abs(y) - 1) * rows);
                    distanceWalked += topRight + 2;
                    toggle = distanceWalked % 2 == 0 ? 0 : 1;
                    var remainingSteps = requiredSteps - distanceWalked;
                    totalReached += distancesLowerLeft.Count(d => d % 2 == toggle && d <= remainingSteps);
                }
            }

            //Across the top
            for (var x = 1; x <= gridsLeft; x++)
            {
                for (var y = -gridsUp; y < -grids; y++)
                {
                    var distanceWalked = ((x - 1) * cols) + ((Math.Abs(y) - 1) * rows);
                    distanceWalked += topRight + 2;
                    toggle = distanceWalked % 2 == 0 ? 0 : 1;
                    var remainingSteps = requiredSteps - distanceWalked;
                    totalReached += distancesLowerLeft.Count(d => d % 2 == toggle && d <= remainingSteps);
                }
            }
            
            
            /////////////////////
            // Lower Left Grid
            /////////////////////
            available = requiredSteps - lowerLeft - 2;
            grids = available / (cols + rows);
            evens = (grids * grids) / 2;
            odds = (grids * grids) - evens;

            totalReached += odds * distancesTopRight.Count(d => d % 2 == 1 && d != int.MaxValue);
            totalReached += evens * distancesTopRight.Count(d => d % 2 == 0 && d != int.MaxValue);
            
            // Down the left
            for (var x = -gridsLeft; x < -grids; x++)
            {
                for (var y =1; y <= grids; y++)
                {
                    var distanceWalked = ((Math.Abs(x) - 1) * cols) + ((Math.Abs(y) - 1) * rows);
                    distanceWalked += topLeft + 2;
                    toggle = distanceWalked % 2 == 0 ? 0 : 1;
                    var remainingSteps = requiredSteps - distanceWalked;
                    totalReached += distancesTopRight.Count(d => d % 2 == toggle && d <= remainingSteps);
                }
            }

            // Across the bottom
            for (var x = (-gridsLeft); x < 0; x++)
            {
                for (var y = grids+1; y <= gridsUp; y++)
                {
                    var distanceWalked = ((Math.Abs(x) - 1) * cols) + ((Math.Abs(y) - 1) * rows);
                    distanceWalked += topLeft + 2;
                    toggle = distanceWalked % 2 == 0 ? 0 : 1;
                    var remainingSteps = requiredSteps - distanceWalked;
                    totalReached += distancesTopRight.Count(d => d % 2 == toggle && d <= remainingSteps);
                }
            }
            

            for (var x = -gridsLeft; x <= gridsLeft; x++)
            {
                if (x != 0)
                {
                    for (var y = -gridsUp; y <= gridsUp; y++)
                    {
                        if (y != 0)
                        {
                            // The distance walked to reach the corner of the grid
                            var distanceWalked = ((Math.Abs(x) - 1) * cols) + ((Math.Abs(y) - 1) * rows);
                            if (x < 0 && y < 0)
                                distanceWalked += topLeft + 2;
                            if (x < 0 && y > 0)
                                distanceWalked += lowerLeft + 2;
                            if (x > 0 && y < 0)
                                distanceWalked += topRight + 2;
                            if (x > 0 && y > 0)
                                distanceWalked += lowerRight + 2;

                            toggle = distanceWalked % 2 == 0 ? 0 : 1;

                            var remainingSteps = requiredSteps - distanceWalked;
                            // if (x < 0 && y < 0)
                            //     totalReached += distancesLowerRight.Count(d => d % 2 == toggle && d <= remainingSteps);
                            // if (x < 0 && y > 0)
                            //     totalReached += distancesTopRight.Count(d => d % 2 == toggle && d <= remainingSteps);
                            // if (x > 0 && y < 0)
                            //     totalReached += distancesLowerLeft.Count(d => d % 2 == toggle && d <= remainingSteps);
                            if (x > 0 && y > 0)
                                totalReached += distancesTopLeft.Count(d => d % 2 == toggle && d <= remainingSteps);
                        }
                    }
                }
            }


            // Walk to the right.  Starting at the TopRight / LowerRight
            var walked = topRight; // Take the shortest path to the top-right corner.
            walked += 1; // Then take a single step to the right, so be in the top-left corner
            while (walked < requiredSteps)
            {
                toggle = walked % 2 == 0 ? 0 : 1;
                var remaining = requiredSteps - walked;
                if (remaining >= cols*2)
                    totalReached += distancesTopLeft.Count(d => d % 2 == toggle && d <= remaining);
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
                    
                    totalReached += fromAbove.Concat(fromBelow).ToHashSet().Count();
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
                    totalReached += distancesTopRight.Count(d => d % 2 == toggle && d <= remaining);
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
                    
                    totalReached += fromAbove.Concat(fromBelow).ToHashSet().Count();
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
                    totalReached += distancesLowerLeft.Count(d => d % 2 == toggle && d <= remaining);
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
                    
                    totalReached += fromAbove.Concat(fromBelow).ToHashSet().Count();
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
                   totalReached += distancesTopLeft.Count(d => d % 2 == toggle && d <= remaining);
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
                    
                   totalReached += fromAbove.Concat(fromBelow).ToHashSet().Count();
               }
               
               
                walked += rows;
            }
        }

        // Include the middle square
        totalReached += distancesStartingInTheMiddle.Count(d => d % 2 == 0 && d != int.MaxValue);
        
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