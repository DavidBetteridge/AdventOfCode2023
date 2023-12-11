namespace AOC_2023.Tests;

public class Day11Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Day11();
        Assert.Equal(374, solver.Part1("Day11/sample.txt"));
    }

    [Fact]
    public void Test_Part1()
    {
        var solver = new Day11();
        Assert.Equal(374, solver.Part1("Day11/input.txt"));
    }
    
    // [Fact]
    // public void Test_Part2_Sample()
    // {
    //     var solver = new Day11();
    //     Assert.Equal(10, solver.Part2("Day11/sample2.txt"));
    // }
    //
    // [Fact]
    // public void Test_Part2()
    // {
    //     var solver = new Day11();
    //     Assert.Equal(948, solver.Part2("Day11/input.txt"));
    // }
}