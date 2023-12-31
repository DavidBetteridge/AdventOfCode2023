namespace AOC_2023.Tests;

public class Day21Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Day21();
        Assert.Equal(16, solver.Part1("Day21/sample.txt", 6));
    }

    [Fact]
    public void Test_Part1()
    {
        var solver = new Day21();
        Assert.Equal(3605, solver.Part1("Day21/input.txt", 64));
    }

    [Theory]
    [InlineData(6, 16)]
    [InlineData(10, 50)]
   // [InlineData(50, 1594)]
    public void Test_Part2_Sample(int steps, int expected)
    {
        var solver = new Day21();
        Assert.Equal(expected, solver.Part2("Day21/sample.txt", steps));
    }
    
    // In exactly 6 steps, he can still reach 16 garden plots.
    //     In exactly 10 steps, he can reach any of 50 garden plots.
    //     In exactly 50 steps, he can reach 1594 garden plots.
    //     In exactly 100 steps, he can reach 6536 garden plots.
    //     In exactly 500 steps, he can reach 167004 garden plots.
    //     In exactly 1000 steps, he can reach 668697 garden plots.
    //     In exactly 5000 steps, he can reach 16733044 garden plots.
    
    // [Fact]
    // public void Test_Part2()
    // {
    //     var solver = new Day21();
    //     Assert.Equal(130090458884662, solver.Part2("Day21/input.txt"));
    // }
}