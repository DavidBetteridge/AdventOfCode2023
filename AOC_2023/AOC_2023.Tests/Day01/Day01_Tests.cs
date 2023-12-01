namespace AOC_2023.Tests;

public class Day01Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Day01();
        Assert.Equal(142, solver.Part1("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day01/part1_sample.txt"));
    }
    
    [Fact]
    public void Test_Part1()
    {
        var solver = new Day01();
        Assert.Equal(56049, solver.Part1("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day01/part1.txt"));
    }
}