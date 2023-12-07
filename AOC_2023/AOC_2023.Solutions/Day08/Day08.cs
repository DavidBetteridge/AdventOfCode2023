namespace AOC_2023.Solutions;

public class Day08
{
    public int Part1(string filename)
    {
        var text = File.ReadAllText(filename);
        var lrParser = new LRParser(text);
        return 0;
    }
}