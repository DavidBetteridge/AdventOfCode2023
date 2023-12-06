namespace AOC_2023.Tests;

public class Day06Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Day06();
        Assert.Equal(288, solver.Part1("Day06/sample.txt"));
    }

    [Fact]
    public void Test_Part1()
    {
        var solver = new Day06();
        Assert.Equal(4811940, solver.Part1("Day06/input.txt"));
    }
    
    [Fact]
    public void Test_Part2_Sample()
    {
        var solver = new Day06();
        Assert.Equal(71503, solver.Part2("Day06/sample.txt"));
    }
    
    [Fact]
    public void Test_Part2()
    {
        var solver = new Day06();
        Assert.Equal(30077773, solver.Part2("Day06/input.txt"));
    }
}