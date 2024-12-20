namespace AOC_2023.Tests;

public class Day19Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Day19();
        Assert.Equal(19114, solver.Part1("Day19/sample.txt"));
    }

    [Fact]
    public void Test_Part1()
    {
        var solver = new Day19();
        Assert.Equal(446517, solver.Part1("Day19/input.txt"));
    }

    [Fact]
    public void Test_Part2_Sample()
    {
        var solver = new Day19();
        Assert.Equal(167409079868000, solver.Part2("Day19/sample.txt"));
    }
    
    [Fact]
    public void Test_Part2()
    {
        var solver = new Day19();
        Assert.Equal(130090458884662, solver.Part2("Day19/input.txt"));
    }
}