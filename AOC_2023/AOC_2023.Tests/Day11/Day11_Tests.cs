namespace AOC_2023.Tests;

public class Day11Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Day11();
        Assert.Equal(374, solver.Part1And2(1, "Day11/sample.txt"));
    }

    [Fact]
    public void Test_Part1()
    {
        var solver = new Day11();
        Assert.Equal(10313550, solver.Part1And2(1, "Day11/input.txt"));
    }
    
    [Fact]
    public void Test_Part2_Sample()
    {
        var solver = new Day11();
        Assert.Equal(1030, solver.Part1And2(10-1, "Day11/sample.txt"));
        Assert.Equal(8410, solver.Part1And2(100-1, "Day11/sample.txt"));
    }
    
    [Fact]
    public void Test_Part2()
    {
        var solver = new Day11();
        Assert.Equal(611998089572, solver.Part1And2(1000000-1, "Day11/input.txt"));
    }
}