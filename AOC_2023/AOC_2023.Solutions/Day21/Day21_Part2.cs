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
                            if (x < 0 && y < 0)
                                totalReached += distancesLowerRight.Count(d => d % 2 == toggle && d <= remainingSteps);
                            if (x < 0 && y > 0)
                                totalReached += distancesTopRight.Count(d => d % 2 == toggle && d <= remainingSteps);
                            if (x > 0 && y < 0)
                                totalReached += distancesLowerLeft.Count(d => d % 2 == toggle && d <= remainingSteps);
                            if (x > 0 && y > 0)
                                totalReached += distancesTopLeft.Count(d => d % 2 == toggle && d <= remainingSteps);
                        }
                    }
                }
            }

            // 639879


            // Walk to the right.  Starting at the shorter of TopRight and LowerRight
            var walked = topRight; // Take the shortest path to the top-right corner.
            walked += 1; // Then take a single step to the right, so be in the top-left corner
            while (walked < requiredSteps)
            {
                toggle = walked % 2 == 0 ? 0 : 1;
                totalReached += distancesTopLeft.Count(d => d % 2 == toggle && d <= (requiredSteps - walked));
                walked += cols;
            }

            // Walk to the left.  Starting at the shorter of TopLeft and LowerLeft
            walked = topLeft; // Take the shortest path to the top-left corner.
            walked += 1; // Then take a single step to the left, so be in the top-right corner
            while (walked < requiredSteps)
            {
                toggle = walked % 2 == 0 ? 0 : 1;
                totalReached += distancesTopRight.Count(d => (d % 2) == toggle && (d <= (requiredSteps - walked)));
                walked += cols;
            }

            // Walk up.  Starting at the shorter of TopLeft and TopRight
            walked = topLeft; // Take the shortest path to the top-left corner.
            walked += 1; // Then take a single step up, so be in the lower-left corner
            while (walked < requiredSteps)
            {
                toggle = walked % 2 == 0 ? 0 : 1;
                totalReached += distancesLowerLeft.Count(d => d % 2 == toggle && d <= (requiredSteps - walked));
                walked += rows;
            }

            // Walk down.  Starting at the shorter of LowerLeft and LowerRight
            walked = lowerLeft; // Take the shortest path to the top-left corner.
            walked += 1; // Then take a single step down, so be in the top-left corner
            while (walked < requiredSteps)
            {
                toggle = walked % 2 == 0 ? 0 : 1;
                totalReached += distancesTopLeft.Count(d => d % 2 == toggle && d <= (requiredSteps - walked));
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