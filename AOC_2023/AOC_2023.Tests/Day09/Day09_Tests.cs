namespace AOC_2023.Tests;

public class Day09Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Day09();
        Assert.Equal(114, solver.Part1("Day09/sample.txt"));
    }

    [Fact]
    public void Test_Part1()
    {
        var solver = new Day09();
        Assert.Equal(19099, solver.Part1("Day09/input.txt"));
    }
    
    [Fact]
    public void Test_Part2_Sample()
    {
        var solver = new Day09();
        Assert.Equal(2, solver.Part2("Day09/sample.txt"));
    }
    
    [Fact]
    public void Test_Part2()
    {
        var solver = new Day09();
        Assert.Equal(7099847107071, solver.Part2("Day09/input.txt"));
    }
}