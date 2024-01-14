namespace AOC_2023.Solutions;

public class Day11
{
    public long Part1And2(int increase, string filename)
    {
        var lines = File.ReadAllLines(filename);
        var galaxiesRow = new List<uint>();
        var galaxiesColumn = new List<uint>();

        var numberOfRows = lines.Length;
        var numberOfColumns = lines[0].Length;
        var columnOffsets = new int[numberOfColumns];

        long movedDown = 0;
        for (var rowNumber = 0; rowNumber < numberOfRows; rowNumber++)
        {
            var populated = false;
            for (var columnNumber = 0; columnNumber < numberOfColumns; columnNumber++)
            {
                if (lines[rowNumber][columnNumber] == '#')
                {
                    galaxiesRow.Add((uint)(rowNumber + movedDown));
                    galaxiesColumn.Add((uint)columnNumber);
                    populated = true;
                    columnOffsets[columnNumber] = 1;
                }
            }

            if (!populated)
                movedDown += increase;
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

        var gs = galaxiesColumn.Count;
        var cols = galaxiesColumn.Select(r => r + (increase*columnOffsets[r])).Order().ToArray();
        
        var result = 0L;

        for (var i = 0; i < gs-1; i++)
        {
            result += galaxiesRow[i+1] * (i+1);
            result += -galaxiesRow[i] * (gs-i-1);
            result += cols[i+1] * (i+1);
            result += -cols[i] * (gs-i-1);
        }
        return result;
    }
}