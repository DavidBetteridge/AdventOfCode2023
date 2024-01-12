using BenchmarkDotNet.Attributes;

namespace AOC_2023.Solutions;

[MemoryDiagnoser(true)]
public class Day09BenchmarkTests
{
    [Benchmark]
    public void Day09_Part1()
    {
        var solver = new Day09();
        solver.Part1("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day09/input.txt");
    }
    
    [Benchmark]
    public void Day09_Part2()
    {
        var solver = new Day09();
        solver.Part2("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day09/input.txt");
    }
}