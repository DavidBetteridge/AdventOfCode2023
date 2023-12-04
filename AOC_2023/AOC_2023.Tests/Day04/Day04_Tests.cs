namespace AOC_2023.Tests;

public class Day04Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Day04();
        Assert.Equal(13, solver.Part1("Day04/sample.txt"));
    }
    
    [Fact]
    public void Test_Part1()
    {
        var solver = new Day04();
        Assert.Equal(24706, solver.Part1("Day04/input.txt"));
    }
    
    // [Fact]
    // public void Test_Part2_Sample()
    // {
    //     var solver = new Day04();
    //     Assert.Equal(467845, solver.Part2("Day04/sample.txt"));
    // }
    //
    // [Fact]
    // public void Test_Part2()
    // {
    //     var solver = new Day04();
    //     Assert.Equal(74528807, solver.Part2("Day04/input.txt"));
    // }
}