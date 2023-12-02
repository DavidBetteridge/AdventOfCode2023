namespace AOC_2023.Tests;

public class Day02Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Day02();
        Assert.Equal(8, solver.Part1("Day02/part1_sample.txt"));
    }
    
    [Fact]
    public void Test_Part1()
    {
        var solver = new Day02();
        Assert.Equal(56049, solver.Part1("Day02/input.txt"));
    }
    //
    // [Fact]
    // public void Test_Part2_Sample()
    // {
    //     var solver = new Day01();
    //     Assert.Equal(281, solver.Part2("Day01/part2_sample.txt"));
    // }
    //
    // [Fact]
    // public void Test_Part2()
    // {
    //     var solver = new Day01();
    //     Assert.Equal(54530, solver.Part2("Day01/input.txt"));
    // }
}