namespace AOC_2023.Solutions;

public class Day23
{
    public long Part1(string filename)
    {
        var wood = File.ReadAllLines(filename);
        var rows = wood.Length;
        var cols = wood[0].Length;
        
        var startX = wood[0].IndexOf('.');
        var startY = 0;
        
        var endX = wood[^1].IndexOf('.');
        var endY = wood.Length-1;

        var seen = new bool[rows, cols];
        
        return Walk(startX, startY, 0);
        
        int Walk(int x, int y, int numberOfSteps)
        {
            var result = 0;
            
            if (x < 0 || x >= cols) return -4;
            if (y < 0 || y >= rows) return -5;
            if (wood[y][x] == '#') return -3;
            
            if (x == endX && y == endY)
            {
                return numberOfSteps;
            }

            // Console.WriteLine($"{x}, {y} {numberOfSteps}");
            
            if (seen[y, x]) 
                return -2;

            seen[y, x] = true;
            
            //If we are on a slope,  then we must follow it.
            if (wood[y][x] == '>')
                result = Walk(x + 1, y, numberOfSteps + 1);
            else if (wood[y][x] == '<')
                result =  Walk(x - 1, y, numberOfSteps + 1);
            else if (wood[y][x] == 'v')
                result =  Walk(x, y+1, numberOfSteps + 1);
            else if (wood[y][x] == '^')
                result =  Walk(x, y-1, numberOfSteps + 1);
            else
            {
                // Try all directions
                result = Walk(x + 1, y, numberOfSteps + 1);
                result = Math.Max(result, Walk(x - 1, y, numberOfSteps + 1));
                result = Math.Max(result, Walk(x, y+1, numberOfSteps + 1));
                result = Math.Max(result, Walk(x, y-1, numberOfSteps + 1));
            }
            
            seen[y, x] = false;
            
            return result;
        }
        
    }


}