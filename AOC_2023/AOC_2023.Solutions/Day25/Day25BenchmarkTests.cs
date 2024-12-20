using BenchmarkDotNet.Attributes;

namespace AOC_2023.Solutions;

[MemoryDiagnoser(true)]
public class Day25BenchmarkTests
{
    [Benchmark(Baseline = true)]
    public void Part1()
    {
        var solver = new Day25();
        solver.Part1("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day25/input.txt");
    }
}
