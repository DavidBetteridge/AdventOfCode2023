namespace AOC_2023.Tests;

public class Day01Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Day01();
        Assert.Equal(142, solver.Part1("Day01/part1_sample.txt"));
    }
    
    [Fact]
    public void Test_Part1()
    {
        var solver = new Day01();
        Assert.Equal(56049, solver.Part1("Day01/input.txt"));
    }
    
    [Fact]
    public void Test_Part2_Sample()
    {
        var solver = new Day01();
        Assert.Equal(281, solver.Part2("Day01/part2_sample.txt"));
    }
    
    [Fact]
    public void Test_Part2()
    {
        var solver = new Day01();
        Assert.Equal(54530, solver.Part2("Day01/input.txt"));
    }
}