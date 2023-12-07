namespace AOC_2023.Tests;

public class Day07Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Day07();
        Assert.Equal(6440, solver.Part1("Day07/sample.txt"));
    }

    [Fact]
    public void Test_Part1()
    {
        var solver = new Day07();
        Assert.Equal(251216224, solver.Part1("Day07/input.txt"));
    }
    
    // [Fact]
    // public void Test_Part2_Sample()
    // {
    //     var solver = new Day07();
    //     Assert.Equal(71503, solver.Part2("Day07/sample.txt"));
    // }
    //
    // [Fact]
    // public void Test_Part2()
    // {
    //     var solver = new Day07();
    //     Assert.Equal(30077773, solver.Part2("Day07/input.txt"));
    // }
}