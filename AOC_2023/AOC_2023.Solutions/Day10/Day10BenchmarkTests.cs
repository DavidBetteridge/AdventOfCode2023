using BenchmarkDotNet.Attributes;

namespace AOC_2023.Solutions;

[MemoryDiagnoser(true)]
public class Day10BenchmarkTests
{
    [Benchmark]
    public void Day10_Part1()
    {
        var solver = new Day10();
        solver.Part1("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day10/input.txt");
    }
    
    [Benchmark]
    public void Day10_Part2()
    {
        var solver = new Day10();
        solver.Part2("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day10/input.txt");
    }
}