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
    // [InlineData(64, 3605, "Day21/input.txt")]
    // [InlineData(6, 16, "Day21/sample.txt")]
    //[InlineData(10, 50, "Day21/sample.txt")]
    [InlineData(1000, 668697, "Day21/sample.txt")]
    [InlineData(5000, 16733044, "Day21/sample.txt")]
    public void Test_Part2_Sample(int steps, ulong expected, string filename)
    {
        var solver = new Day21_Part2();
        Assert.Equal(expected, solver.Part2(filename, steps));
    }
    
    // In exactly 6 steps, he can still reach 16 garden plots.
    //     In exactly 10 steps, he can reach any of 50 garden plots.
    //     In exactly 50 steps, he can reach 1594 garden plots.
    //     In exactly 100 steps, he can reach 6536 garden plots.
    //     In exactly 500 steps, he can reach 167004 garden plots.
    //     In exactly 1000 steps, he can reach 668697 garden plots.
    //     In exactly 5000 steps, he can reach 16733044 garden plots.
    
    [Fact]
    public void Test_Part2()
    {
        var solver = new Day21_Part2();
        // 596734610902877 too low
        // 596734610917443 too low
        // 596734610917575 too low
        // 596734624269210
        // 596734637620911 wrong
        // 
        
        Assert.Equal((ulong)596734624269210, solver.Part2("Day21/input.txt", 26501365));
    }
}