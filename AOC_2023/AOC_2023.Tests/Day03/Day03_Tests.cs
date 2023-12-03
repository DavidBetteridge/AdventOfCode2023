namespace AOC_2023.Tests;

public class Day03Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Day03();
        Assert.Equal(4361, solver.Part1("Day03/sample.txt"));
    }
    
    [Fact]
    public void Test_Part1()
    {
        var solver = new Day03();
        Assert.Equal(519444, solver.Part1("Day03/input.txt"));
    }
}