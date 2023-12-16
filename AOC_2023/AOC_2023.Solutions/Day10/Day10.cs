using Microsoft.Diagnostics.Tracing.Parsers.Kernel;
using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftAntimalwareEngine;

namespace AOC_2023.Solutions;

public class Day10
{
    public int Part1(string filename)
    {
        var grid = File.ReadAllLines(filename);

        // Find the start position (row,column)
        var startLocation = (RowNumber: 0, ColumnNumber: 0);
        for (var rowNumber = 0; rowNumber < grid.Length; rowNumber++)
        {
            var columnNumber = grid[rowNumber].IndexOf('S');
            if (columnNumber != -1)
            {
                startLocation = (rowNumber, columnNumber);
                break;
            }
        }

        var direction = FindPossibleStartDirection(startLocation, grid);
        var currentLocation = startLocation;
        if (direction == "NORTH")
            currentLocation = (currentLocation.RowNumber - 1, currentLocation.ColumnNumber);
        else if (direction == "SOUTH")
            currentLocation = (currentLocation.RowNumber + 1, currentLocation.ColumnNumber);
        else if (direction == "EAST")
            currentLocation = (currentLocation.RowNumber, currentLocation.ColumnNumber + 1);
        else if (direction == "WEST")
            currentLocation = (currentLocation.RowNumber, currentLocation.ColumnNumber - 1);
        var length = 1;

        while (currentLocation != startLocation)
        {
            var current = grid[currentLocation.RowNumber][currentLocation.ColumnNumber];
            if (current is '|' && direction == "NORTH")
                direction = "NORTH";
            else if (current is '|' && direction == "SOUTH")
                direction = "SOUTH";
            else if (current is '-' && direction == "EAST")
                direction = "EAST";
            else if (current is '-' && direction == "WEST")
                direction = "WEST";
            else if (current is 'F' && direction == "NORTH")
                direction = "EAST";
            else if (current is 'F' && direction == "WEST")
                direction = "SOUTH";
            else if (current is '7' && direction == "NORTH")
                direction = "WEST";
            else if (current is '7' && direction == "EAST")
                direction = "SOUTH";
            else if (current is 'L' && direction == "WEST")
                direction = "NORTH";
            else if (current is 'L' && direction == "SOUTH")
                direction = "EAST";
            else if (current is 'J' && direction == "EAST")
                direction = "NORTH";
            else if (current is 'J' && direction == "SOUTH")
                direction = "WEST";

            if (direction == "NORTH")
                currentLocation = (currentLocation.RowNumber - 1, currentLocation.ColumnNumber);
            else if (direction == "SOUTH")
                currentLocation = (currentLocation.RowNumber + 1, currentLocation.ColumnNumber);
            else if (direction == "EAST")
                currentLocation = (currentLocation.RowNumber, currentLocation.ColumnNumber + 1);
            else if (direction == "WEST")
                currentLocation = (currentLocation.RowNumber, currentLocation.ColumnNumber - 1);
            length += 1;
        }

        return length / 2;
    }

