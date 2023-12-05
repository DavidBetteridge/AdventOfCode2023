namespace AOC_2023.Solutions;

public class Day05
{
    public int Part1(string filename)
    {
        var result = 0;
        var lines = File.ReadAllLines(filename);
        
        var lrParser = new LRParser();
        return result;
    }

    private int Lookup(List<Map> maps, int value)
    {
        foreach (var map in maps)
        {
            if (value >= map.Source)
            {
                var offset = value - map.Source;
                if (offset <= map.Length)
                    return map.Destination + offset;
            }
        }

        return value;
    }

    private record Map
    {
        public int Source { get; set; }
        public int Destination { get; set; }
        public int Length { get; set; }
    }
    
}