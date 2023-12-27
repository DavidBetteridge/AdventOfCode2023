namespace AOC_2024.Tests;

public class Day24Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Day24();
        Assert.Equal(2, solver.Part1("Day24/sample.txt",7,27));
    }

    [Fact]
    public void Test_Part1()
    {
        var solver = new Day24();
        Assert.Equal(11098, solver.Part1("Day24/input.txt",200000000000000,400000000000000));
    }
    
    // [Fact]
    // public void Test_Part2_Sample()
    // {
    //     var solver = new Day24();
    //     Assert.Equal(154, solver.Part2("Day24/sample.txt"));
    // }
    //
    // [Fact]
    // public void Test_Part2()
    // {
    //     var solver = new Day24();
    //     Assert.Equal(6898, solver.Part2("Day24/input.txt"));
    // }
}