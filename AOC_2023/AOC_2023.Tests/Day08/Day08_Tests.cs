namespace AOC_2023.Tests;

public class Day08Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Day08();
        Assert.Equal(6440, solver.Part1("Day08/sample.txt"));
    }

    [Fact]
    public void Test_Part1()
    {
        var solver = new Day08();
        Assert.Equal(251216224, solver.Part1("Day08/input.txt"));
    }
    
    // [Fact]
    // public void Test_Part2_Sample()
    // {
    //     var solver = new Day08();
    //     Assert.Equal(5905, solver.Part2("Day08/sample.txt"));
    // }
    //
    // [Fact]
    // public void Test_Part2()
    // {
    //     var solver = new Day08();
    //     Assert.Equal(250825981, solver.Part2("Day08/input.txt"));
    // }
}