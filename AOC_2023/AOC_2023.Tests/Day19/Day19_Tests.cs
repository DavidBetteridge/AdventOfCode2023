namespace AOC_2023.Tests;

public class Day19Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Day19();
        Assert.Equal(19114, solver.Part1("Day19/sample.txt"));
    }

    [Fact]
    public void Test_Part1()
    {
        var solver = new Day19();
        Assert.Equal(446517, solver.Part1("Day19/input.txt"));
    }

    // [Fact]
    // public void Test_Part2_Sample()
    // {
    //     var solver = new Day19();
    //     Assert.Equal(952408144115, solver.Part2("Day19/sample.txt"));
    // }
    //
    // [Fact]
    // public void Test_Part2()
    // {
    //     var solver = new Day19();
    //     Assert.Equal(72811019847283, solver.Part2("Day19/input.txt"));
    // }
}