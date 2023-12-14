namespace AOC_2023.Tests;

public class Day14Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Day14();
        Assert.Equal(136, solver.Part1("Day14/sample.txt"));
    }

    [Fact]
    public void Test_Part1()
    {
        var solver = new Day14();
        Assert.Equal(105003, solver.Part1("Day14/input.txt"));
    }

}