namespace AOC_2023.Solutions;



public class Day21_Part2
{
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

            // How many steps are needed to reach the middle of each side of the grid
            var topMiddle = distancesStartingInTheMiddle[rows/2];
            var leftMiddle = distancesStartingInTheMiddle[cols * (rows/2)];
            var lowerMiddle = distancesStartingInTheMiddle[cols * (rows - 1) + (cols/2)];
            var rightMiddle = distancesStartingInTheMiddle[ (cols * (rows / 2)) + cols];
            
            // What are the distances from each corner
            var distancesTopLeft = CountPossibleSquares(requiredSteps, rows, cols, 0, 0, graph);
            var distancesTopRight = CountPossibleSquares(requiredSteps, rows, cols, cols - 1, 0, graph);
            var distancesLowerLeft = CountPossibleSquares(requiredSteps, rows, cols, 0, rows - 1, graph);
            var distancesLowerRight = CountPossibleSquares(requiredSteps, rows, cols, cols - 1, rows - 1, graph);

            // What are the distances from the middle of each side
            var distancesTopMiddle = CountPossibleSquares(requiredSteps, rows, cols, cols/2, 0, graph);
            var distancesRightMiddle = CountPossibleSquares(requiredSteps, rows, cols, cols - 1, rows/2, graph);
            var distancesLowerMiddle = CountPossibleSquares(requiredSteps, rows, cols, cols/2, rows - 1, graph);
            var distancesLeftMiddle = CountPossibleSquares(requiredSteps, rows, cols, 0, rows/2, graph);
            
            //Display("distancesStartingInTheMiddle", distancesStartingInTheMiddle, rows, cols, graph);
            //Display("distancesTopLeft", distancesTopLeft, rows, cols, graph);
            //Display("distancesTopRight", distancesTopRight, rows, cols, graph);
            //Display("distancesLowerLeft", distancesLowerLeft, rows, cols, graph);
            //Display("distancesLowerRight", distancesLowerRight, rows, cols, graph);
            // Display("distancesTopMiddle", distancesTopMiddle, rows, cols, graph);
            // Display("distancesRightMiddle", distancesRightMiddle, rows, cols, graph);
            // Display("distancesLowerMiddle", distancesLowerMiddle, rows, cols, graph);
            // Display("distancesLeftMiddle", distancesLeftMiddle, rows, cols, graph);

            var toggle = 0;
            
            var fullOddGrid = distancesStartingInTheMiddle.Count(d => d % 2 == 1 && d != int.MaxValue);
            var fullEvenGrid = distancesStartingInTheMiddle.Count(d => d % 2 == 0 && d != int.MaxValue);
            
            // Same distance to all edges
            var walked1 = topMiddle + 1;
            var fullGrids = ((requiredSteps - walked1) / size)-1;
            var partial = (requiredSteps - walked1) - (fullGrids * size);
            
            // Fullgrids are 0 E 0 E or 0 E 0 or O E
            var evens = fullGrids / 2;
            var odds = fullGrids - evens;

            if (requiredSteps % 2 == 1)
                (evens, odds) = (odds, evens);
            
            totalReached = 4* (((ulong)fullOddGrid * (ulong)odds) + (ulong)(fullEvenGrid * evens));
            
            
            // 4 partials
            totalReached += (ulong)distancesTopMiddle.Count(d => d % 2 == (partial % 2) && d <= partial);
            totalReached += (ulong)distancesTopMiddle.Count(d => d % 2 == ((partial - rows) % 2) && d <= (partial - rows));
            totalReached += (ulong)distancesLowerMiddle.Count(d => d % 2 == (partial % 2) && d <= partial);
            totalReached += (ulong)distancesLowerMiddle.Count(d => d % 2 == ((partial - rows) % 2) && d <= (partial - rows));
            totalReached += (ulong)distancesLeftMiddle.Count(d => d % 2 == (partial % 2) && d <= partial);
            totalReached += (ulong)distancesLeftMiddle.Count(d => d % 2 == ((partial - rows) % 2) && d <= (partial - rows));
            totalReached += (ulong)distancesRightMiddle.Count(d => d % 2 == (partial % 2) && d <= partial);
            totalReached += (ulong)distancesRightMiddle.Count(d => d % 2 == ((partial - rows) % 2) && d <= (partial - rows));
            
            if (requiredSteps % 2 == 0)
                totalReached += (ulong)fullEvenGrid;
            else
                totalReached += (ulong)fullOddGrid;
            
            totalReached += Count(requiredSteps, distancesLowerRight, topLeft, size, fullEvenGrid, fullOddGrid);
            totalReached += Count(requiredSteps, distancesLowerLeft, topRight, size, fullEvenGrid, fullOddGrid);
            totalReached += Count(requiredSteps, distancesTopRight, lowerLeft, size, fullEvenGrid, fullOddGrid);
            totalReached += Count(requiredSteps, distancesTopLeft, lowerRight, size, fullEvenGrid, fullOddGrid);

          return totalReached;
         
