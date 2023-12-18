namespace AOC_2023.Tests;

public class Day17Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Day17();
        Assert.Equal(102, solver.Part1("Day17/sample.txt"));
    }

    [Fact]
    public void Test_Part1()
    {
        var solver = new Day17();
        Assert.Equal(7111, solver.Part1("Day17/input.txt"));
    }

    // [Fact]
    // public void Test_Part2_Sample()
    // {
    //     var solver = new Day16();
    //     Assert.Equal(51, solver.Part2("Day16/sample.txt"));
    // }
    //
    // [Fact]
    // public void Test_Part2()
    // {
    //     var solver = new Day16();
    //     Assert.Equal(7831, solver.Part2("Day16/input.txt"));
    // }
}