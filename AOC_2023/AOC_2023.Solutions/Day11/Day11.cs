namespace AOC_2023.Solutions;

public class Day11
{
    private class Location
    {
        public long ColumnNumber { get; set; }
        public long RowNumber { get; set; }
    }
    
    public long Part1And2(int increase, string filename)
    {
        var universe = File.ReadAllLines(filename);
        var galaxies = new List<Location>();
        
        // Find locations of all galaxies
        for (var rowNumber = 0; rowNumber < universe.Length; rowNumber++)
        {
            for (var columnNumber = 0; columnNumber < universe[0].Length; columnNumber++)
            {
                if (universe[rowNumber][columnNumber] == '#')
                    galaxies.Add(new Location{ColumnNumber=columnNumber, RowNumber = rowNumber});
            }
        }
        
        // Expand empty rows
        var movedDown = 0L;
        for (var rowNumber = 0; rowNumber < universe.Length; rowNumber++)
        {
            if (universe[rowNumber].IndexOf('#') == -1)
            {
                // This row is empty,  so any galaxy after this one needs to be moved down one space
                foreach (var galaxy in galaxies)
                {
                    if (galaxy.RowNumber > (rowNumber+movedDown))
                        galaxy.RowNumber+=increase;
                }
                movedDown+=increase;
            }
        }
        
        // Expand empty columns
        var moved = 0L;
        for (var columnNumber = 0; columnNumber < universe[0].Length; columnNumber++)
        {
            if (universe.All(row => row[columnNumber] != '#'))
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
                var distance = Math.Abs(galaxies[j].RowNumber - galaxies[i].RowNumber) +
                               Math.Abs(galaxies[j].ColumnNumber - galaxies[i].ColumnNumber);
                result += distance;
            }
        }
        
        return result;
    }
}