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
        var grid = File.ReadAllText(filename);

        var width = grid.IndexOf('\n')+1;
        var height = grid.Length / width;
        var start = grid.IndexOf('S');
        
        var north = -width;
        var south = width;
        var east = 1;
        var west = -1;

        var direction = FindStartDirection(start);
        var currentLoc = start+direction;

        var length = 1;
        var path = new HashSet<int>();
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
            path.Add(currentLoc);
            currentLoc += direction;
            length += 1;
        } while (currentLoc != start);

        var result = 0;
        for (var rowNumber = 1; rowNumber < height - 1; rowNumber++)
        {
            var rowTotal = 0;
            var inSide = false;
            var previous = ' ';
            for (var columnNumber = 0; columnNumber < (width-1); columnNumber++)
            {
                var cell = (rowNumber * width) + columnNumber;
                var c = grid[cell];
             
                var onLoop = path.Contains(cell);
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

}