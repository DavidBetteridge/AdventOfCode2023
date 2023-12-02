namespace AOC_2023.Tests;

public class Day02Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Day02();
        Assert.Equal(8, solver.Part1("Day02/sample.txt"));
    }
    
    [Fact]
    public void Test_Part1()
    {
        var solver = new Day02();
        Assert.Equal(2348, solver.Part1("Day02/input.txt"));
    }
    
    [Fact]
    public void Test_Part2_Sample()
    {
        var solver = new Day02();
        Assert.Equal(2286, solver.Part2("Day02/sample.txt"));
    }
    
    [Fact]
    public void Test_Part2()
    {
        var solver = new Day02();
        Assert.Equal(76008, solver.Part2("Day02/input.txt"));
    }
}