namespace AOC_2023.Solutions;

public class Day11Parser
{
   
    public long Part1And2(int increase, string filename)
    {
        var lines = File.ReadAllLines(filename);
        var galaxiesRow = new List<uint>();
        var galaxiesColumn = new List<uint>();

        var numberOfRows = lines.Length;
        var numberOfColumns = lines[0].Length;
        var columnOffsets = new int[numberOfColumns];
        var rowOffsets = new int[numberOfColumns];

        var movedDown = 0;
        for (var rowNumber = 0; rowNumber < numberOfRows; rowNumber++)
        {
            var populated = false;
            for (var columnNumber = 0; columnNumber < numberOfColumns; columnNumber++)
            {
                if (lines[rowNumber][columnNumber] == '#')
                {
                    galaxiesRow.Add((uint)rowNumber);
                    galaxiesColumn.Add((uint)columnNumber);
                    populated = true;
                    columnOffsets[columnNumber] = 1;
                }
            }

            rowOffsets[rowNumber] = movedDown;
            if (!populated)
                movedDown += 1;
        }
        
        var columnOffset = 0;
        for (var columnNumber = 0; columnNumber < numberOfColumns; columnNumber++)
        {
            if (columnOffsets[columnNumber] == 0)
            {
                columnOffsets[columnNumber] = columnOffset;
                columnOffset+=1;
            }
            else
            {
                columnOffsets[columnNumber] = columnOffset;
            }
        }

        var gs = galaxiesRow.Count;
        var rows = galaxiesRow.Select(r => r + (increase*rowOffsets[r])).ToArray();
        var cols = galaxiesColumn.Select(r => r + (increase*columnOffsets[r])).Order().ToArray();
        
        var result = 0L;

        for (var i = 0; i < gs-1; i++)
        {
            result += rows[i+1] * (i+1);
            result += -rows[i] * (gs-i-1);
            result += cols[i+1] * (i+1);
            result += -cols[i] * (gs-i-1);
        }
        return result;
    }
    
    
    public long Part1And2B(int increase, string filename)
    {
        var lines = File.ReadAllLines(filename);
        var galaxiesRow = new List<uint>();
        var galaxiesColumn = new List<uint>();

        var numberOfRows = lines.Length;
        var numberOfColumns = lines[0].Length;
        var columnOffsets = new int[numberOfColumns];
        var rowOffsets = new int[numberOfColumns];

        var movedDown = 0;
        for (var rowNumber = 0; rowNumber < numberOfRows; rowNumber++)
        {
            var populated = false;
            for (var columnNumber = 0; columnNumber < numberOfColumns; columnNumber++)
            {
                if (lines[rowNumber][columnNumber] == '#')
                {
                    galaxiesRow.Add((uint)rowNumber);
                    galaxiesColumn.Add((uint)columnNumber);
                    populated = true;
                    columnOffsets[columnNumber] = 1;
                }
            }

            rowOffsets[rowNumber] = movedDown;
            if (!populated)
                movedDown += 1;
        }
        
        var columnOffset = 0;
        for (var columnNumber = 0; columnNumber < numberOfColumns; columnNumber++)
        {
            if (columnOffsets[columnNumber] == 0)
            {
                columnOffsets[columnNumber] = columnOffset;
                columnOffset+=1;
            }
            else
            {
                columnOffsets[columnNumber] = columnOffset;
            }
        }

        var rows = galaxiesRow.Select(r => r + (increase*rowOffsets[r])).ToArray();
        var cols = galaxiesColumn.Select(r => r + (increase*columnOffsets[r])).ToArray();

        var result = 0L;
        var gs = rows.Length;
        for (var i = 0; i < gs-1; i++)
        {
            for (var j = i+1; j < gs; j++)
                result += rows[j] - rows[i] + Math.Abs(cols[i] - cols[j]);
        }
        return result;
    }
}