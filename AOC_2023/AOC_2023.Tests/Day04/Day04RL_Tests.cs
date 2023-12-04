namespace AOC_2023.Tests;

public class Day04RLTests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Day04RL();
        Assert.Equal(13, solver.Part1("Day04/sample.txt"));
    }
    
    [Fact]
    public void Test_Part1()
    {
        var solver = new Day04RL();
        Assert.Equal(24706, solver.Part1("Day04/input.txt"));
    }
    
    [Fact]
    public void Test_Part2_Sample()
    {
        var solver = new Day04RL();
        Assert.Equal(30, solver.Part2("Day04/sample.txt"));
    }
    
    [Fact]
    public void Test_Part2()
    {
        var solver = new Day04RL();
        Assert.Equal(13114317, solver.Part2("Day04/input.txt"));
    }
}