            // Walk to the right.  Starting at the TopRight / LowerRight
            var walked = topMiddle; // Take the shortest path to the top-right corner.
            walked += 1; // Then take a single step to the right, so be in the top-left corner
            while (walked < requiredSteps)
            {
                toggle = walked % 2 == 0 ? 0 : 1;
                var remaining = requiredSteps - walked;
                if (remaining >= cols*2)
                    totalReached += (ulong)distancesLowerMiddle.Count(d => d % 2 == (remaining % 2) && d <= remaining);
                else
                {
                    totalReached += (ulong)distancesLowerMiddle.Select((d,i) => new {d,i})
                        .Where(d => d.d % 2 == (remaining % 2) && d.d <= remaining)
                        .Select(d=> d.i)
                        .Count();
                    
                    // var fromBelow = distancesLowerLeft.Select((d,i) => new {d,i})
                    //     .Where(d => d.d % 2 == toggle && d.d <= ((remaining + topRight) - lowerRight))
                    //     .Select(d=> d.i)
                    //     .ToHashSet();
                    
                  //  totalReached += (ulong)fromAbove.Concat(fromBelow).ToHashSet().Count();
                }
                
                walked += cols;
            }

            // Walk to the left.  Starting at TopLeft / LowerLeft
            walked = topMiddle; // Take the shortest path to the top-left corner.
            walked += 1; // Then take a single step to the left, so be in the top-right corner
            while (walked < requiredSteps)
            {
                toggle = walked % 2 == 0 ? 0 : 1;
                var remaining = requiredSteps - walked;
                if (remaining >= cols*2)
                    totalReached += (ulong)distancesTopMiddle.Count(d => d % 2 == (remaining % 2) && d <= remaining);
                else
                {
                    totalReached += (ulong)distancesTopMiddle.Select((d,i) => new {d,i})
                        .Where(d => d.d % 2 == (remaining % 2) && d.d <= remaining)
                        .Select(d=> d.i)
                        .Count();
                    
                    // var fromBelow = distancesLowerRight.Select((d,i) => new {d,i})
                    //     .Where(d => d.d % 2 == toggle && d.d <= remaining + topLeft - lowerLeft )
                    //     .Select(d=> d.i)
                    //     .ToHashSet();
                    //
                    // totalReached += (ulong)fromAbove.Concat(fromBelow).ToHashSet().Count();
                }
                
                walked += cols;
            }

            // Walk up.  Starting TopLeft / TopRight
            walked = topMiddle; // Take the shortest path to the top-left corner.
            walked += 1; // Then take a single step up, so be in the lower-left corner
            while (walked < requiredSteps)
            {
                toggle = walked % 2 == 0 ? 0 : 1;
             
                var remaining = requiredSteps - walked;
                if (remaining >= cols*2)
                    totalReached += (ulong)distancesLeftMiddle.Count(d => d % 2 == (remaining % 2) && d <= remaining);
                else
                {
                    totalReached += (ulong)distancesLeftMiddle.Select((d,i) => new {d,i})
                        .Where(d => d.d % 2 == (remaining % 2) && d.d <= remaining)
                        .Select(d=> d.i)
                        .Count();
                    
                    // var fromBelow = distancesLowerRight.Select((d,i) => new {d,i})
                    //     .Where(d => d.d % 2 == toggle && d.d <= ((remaining + topLeft) - topRight))
                    //     .Select(d=> d.i)
                    //     .ToHashSet();
                    //
                    // totalReached += (ulong)fromAbove.Concat(fromBelow).ToHashSet().Count();
                }
                
                walked += rows;
            }

            // Walk down.  Starting at tLowerLeft / LowerRight
            walked = topMiddle; // Take the shortest path to the top-left corner.
            walked += 1; // Then take a single step down, so be in the top-left corner
            while (walked < requiredSteps)
            {
                toggle = walked % 2 == 0 ? 0 : 1;
               
               var remaining = requiredSteps - walked;
               if (remaining >= cols*2)
                   totalReached += (ulong)distancesRightMiddle.Count(d => d % 2 == (remaining % 2) && d <= remaining);
               else
               {
                   totalReached += (ulong)distancesRightMiddle.Select((d,i) => new {d,i})
                       .Where(d => d.d % 2 == (remaining % 2) && d.d <= remaining)
                       .Select(d=> d.i)
                       .Count();

                    
                   // var fromBelow = distancesTopRight.Select((d,i) => new {d,i})
                   //     .Where(d => d.d % 2 == toggle && d.d <= ((remaining + lowerLeft) - lowerRight))
                   //     .Select(d=> d.i)
                   //     .ToHashSet();
                   //  
                   // totalReached += (ulong)fromAbove.Concat(fromBelow).ToHashSet().Count();
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
      //  var m1 =  distances.Where(d => d != int.MaxValue).Max();
        
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
        
        // partial -= size; 
        // toggle = partial % 2;
        // partialCount = (ulong)distances.Count(d => d % 2 == toggle && d <= partial);
        // totalReached += partialCount * (ulong)(3+fullGrids);
        
        var evens = 0L;
        for (var i = 1; i <= fullGrids; i += 2)
            evens += i;
        
        var odds = 0L;
        for (var i = 0; i <= fullGrids; i += 2)
            odds += i;
        
        if (requiredSteps % 2 == 1)
            (evens, odds) = (odds, evens);
        
        totalReached+=((ulong)evens * (ulong)fullEvenGrid) + ((ulong)odds * (ulong)fullOddGrid);
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