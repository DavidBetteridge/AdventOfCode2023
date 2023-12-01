namespace AOC_2023.Solutions;

public class Day01
{
    public int Part1(string filename) => 
        File.ReadAllLines(filename).Sum(line => int.Parse(line.First(char.IsDigit).ToString()) * 10 + 
                                                int.Parse(line.Last(char.IsDigit).ToString()));
    
}