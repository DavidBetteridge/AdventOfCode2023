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
        var odds = distancesStartingInTheMiddle.Count(d => d % 2 == 1);
        var evens = distancesStartingInTheMiddle.Count(d => d % 2 == 0);
        
        // How many sub-grids can be reached
        var gridsUp = requiredSteps / rows;
        var gridsLeft = requiredSteps / cols;
        
        // How many steps are needed to reach a corner of the grid
        var topLeft = distancesStartingInTheMiddle[0];
        var topRight = distancesStartingInTheMiddle[cols-1];
        var lowerLeft = distancesStartingInTheMiddle[cols * (rows-1)];
        var lowerRight = distancesStartingInTheMiddle[(cols * rows) -1];
        
        // What are the distances from each corner
        var distancesTopLeft= CountPossibleSquares(requiredSteps, rows, cols, 0, 0, graph);
        var distancesTopRight= CountPossibleSquares(requiredSteps, rows, cols, cols-1, 0, graph);
        var distancesLowerLeft= CountPossibleSquares(requiredSteps, rows, cols, 0, rows-1, graph);
        var distancesLowerRight= CountPossibleSquares(requiredSteps, rows, cols, cols-1, rows-1, graph);

        // How many steps are required to reach the whole of the grid
        var maxTopLeft = distancesTopLeft.Where(d => d % 2 == 0).Max();
        var maxTopRight = distancesTopRight.Where(d => d % 2 == 0).Max();
        var maxLowerLeft = distancesLowerLeft.Where(d => d % 2 == 0).Max();
        var maxLowerRight = distancesLowerRight.Where(d => d % 2 == 0).Max();
        
        // How many steps are reachable
        var plotsTopLeft = distancesTopLeft.Count(d => d % 2 == 0);
        var plotsTopRight = distancesTopRight.Count(d => d % 2 == 0);
        var plotsLowerLeft = distancesLowerLeft.Count(d => d % 2 == 0);
        var plotsLowerRight = distancesLowerRight.Count(d => d % 2 == 0);
        
        
        //2559600
        var totalReached = 0L;
        for (var x = -gridsLeft; x <= gridsLeft; x++)
        {
            if (x != 0)
            {
                for (var y = -gridsUp; y <= gridsUp; y++)
                {
                    if (y != 0)
                    {
                        // The distance walked to reach the corner of the grid
                        var distanceWalked = Math.Abs(x * cols) + Math.Abs(y * rows);
                        if (x < 0 && y < 0)
                            distanceWalked += topLeft + 2;
                        if (x < 0 && y > 0)
                            distanceWalked += lowerLeft + 2;
                        if (x > 0 && y < 0)
                            distanceWalked += topRight + 2;
                        if (x > 0 && y > 0)
                            distanceWalked += lowerRight + 2;
                        
                        var stepsToCompleteGrid = 0;
                        if (x < 0 && y < 0)
                            stepsToCompleteGrid += maxTopLeft;
                        if (x < 0 && y > 0)
                            stepsToCompleteGrid += maxLowerLeft;
                        if (x > 0 && y < 0)
                            stepsToCompleteGrid += maxTopRight;
                        if (x > 0 && y > 0)
                            stepsToCompleteGrid += maxLowerRight;
                        
                        if ((distanceWalked + stepsToCompleteGrid) < requiredSteps)
                        {
                            totalReached += Math.Min(evens, odds);
                        }
                    }
                }
            }
        }
        
        return totalReached;
    }

    private (long odds, long evens) ReachableSquares(int requiredSteps, int rows, int cols, int startCol,
        int startRow, string[] graph)
    {
        var distances = CountPossibleSquares(requiredSteps, rows, cols, startCol, startRow, graph);
        var odds = distances.Count(d => d % 2 == 1);
        var evens = distances.Count(d => d % 2 == 0);
        return (odds, evens);
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