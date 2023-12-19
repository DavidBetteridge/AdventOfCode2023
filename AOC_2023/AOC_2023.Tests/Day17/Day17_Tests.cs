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
        Assert.Equal(755, solver.Part1("Day17/input.txt"));
    }

    [Fact]
    public void Test_Part2_Sample()
    {
        var solver = new Day17();
        Assert.Equal(94, solver.Part2("Day17/sample.txt"));
    }
    
    [Fact]
    public void Test_Part2_Sample2()
    {
        var solver = new Day17();
        Assert.Equal(71, solver.Part2("Day17/sample3.txt"));
    }
    
    [Fact]
    public void Test_Part2()
    {
        var solver = new Day17();
        Assert.Equal(881, solver.Part2("Day17/input.txt"));  //881 too low
    }
}