    public int Part2(string filename)
    {
        var grid = File.ReadAllLines(filename);
        var numberOfCols = grid[0].Length;

        // Find the start position (row,column)
        var startLocation = (RowNumber: 0, ColumnNumber: 0);
        for (var rowNumber = 0; rowNumber < grid.Length; rowNumber++)
        {
            var columnNumber = grid[rowNumber].IndexOf('S');
            if (columnNumber != -1)
            {
                startLocation = (rowNumber, columnNumber);
                break;
            }
        }

        var direction = FindPossibleStartDirection(startLocation, grid);
        var currentLocation = startLocation;
        if (direction == "NORTH")
            currentLocation = (currentLocation.RowNumber - 1, currentLocation.ColumnNumber);
        else if (direction == "SOUTH")
            currentLocation = (currentLocation.RowNumber + 1, currentLocation.ColumnNumber);
        else if (direction == "EAST")
            currentLocation = (currentLocation.RowNumber, currentLocation.ColumnNumber + 1);
        else if (direction == "WEST")
            currentLocation = (currentLocation.RowNumber, currentLocation.ColumnNumber - 1);

        var path = new Dictionary<(int,int), string>();

        while (currentLocation != startLocation)
        {
            var current = grid[currentLocation.RowNumber][currentLocation.ColumnNumber];
            if (current is '|' && direction == "NORTH")
                direction = "NORTH";
            else if (current is '|' && direction == "SOUTH")
                direction = "SOUTH";
            else if (current is '-' && direction == "EAST")
                direction = "EAST";
            else if (current is '-' && direction == "WEST")
                direction = "WEST";
            else if (current is 'F' && direction == "NORTH")
                direction = "EAST";
            else if (current is 'F' && direction == "WEST")
                direction = "SOUTH";
            else if (current is '7' && direction == "NORTH")
                direction = "WEST";
            else if (current is '7' && direction == "EAST")
                direction = "SOUTH";
            else if (current is 'L' && direction == "WEST")
                direction = "NORTH";
            else if (current is 'L' && direction == "SOUTH")
                direction = "EAST";
            else if (current is 'J' && direction == "EAST")
                direction = "NORTH";
            else if (current is 'J' && direction == "SOUTH")
                direction = "WEST";
            path.Add(currentLocation, direction);
            if (direction == "NORTH")
                currentLocation = (currentLocation.RowNumber - 1, currentLocation.ColumnNumber);
            else if (direction == "SOUTH")
                currentLocation = (currentLocation.RowNumber + 1, currentLocation.ColumnNumber);
            else if (direction == "EAST")
                currentLocation = (currentLocation.RowNumber, currentLocation.ColumnNumber + 1);
            else if (direction == "WEST")
                currentLocation = (currentLocation.RowNumber, currentLocation.ColumnNumber - 1);
        }

        for (var rowNumber = 0; rowNumber < grid.Length; rowNumber++)
        {
            for (var columnNumber = 0; columnNumber < grid[0].Length; columnNumber++)
            {
                if (path.ContainsKey((rowNumber, columnNumber)))
                {
                    Console.Write('#');
                }
                else
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }
        
        var result = 0;
        for (var rowNumber = 1; rowNumber < grid.Length - 1; rowNumber++)
        {
            var rowTotal = 0;
            var inSide = false;
            var previous = ' ';
            for (var columnNumber = 0; columnNumber < grid[0].Length; columnNumber++)
            {
                var c = grid[rowNumber][columnNumber];
             
                var onLoop = path.ContainsKey((rowNumber, columnNumber));
                if (!onLoop && inSide)
                {
                    rowTotal += 1;
                }
                else if (onLoop)
                {
                    //S==7
                    switch (c)
                    {
                        case '-':
                            break;
                        case '|':
                            inSide = !inSide;
                            break;
                        case '7':
                            if (previous != 'F' && previous != 'S')  // Hard coded!
                                inSide = !inSide;
                            break;
                        case 'J':
                            if (previous != 'L')
                                inSide = !inSide;
                            break;
                    }
                }
                if (c != '-') previous = c;
            }
            Console.WriteLine($"{rowNumber} {rowTotal}");
            result += rowTotal;
        }
        return result;


 
        //
        // // Left wall - first odd block you cross
        //
        //
        // var ins = new Dictionary<(int,int),bool>();
        // var result = 0;
        // for (var rowNumber = 1; rowNumber < grid.Length - 1; rowNumber++)
        // {
        //     var columnNumber = 0;
        //     while (columnNumber < grid[0].Length - 1)
        //     {
        //         if (path.ContainsKey((rowNumber, columnNumber)) &&
        //             !path.ContainsKey((rowNumber, columnNumber + 1)))
        //         {
        //
        //             var toMark = new Dictionary<(int,int),bool>();
        //             var c = 0;
        //             do
        //             {
        //                 columnNumber++;
        //                 if (!path.ContainsKey((rowNumber, columnNumber)))
        //                 {
        //                     toMark.Add((rowNumber, columnNumber), c%2==0);
        //                     c++;
        //                 }
        //             } while ( columnNumber < grid[0].Length &&
        //                       (path.ContainsKey((rowNumber, columnNumber - 1)) ||
        //                        !path.ContainsKey((rowNumber, columnNumber)))
        //                     );
        //
        //             if (columnNumber < grid[0].Length)
        //             {
        //                 foreach (var kv in toMark)
        //                 {
        //                     ins.Add(kv.Key, kv.Value);
        //                 }
        //                 result += c;
        //             }
        //
        //             
        //         }
        //
        //         columnNumber++;
        //     }
        // }
        //
        // for (var rowNumber = 0; rowNumber < grid.Length; rowNumber++)
        // {
        //     for (var columnNumber = 0; columnNumber < grid[0].Length; columnNumber++)
        //     {
        //         if (path.ContainsKey((rowNumber, columnNumber)))
        //         {
        //             Console.Write('#');
        //         }
        //         else if (ins.ContainsKey((rowNumber, columnNumber)))
        //         {
        //             Console.Write( ins[(rowNumber, columnNumber)] ? "E" : "O" );
        //         }
        //         else
        //         {
        //             Console.Write(" ");
        //         }
        //     }
        //     Console.WriteLine();
        // }


        // var up = path.ContainsKey((rowNumber, columnNumber - 1));
        // var down = path.ContainsKey((rowNumber, columnNumber + 1));
        //
        // if (path.ContainsKey((rowNumber, columnNumber)) && ( up && down ) ) 
        // {
        //     // First cross point.
        //     var first = columnNumber;
        //     do
        //     {
        //         columnNumber++;
        //     } while (
        //         !(path.ContainsKey((rowNumber, columnNumber)) &&
        //         (up && path.ContainsKey((rowNumber, columnNumber+1)) ||
        //          down && path.ContainsKey((rowNumber, columnNumber-1)))
        //         ));
        //
        //     result += columnNumber - first + 1;
        //     Console.WriteLine($"{rowNumber} {result}");
        //         }
        //         columnNumber++;
        //     }
        // }


        //
        //
        // // When you walk north,  count the gaps between you and the next wall
        // var result = 0;

        // foreach (var kv in path)
        // {
        //     if (kv.Value == "NORTH")
        //     {
        //         // Find the next entry to the east
        //         var rowNumber = kv.Key.Item1;
        //         var columnNumber = kv.Key.Item2+1;
        //         
        //         while (columnNumber < numberOfCols && !path.ContainsKey((rowNumber, columnNumber)))
        //             columnNumber++;
        //
        //         if (columnNumber < numberOfCols)
        //             result += columnNumber - kv.Key.Item2-1;
        //         // else
        //         // {
        //         //     columnNumber = kv.Key.Item2-1;
        //         //     while (columnNumber > 0 && !path.ContainsKey((rowNumber, columnNumber)))
        //         //         columnNumber--;
        //         //     result += kv.Key.Item2 - columnNumber - 1;
        //         // }
        //     }
        // }

        // return result;

        // var result = 0;
        // var heights = new int[grid[0].Length];
        // for (var rowNumber = 0; rowNumber < grid.Length; rowNumber++)
        // {
        //     var lines = 0;
        //     for (var columnNumber = 0; columnNumber < grid[0].Length; columnNumber++)
        //     {
        //         if (path.Contains((rowNumber, columnNumber)))
        //         {
        //             lines++;
        //             heights[columnNumber]++;
        //         }
        //         else
        //         {
        //             if (lines % 2 == 1)
        //                 result++;
        //         }
        //     }
        // }

        // return result;  // 435 too high

        //
        // var inside = new HashSet<(int,int)>();
        // var outside = new HashSet<(int,int)>();
        // for (var rowNumber = 0; rowNumber < grid.Length; rowNumber++)
        // {
        //     for (var columnNumber = 0; columnNumber < grid[0].Length; columnNumber++)
        //     {
        //         if (!path.Contains((rowNumber, columnNumber)) &&
        //             !inside.Contains((rowNumber, columnNumber)) &&
        //             !outside.Contains((rowNumber, columnNumber)))
        //         {
        //             // Flood fill from here checking to see if we reach the outside.  If we do,
        //             // then mark all nodes as outside,  otherwise mark them all as inside.
        //             var seen = new HashSet<(int,int)>();
        //             var queue = new Queue<(int,int)>();
        //             var isOutside = false;
        //             queue.Enqueue((rowNumber, columnNumber));
        //
        //             while (queue.Any())
        //             {
        //                 // Look up
        //                 if (rowNumber == 0)
        //                     isOutside = true;
        //                 else if (!path.Contains((rowNumber-1, columnNumber)) && !seen.Contains((rowNumber-1, columnNumber)))
        //                         queue.Enqueue((rowNumber-1, columnNumber));
        //
        //                 // Look left
        //                 if (columnNumber == 0)
        //                     isOutside = true;
        //                 else if (!path.Contains((rowNumber, columnNumber-1)) && !seen.Contains((rowNumber, columnNumber-1)))
        //                     queue.Enqueue((rowNumber, columnNumber-1));
        //                 
        //                 // Look up
        //                 if (rowNumber+1 == grid.Length)
        //                     isOutside = true;
        //                 else if (!path.Contains((rowNumber+1, columnNumber)) && !seen.Contains((rowNumber+1, columnNumber)))
        //                     queue.Enqueue((rowNumber+1, columnNumber));
        //                 
        //                 // Look right
        //                 if (columnNumber+1 == grid[0].Length)
        //                     isOutside = true;
        //                 else if (!path.Contains((rowNumber, columnNumber+1)) && !seen.Contains((rowNumber, columnNumber+1)))
        //                     queue.Enqueue((rowNumber, columnNumber+1));
        //                 
        //                 seen.Add((rowNumber, columnNumber));
        //             }
        //             
        //             if (isOutside)
        //                 outside.UnionWith(seen);
        //             else
        //                 inside.UnionWith(seen);
        //         }
        //     }
        // }
        //
        // for (var rowNumber = 0; rowNumber < grid.Length; rowNumber++)
        // {
        //     for (var columnNumber = 0; columnNumber < grid[0].Length; columnNumber++)
        //     {
        //         if (path.Contains((rowNumber,columnNumber)))
        //             Console.Write("*");
        //         else
        //             Console.Write(grid[rowNumber][columnNumber]);
        //     }
        //     Console.WriteLine();
        // }
        //
        // return result;
    }

    private static string FindPossibleStartDirection((int RowNumber, int ColumnNumber) currentLocation,
        string[] grid)
    {
        if (currentLocation.RowNumber > 0)
        {
            var above = grid[currentLocation.RowNumber - 1][currentLocation.ColumnNumber];
            if (above is '|' or '7' or 'F')
                return "NORTH";
        }

        if (currentLocation.ColumnNumber > 0)
        {
            var left = grid[currentLocation.RowNumber][currentLocation.ColumnNumber - 1];
            if (left is '-' or 'F' or 'L')
                return "WEST";
        }

        if (currentLocation.RowNumber + 1 < grid.Length)
        {
            var below = grid[currentLocation.RowNumber + 1][currentLocation.ColumnNumber];
            if (below is '|' or 'L' or 'J')
                return "SOUTH";
        }

        if (currentLocation.ColumnNumber + 1 < grid[currentLocation.RowNumber].Length)
        {
            var right = grid[currentLocation.RowNumber][currentLocation.ColumnNumber + 1];
            if (right is '-' or '7' or 'J')
                return "EAST";
        }

        throw new Exception($"{currentLocation}");
    }
}