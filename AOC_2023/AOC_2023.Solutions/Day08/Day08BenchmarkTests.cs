using BenchmarkDotNet.Attributes;

namespace AOC_2023.Solutions;

[MemoryDiagnoser(true)]
public class Day08BenchmarkTests
{
    [Benchmark]
    public void Part1()
    {
        var solver = new Day08();
        solver.Part1("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day08/input.txt");
    }
    
    [Benchmark]
    public void Part2()
    {
        var solver = new Day08();
        solver.Part2("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day08/input.txt");
    }
}