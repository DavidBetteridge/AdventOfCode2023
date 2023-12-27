namespace AOC_2023.Tests;

public class Day25Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Day25();
        Assert.Equal(94, solver.Part1("Day25/sample.txt"));
    }

    [Fact]
    public void Test_Part1()
    {
        var solver = new Day25();
        Assert.Equal(2442, solver.Part1("Day25/input.txt"));
    }

}