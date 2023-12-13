namespace AOC_2023.Tests;

public class Day13Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Day13();
        Assert.Equal(405, solver.Part1("Day13/sample.txt"));
    }

    [Fact]
    public void Test_Part1()
    {
        var solver = new Day13();
        Assert.Equal(33780, solver.Part1("Day13/input.txt"));
    }
    
    // [Fact]
    // public void Test_Part2_Sample()
    // {
    //     var solver = new Day13();
    //     Assert.Equal(525152, solver.Part1And2("Day12/sample.txt", 5));
    // }
    //
    // [Fact]
    // public void Test_Part2()
    // {
    //     var solver = new Day12();
    //     Assert.Equal(25470469710341, solver.Part1And2("Day12/input.txt", 5));
    // }
}