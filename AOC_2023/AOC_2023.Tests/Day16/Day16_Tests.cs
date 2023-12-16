namespace AOC_2023.Tests;

public class Day16Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Day16();
        Assert.Equal(46, solver.Part1("Day16/sample.txt"));
    }

    [Fact]
    public void Test_Part1()
    {
        var solver = new Day16();
        Assert.Equal(7111, solver.Part1("Day16/input.txt"));
    }

    [Fact]
    public void Test_Part2_Sample()
    {
        var solver = new Day16();
        Assert.Equal(51, solver.Part2("Day16/sample.txt"));
    }
    
    [Fact]
    public void Test_Part2()
    {
        var solver = new Day16();
        Assert.Equal(7831, solver.Part2("Day16/input.txt"));
    }
}