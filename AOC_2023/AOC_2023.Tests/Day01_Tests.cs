namespace AOC_2023.Tests;

public class Day01Tests
{
    [Fact]
    public void Test_Part1()
    {
        var solver = new Day01();
        Assert.Equal(123, solver.Part1());
    }
}