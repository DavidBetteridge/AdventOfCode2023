namespace AOC_2023.Solutions;

public class Day11Parser
{
    private class Location
    {
        public long ColumnNumber { get; set; }
        public long RowNumber { get; set; }
    }
    
    public long Part1And2(int increase, string filename)
    {
        var lines = File.ReadAllLines(filename);
        var galaxies = new List<Location>();
        var parser = new LRParserSpan();

        var numberOfRows = lines.Length;
        var numberOfColumns = lines[0].Length;
        var populatedColumns =new bool[numberOfColumns];

        var movedDown = 0L;
        for (var rowNumber = 0; rowNumber < numberOfRows; rowNumber++)
        {
            parser.Reset(lines[rowNumber]);
            var columnNumber = 0;
            var populated = false;
            do
            {
                if (parser.EatChar() == '#')
                {
                    galaxies.Add(new Location { ColumnNumber = columnNumber, RowNumber = rowNumber+movedDown });
                    populated = true;
                    populatedColumns[columnNumber] = true;
                }

                columnNumber++;
            } while (!parser.EOF);

            if (!populated)
                movedDown+=increase;
        }
        
        // Expand empty columns
        var moved = 0L;
        for (var columnNumber = 0; columnNumber < numberOfColumns; columnNumber++)
        {
            if (!populatedColumns[columnNumber])
            {
                // This column is empty,  so any galaxy after this one needs to be moved right one space
                foreach (var galaxy in galaxies)
                {
                    if (galaxy.ColumnNumber > columnNumber+moved)
                        galaxy.ColumnNumber+=increase;
                }
                
                moved+=increase;  
            }
        }

        var result = 0L;
        for (var i = 0; i < galaxies.Count-1; i++)
        {
            for (var j = i+1; j < galaxies.Count; j++)
            {
                var distance = galaxies[j].RowNumber - galaxies[i].RowNumber +
                               Math.Abs(galaxies[j].ColumnNumber - galaxies[i].ColumnNumber);
                result += distance;
            }
        }
        
        return result;
    }
}