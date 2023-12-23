namespace AOC_2023.Tests;

public class Day23Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Day23();
        Assert.Equal(94, solver.Part1("Day23/sample.txt"));
    }

    [Fact]
    public void Test_Part1()
    {
        var solver = new Day23();
        Assert.Equal(3605, solver.Part1("Day23/input.txt"));
    }
    //
    // [Fact]
    // public void Test_Part2_Sample()
    // {
    //     var solver = new Day23();
    //     Assert.Equal(167409079868000, solver.Part2("Day23/sample.txt"));
    // }
    //
    // [Fact]
    // public void Test_Part2()
    // {
    //     var solver = new Day23();
    //     Assert.Equal(130090458884662, solver.Part2("Day23/input.txt"));
    // }
}