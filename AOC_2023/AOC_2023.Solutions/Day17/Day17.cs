namespace AOC_2023.Solutions;

public class Day17
{
    public record Link
    {
        public int Node { get; set; }
        public int Weight { get; set; }
    }
    public long Part1(string filename)
    {
        var weights = File.ReadAllLines(filename)
                            .Select(line => line.Select(c => c - '0').ToArray())
                            .ToArray();

        var numberOfRows = weights.Length;
        var numberOfColumns = weights[0].Length;
        var numberOfCells = numberOfColumns * numberOfRows;
        
        // Horizontal nodes are.....  col + row * number of cols
        // Vertical nodes are ..... col  + (row * number of cols) + (number of rows * number of cols)
        // but we are really interested in the links between nodes.
        // Mapping Source -> Targets x 6 (Node, Weight)
        var numberOfNodes = (numberOfCells * 2);
        var graph = new Link?[numberOfNodes, 6];
        
        for (var row = 0; row < numberOfRows; row++)
        {
            for (var column = 0; column < numberOfColumns; column++)
            {
                if ((row + 1 == numberOfRows) && (column + 1 == numberOfColumns))
                    continue;
                
                // Horizontal node
                var sourceH = column + (row * numberOfColumns);
                
                // Going right....
                var weight = 0;
                for (var length = 1; length <= 3; length++)
                {
                    if (column + 1 + length <= numberOfColumns)
                    {
                        var target = (column + length) + (row * numberOfColumns) + numberOfCells;
                        if (target == numberOfNodes - 1) target = numberOfCells - 1;
                        weight += weights[row][column + length];
                        graph[sourceH, length - 1] = new Link { Node = target, Weight = weight };
                    }
                }
                
                // Going left....
                weight = 0;
                for (var length = 1; length <= 3; length++)
                {
                    if (column - length >= 0)
                    {
                        var target = (column-length) + (row * numberOfColumns) + numberOfCells;
                        weight += weights[row][column - length];
                        graph[sourceH, 3 + length - 1] = new Link { Node = target, Weight = weight };
                    }
                }
                
                // Vertical node
                var sourceV = column + (row * numberOfColumns) + numberOfCells;
                
                // Going down....
                weight = 0;
                for (var length = 1; length <= 3; length++)
                {
                    if (row + 1 + length <= numberOfRows)
                    {
                        var target = column + ((row + length)  * numberOfColumns);
                        if (target == numberOfNodes - 1) target = numberOfCells - 1;
                        weight += weights[row + length][column];
                        graph[sourceV, length - 1] = new Link { Node = target, Weight = weight };
                    }
                }
                
                // Going up....
                weight = 0;
                for (var length = 1; length <= 3; length++)
                {
                    if (row - length >= 0)
                    {
                        var target = column + ((row - length)  * numberOfColumns);
                        weight += weights[row - length][column];
                        graph[sourceV, 3 + length - 1] = new Link { Node = target, Weight = weight };
                    }
                }
            }
        }

        // bit hacky,  but we can also go down from the first node
        var w = 0;
        for (var length = 1; length <= 3; length++)
        {
            if (1 + length <= numberOfRows)
            {
                var target = 0 + (length * numberOfColumns);
                w += weights[length][0];
                graph[0, 3+length - 1] = new Link { Node = target, Weight = w };
            }
        }
        
        
        var distances = new int[numberOfNodes-1];
        Array.Fill(distances, int.MaxValue);
        distances[0] = 0;

        var seen = new bool[numberOfNodes-1];

        for (var n = 0; n < numberOfNodes-1; n++)
        {
            var v = FindSmallest(seen, distances);
            seen[v] = true;

            for (var u = 0; u < 6; u++)
            {
                var link = graph[v, u];
                if (link is not null)
                {
                    if ((distances[v] + link.Weight) < distances[link.Node])
                    {
                        distances[link.Node] = distances[v] + link.Weight;
                    }
                }
            }
        }
        
        
        return distances[numberOfCells - 1];
    }

    private int FindSmallest(bool[] seen, int[] distances)
    {
        var smallestCost = int.MaxValue-1;
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