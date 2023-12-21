namespace AOC_2023.Tests;

public class Day21Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Day21();
        Assert.Equal(16, solver.Part1("Day21/sample.txt", 6));
    }

    [Fact]
    public void Test_Part1()
    {
        var solver = new Day21();
        Assert.Equal(3605, solver.Part1("Day21/input.txt", 64));   // Not 40
    }

    [Fact]
    public void Test_Part2_Sample()
    {
        var solver = new Day21();
        Assert.Equal(167409079868000, solver.Part2("Day21/sample.txt"));
    }
    
    [Fact]
    public void Test_Part2()
    {
        var solver = new Day21();
        Assert.Equal(130090458884662, solver.Part2("Day21/input.txt"));
    }
}