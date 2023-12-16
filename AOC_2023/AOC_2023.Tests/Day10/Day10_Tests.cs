namespace AOC_2023.Tests;

public class Day10Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Day10();
        Assert.Equal(8, solver.Part1("Day10/sample.txt"));
    }

    [Fact]
    public void Test_Part1()
    {
        var solver = new Day10();
        Assert.Equal(6864, solver.Part1("Day10/input.txt"));
    }
    
    [Fact]
    public void Test_Part2_Sample()
    {
        var solver = new Day10();
        Assert.Equal(10, solver.Part2("Day10/sample2.txt"));
    }
    
    [Fact]
    public void Test_Part2()
    {
        var solver = new Day10();
        Assert.Equal(349, solver.Part2("Day10/input.txt"));
    }
}