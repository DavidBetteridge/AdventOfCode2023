namespace AOC_2023.Tests;

public class Day22Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Day22();
        Assert.Equal(5, solver.Part1("Day22/sample.txt"));
    }

    [Fact]
    public void Test_Part1()
    {
        var solver = new Day22();
        Assert.Equal(407, solver.Part1("Day22/input.txt"));
    }
    //
    // [Fact]
    // public void Test_Part2_Sample()
    // {
    //     var solver = new Day22();
    //     Assert.Equal(167409079868000, solver.Part2("Day22/sample.txt"));
    // }
    //
    // [Fact]
    // public void Test_Part2()
    // {
    //     var solver = new Day22();
    //     Assert.Equal(130090458884662, solver.Part2("Day22/input.txt"));
    // }
}