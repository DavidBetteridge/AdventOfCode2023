namespace AOC_2023.Solutions;

public class Day10
{
    private const int NORTH = 1;
    private const int SOUTH = 2;
    private const int EAST = 3;
    private const int WEST = 4;
    
    public int Part1(string filename)
    {
        var grid = File.ReadAllText(filename);

        var width = grid.IndexOf('\n')+1;
        var start = grid.IndexOf('S');
        
        var north = -width;
        var south = width;
        var east = 1;
        var west = -1;

        var direction = FindStartDirection(start);
        var currentLoc = start+direction;

        var length = 1;

        do
        {
            direction = grid[currentLoc] switch
            {
                'F' when direction == north => east,
                'F' when direction == west => south,
                '7' when direction == north => west,
                '7' when direction == east => south,
                'L' when direction == west => north,
                'L' when direction == south => east,
                'J' when direction == east => north,
                'J' when direction == south => west,
                _ => direction
            };

            currentLoc += direction;
            length += 1;
        } while (currentLoc != start);

        return length / 2;
        
        int FindStartDirection(int currentLocation)
        {
            if (currentLocation > width)
            {
                var above = grid[currentLocation - width];
                if (above is '|' or '7' or 'F')
                    return north;
            }

            if (currentLocation % width > 0)
            {
                var left = grid[currentLocation-1];
                if (left is '-' or 'F' or 'L')
                    return west;
            }

            if (currentLocation + width < grid.Length)
            {
                var below = grid[currentLocation + width];
                if (below is '|' or 'L' or 'J')
                    return south;
            }

            if ((currentLocation + 1) % width < width)
            {
                var right = grid[currentLocation+1];
                if (right is '-' or '7' or 'J')
                    return east;
            }

            throw new Exception($"{currentLocation}");
        }
    }

    public int Part2(string filename)
    {
         const int NORTH = 1;
         const int SOUTH = 2;
         const int EAST = 3;
         const int WEST = 4;
        
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
            direction = current switch
            {
                '|' when direction == NORTH => NORTH,
                '|' when direction == SOUTH => SOUTH,
                '-' when direction == EAST => EAST,
                '-' when direction == WEST => WEST,
                'F' when direction == NORTH => EAST,
                'F' when direction == WEST => SOUTH,
                '7' when direction == NORTH => WEST,
                '7' when direction == EAST => SOUTH,
                'L' when direction == WEST => NORTH,
                'L' when direction == SOUTH => EAST,
                'J' when direction == EAST => NORTH,
                'J' when direction == SOUTH => WEST,
                _ => direction
            };

            path.Add(currentLocation, direction);

            currentLocation = direction switch
            {
                NORTH => (currentLocation.RowNumber - 1, currentLocation.ColumnNumber),
                SOUTH => (currentLocation.RowNumber + 1, currentLocation.ColumnNumber),
                EAST => (currentLocation.RowNumber, currentLocation.ColumnNumber + 1),
                WEST => (currentLocation.RowNumber, currentLocation.ColumnNumber - 1),
                _ => currentLocation
            };
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