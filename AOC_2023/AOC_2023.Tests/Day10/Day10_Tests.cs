namespace AOC_2023.Tests;

public class Day10Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Day10();
        Assert.Equal(114, solver.Part1("Day0/sample.txt"));
    }

    [Fact]
    public void Test_Part1()
    {
        var solver = new Day10();
        Assert.Equal(1938731307, solver.Part1("Day10/input.txt"));
    }
    
    // [Fact]
    // public void Test_Part2_Sample()
    // {
    //     var solver = new Day10();
    //     Assert.Equal(2, solver.Part2("Day10/sample.txt"));
    // }
    //
    // [Fact]
    // public void Test_Part2()
    // {
    //     var solver = new Day10();
    //     Assert.Equal(948, solver.Part2("Day10/input.txt"));
    // }
}