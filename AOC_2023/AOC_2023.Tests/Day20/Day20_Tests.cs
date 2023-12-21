namespace AOC_2023.Tests;

public class Day20Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Day20();
        Assert.Equal(32000000, solver.Part1("Day20/sample.txt"));
    }

    [Fact]
    public void Test_Part1_Sample2()
    {
        var solver = new Day20();
        Assert.Equal(11687500, solver.Part1("Day20/sample2.txt"));
    }
    
    [Fact]
    public void Test_Part1()
    {
        var solver = new Day20();
        Assert.Equal(446517, solver.Part1("Day20/input.txt"));
    }

    [Fact]
    public void Test_Part2_Sample()
    {
        var solver = new Day20();
        Assert.Equal(167409079868000, solver.Part2("Day20/sample.txt"));
    }
    
    [Fact]
    public void Test_Part2()
    {
        var solver = new Day20();
        Assert.Equal(130090458884662, solver.Part2("Day20/input.txt"));
    }
}