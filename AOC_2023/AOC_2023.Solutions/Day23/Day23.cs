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

    
    
    public long Part2(string filename)
    {
        var wood = File.ReadAllLines(filename);
        var rows = wood.Length;
        var cols = wood[0].Length;
        
        var startX = wood[0].IndexOf('.');
        var startY = 0;
        var startIndex = Index(startX, startY);
        
        var endX = wood[^1].IndexOf('.');
        var endY = wood.Length-1;
        var endIndex = Index(endX, endY);

        var seen = new bool[rows*cols];
        
        var graph = new List<(uint index, int weight)>[rows*cols];

        for (var r = 0; r < rows; r++)
        {
            for (var c = 0; c < cols; c++)
            {
                if (wood[r][c] != '#')
                {
                    graph[Index(c, r)] = new List<(uint index, int weight)>();
                    if (r > 0 && wood[r-1][c] != '#') graph[Index(c, r)].Add( (Index(c, r-1) ,1));
                    if (c > 0 && wood[r][c-1] != '#') graph[Index(c, r)].Add( (Index(c-1, r) ,1));
                    if (c+1 < cols && wood[r][c+1] != '#') graph[Index(c, r)].Add( (Index(c+1, r) ,1));
                    if (r+1 < rows && wood[r+1][c] != '#') graph[Index(c, r)].Add( (Index(c, r+1) ,1));
                }
            }
        }
        
        // Remove nodes with degree 2
        for (var r = 0; r < rows; r++)
        {
            for (var c = 0; c < cols; c++)
            {
                var ind = Index(c, r);
                if (graph[ind] != null && graph[ind].Count == 2)
                {
                    var lhs = graph[ind][0];
                    var rhs = graph[ind][1];

                    graph[lhs.index].RemoveAll(l => l.index == ind);
                    graph[rhs.index].RemoveAll(l => l.index == ind);
                    
                    graph[lhs.index].Add((rhs.index, lhs.weight+rhs.weight));
                    graph[rhs.index].Add((lhs.index, lhs.weight+rhs.weight));
                }
            }
        }


        return Walk(startIndex, 0);
        
        int Walk(long currentIndex, int numberOfSteps)
        {
            var result = 0;

            seen[currentIndex] = true;
   
            // Try all directions
            foreach (var dir in graph[currentIndex])
            {
                if (dir.index == endIndex)
                {
                    result = Math.Max(result,  numberOfSteps + dir.weight);
                }
                else
                {
                    if (!seen[dir.index])
                        result = Math.Max(result, Walk(dir.index, numberOfSteps + dir.weight));    
                }
                
            }
            
            seen[currentIndex] = false;
            
            return result;
        }
        
        uint Index(int col, int row)
        {
            return (uint)((row * cols) + col);
        }
        
    }
    
       public long Part2NoWalk(string filename)
    {
        var wood = File.ReadAllLines(filename);
        var rows = wood.Length;
        var cols = wood[0].Length;
        
        var startX = wood[0].IndexOf('.');
        var startY = 0;
        var startIndex = Index(startX, startY);
        
        var endX = wood[^1].IndexOf('.');
        var endY = wood.Length-1;
        var endIndex = Index(endX, endY);

        var seen = new bool[rows*cols];
        
        var graph = new List<(long index, int weight)>[rows*cols];

        for (var r = 0; r < rows; r++)
        {
            for (var c = 0; c < cols; c++)
            {
                if (wood[r][c] != '#')
                {
                    graph[Index(c, r)] = new List<(long index, int weight)>();
                    if (r > 0 && wood[r-1][c] != '#') graph[Index(c, r)].Add( (Index(c, r-1) ,1));
                    if (c > 0 && wood[r][c-1] != '#') graph[Index(c, r)].Add( (Index(c-1, r) ,1));
                    if (c+1 < cols && wood[r][c+1] != '#') graph[Index(c, r)].Add( (Index(c+1, r) ,1));
                    if (r+1 < rows && wood[r+1][c] != '#') graph[Index(c, r)].Add( (Index(c, r+1) ,1));
                }
            }
        }
        
        // Remove nodes with degree 2
        for (var r = 0; r < rows; r++)
        {
            for (var c = 0; c < cols; c++)
            {
                var ind = Index(c, r);
                if (graph[ind] != null && graph[ind].Count == 2)
                {
                    var lhs = graph[ind][0];
                    var rhs = graph[ind][1];

                    graph[lhs.index].RemoveAll(l => l.index == ind);
                    graph[rhs.index].RemoveAll(l => l.index == ind);
                    
                    graph[lhs.index].Add((rhs.index, lhs.weight+rhs.weight));
                    graph[rhs.index].Add((lhs.index, lhs.weight+rhs.weight));
                }
            }
        }


        return 0;
        
        
        long Index(int col, int row)
        {
            return (row * cols) + col;
        }
        
    }
    
       
    public long Part2NoMerge(string filename)
    {
        var wood = File.ReadAllLines(filename);
        var rows = wood.Length;
        var cols = wood[0].Length;
        
        var startX = wood[0].IndexOf('.');
        var startY = 0;
        var startIndex = Index(startX, startY);
        
        var endX = wood[^1].IndexOf('.');
        var endY = wood.Length-1;
        var endIndex = Index(endX, endY);

        var seen = new bool[rows*cols];
        
        var graph = new List<(long index, int weight)>[rows*cols];

        for (var r = 0; r < rows; r++)
        {
            for (var c = 0; c < cols; c++)
            {
                if (wood[r][c] != '#')
                {
                    graph[Index(c, r)] = new List<(long index, int weight)>();
                    if (r > 0 && wood[r-1][c] != '#') graph[Index(c, r)].Add( (Index(c, r-1) ,1));
                    if (c > 0 && wood[r][c-1] != '#') graph[Index(c, r)].Add( (Index(c-1, r) ,1));
                    if (c+1 < cols && wood[r][c+1] != '#') graph[Index(c, r)].Add( (Index(c+1, r) ,1));
                    if (r+1 < rows && wood[r+1][c] != '#') graph[Index(c, r)].Add( (Index(c, r+1) ,1));
                }
            }
        }
       

        return 0;
        
        
        long Index(int col, int row)
        {
            return (row * cols) + col;
        }
        
    }
    
    
    public long Part2NoGraph(string filename)
    {
        var wood = File.ReadAllLines(filename);
        var rows = wood.Length;
        var cols = wood[0].Length;
        
        var startX = wood[0].IndexOf('.');
        var startY = 0;
        var startIndex = Index(startX, startY);
        
        var endX = wood[^1].IndexOf('.');
        var endY = wood.Length-1;
        var endIndex = Index(endX, endY);

        var seen = new bool[rows*cols];
        
        var graph = new List<(long index, int weight)>[rows*cols];

        return 0;
        
        
        long Index(int col, int row)
        {
            return (row * cols) + col;
        }
        
    }
}