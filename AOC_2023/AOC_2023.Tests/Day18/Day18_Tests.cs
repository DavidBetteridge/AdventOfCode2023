namespace AOC_2023.Tests;

public class Day18Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Day18();
        Assert.Equal(62, solver.Part1("Day18/sample.txt"));
    }

    [Fact]
    public void Test_Part1()
    {
        var solver = new Day18();
        Assert.Equal(48400, solver.Part1("Day18/input.txt"));
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