namespace AOC_2023.Tests;

public class Day08Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Day08();
        Assert.Equal(2, solver.Part1("Day08/sample.txt"));
    }

    [Fact]
    public void Test_Part1_Sample2()
    {
        var solver = new Day08();
        Assert.Equal(6, solver.Part1("Day08/sample2.txt"));
    }
    
    [Fact]
    public void Test_Part1()
    {
        var solver = new Day08();
        Assert.Equal(19099, solver.Part1("Day08/input.txt"));
    }
    
    [Fact]
    public void Test_Part2_Sample()
    {
        var solver = new Day08();
        Assert.Equal(6, solver.Part2("Day08/sample3.txt"));
    }
    
    [Fact]
    public void Test_Part2()
    {
        var solver = new Day08();
        Assert.Equal(17099847107071, solver.Part2("Day08/input.txt"));
    }
}