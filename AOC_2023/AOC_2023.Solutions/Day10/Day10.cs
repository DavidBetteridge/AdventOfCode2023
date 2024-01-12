using Microsoft.Diagnostics.Tracing.Parsers.Kernel;
using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftAntimalwareEngine;

namespace AOC_2023.Solutions;

public class Day10
{
    private const int NORTH = 1;
    private const int SOUTH = 2;
    private const int EAST = 3;
    private const int WEST = 4;
    
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
        if (direction == NORTH)
            currentLocation = (currentLocation.RowNumber - 1, currentLocation.ColumnNumber);
        else if (direction == SOUTH)
            currentLocation = (currentLocation.RowNumber + 1, currentLocation.ColumnNumber);
        else if (direction == EAST)
            currentLocation = (currentLocation.RowNumber, currentLocation.ColumnNumber + 1);
        else if (direction == WEST)
            currentLocation = (currentLocation.RowNumber, currentLocation.ColumnNumber - 1);
        var length = 1;

        while (currentLocation != startLocation)
        {
            var current = grid[currentLocation.RowNumber][currentLocation.ColumnNumber];
            if (current is '|' && direction == NORTH)
                direction = NORTH;
            else if (current is '|' && direction == SOUTH)
                direction = SOUTH;
            else if (current is '-' && direction == EAST)
                direction = EAST;
            else if (current is '-' && direction == WEST)
                direction = WEST;
            else if (current is 'F' && direction == NORTH)
                direction = EAST;
            else if (current is 'F' && direction == WEST)
                direction = SOUTH;
            else if (current is '7' && direction == NORTH)
                direction = WEST;
            else if (current is '7' && direction == EAST)
                direction = SOUTH;
            else if (current is 'L' && direction == WEST)
                direction = NORTH;
            else if (current is 'L' && direction == SOUTH)
                direction = EAST;
            else if (current is 'J' && direction == EAST)
                direction = NORTH;
            else if (current is 'J' && direction == SOUTH)
                direction = WEST;

            if (direction == NORTH)
                currentLocation = (currentLocation.RowNumber - 1, currentLocation.ColumnNumber);
            else if (direction == SOUTH)
                currentLocation = (currentLocation.RowNumber + 1, currentLocation.ColumnNumber);
            else if (direction == EAST)
                currentLocation = (currentLocation.RowNumber, currentLocation.ColumnNumber + 1);
            else if (direction == WEST)
                currentLocation = (currentLocation.RowNumber, currentLocation.ColumnNumber - 1);
            length += 1;
        }

        return length / 2;
    }

    public int Part2(string filename)
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
        if (direction == NORTH)
            currentLocation = (currentLocation.RowNumber - 1, currentLocation.ColumnNumber);
        else if (direction == SOUTH)
            currentLocation = (currentLocation.RowNumber + 1, currentLocation.ColumnNumber);
        else if (direction == EAST)
            currentLocation = (currentLocation.RowNumber, currentLocation.ColumnNumber + 1);
        else if (direction == WEST)
            currentLocation = (currentLocation.RowNumber, currentLocation.ColumnNumber - 1);

        var path = new Dictionary<(int,int), int>();

        while (currentLocation != startLocation)
        {
            var current = grid[currentLocation.RowNumber][currentLocation.ColumnNumber];
            if (current is '|' && direction == NORTH)
                direction = NORTH;
            else if (current is '|' && direction == SOUTH)
                direction = SOUTH;
            else if (current is '-' && direction == EAST)
                direction = EAST;
            else if (current is '-' && direction == WEST)
                direction = WEST;
            else if (current is 'F' && direction == NORTH)
                direction = EAST;
            else if (current is 'F' && direction == WEST)
                direction = SOUTH;
            else if (current is '7' && direction == NORTH)
                direction = WEST;
            else if (current is '7' && direction == EAST)
                direction = SOUTH;
            else if (current is 'L' && direction == WEST)
                direction = NORTH;
            else if (current is 'L' && direction == SOUTH)
                direction = EAST;
            else if (current is 'J' && direction == EAST)
                direction = NORTH;
            else if (current is 'J' && direction == SOUTH)
                direction = WEST;
            path.Add(currentLocation, direction);
            if (direction == NORTH)
                currentLocation = (currentLocation.RowNumber - 1, currentLocation.ColumnNumber);
            else if (direction == SOUTH)
                currentLocation = (currentLocation.RowNumber + 1, currentLocation.ColumnNumber);
            else if (direction == EAST)
                currentLocation = (currentLocation.RowNumber, currentLocation.ColumnNumber + 1);
            else if (direction == WEST)
                currentLocation = (currentLocation.RowNumber, currentLocation.ColumnNumber - 1);
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
            result += rowTotal;
        }
        return result;
    }

    private static int FindPossibleStartDirection((int RowNumber, int ColumnNumber) currentLocation,
        string[] grid)
    {
        if (currentLocation.RowNumber > 0)
        {
            var above = grid[currentLocation.RowNumber - 1][currentLocation.ColumnNumber];
            if (above is '|' or '7' or 'F')
                return NORTH;
        }

        if (currentLocation.ColumnNumber > 0)
        {
            var left = grid[currentLocation.RowNumber][currentLocation.ColumnNumber - 1];
            if (left is '-' or 'F' or 'L')
                return WEST;
        }

        if (currentLocation.RowNumber + 1 < grid.Length)
        {
            var below = grid[currentLocation.RowNumber + 1][currentLocation.ColumnNumber];
            if (below is '|' or 'L' or 'J')
                return SOUTH;
        }

        if (currentLocation.ColumnNumber + 1 < grid[currentLocation.RowNumber].Length)
        {
            var right = grid[currentLocation.RowNumber][currentLocation.ColumnNumber + 1];
            if (right is '-' or '7' or 'J')
                return EAST;
        }

        throw new Exception($"{currentLocation}");
    }
}