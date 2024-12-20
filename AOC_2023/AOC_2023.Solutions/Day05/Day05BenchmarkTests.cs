using BenchmarkDotNet.Attributes;

namespace AOC_2023.Solutions;

[MemoryDiagnoser(true)]
public class Day05BenchmarkTests
{
    [Benchmark]
    public void Day05_Part1()
    {
        var solver = new Day05();
        solver.Part1("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day05/input.txt");
    }
    
    [Benchmark]
    public void Day05_Part2()
    {
        var solver = new Day05();
        solver.Part2("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day05/input.txt");
    }
}