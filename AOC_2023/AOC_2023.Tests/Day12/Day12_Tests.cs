namespace AOC_2023.Tests;

public class Day12Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Day12();
        Assert.Equal(21, solver.Part1("Day12/sample.txt"));
    }

    [Fact]
    public void Test_Part1()
    {
        var solver = new Day12();
        Assert.Equal(7286, solver.Part1("Day12/input.txt"));
    }
    
    [Fact]
    public void Test_Part2_Sample()
    {
        var solver = new Day12();
        Assert.Equal(525152, solver.Part2("Day12/sample.txt"));
    }
    
    [Fact]
    public void Test_Part2()
    {
        var solver = new Day12();
        Assert.Equal(25470469710341, solver.Part2("Day12/input.txt"));
    }
}