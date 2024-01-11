namespace AOC_2023.Tests;

public class Day05Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Day05();
        Assert.Equal(35, solver.Part1("Day05/sample.txt"));
    }

    [Fact]
    public void Test_Part1()
    {
        var solver = new Day05();
        Assert.Equal(31599214, solver.Part1("Day05/input.txt"));
    }

    [Fact]
    public void Test_Part2_Sample()
    {
        var solver = new Day05();
        Assert.Equal(46, solver.Part2("Day05/sample.txt"));
    }

    [Fact]
    public void Test_Part2()
    {
        var solver = new Day05();
        Assert.Equal(20358599, solver.Part2("Day05/input.txt"));
    }
}