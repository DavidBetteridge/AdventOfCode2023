using BenchmarkDotNet.Attributes;

namespace AOC_2023.Solutions;

[MemoryDiagnoser(true)]
public class Day18BenchmarkTests
{
    [Benchmark(Baseline = true)]
    public void Part1()
    {
        var solver = new Day18();
        solver.Part1("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day18/input.txt");
    }
    
    [Benchmark(Baseline = false)]
    public void Part2()
    {
        var solver = new Day18();
        solver.Part2("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day18/input.txt");
    }
}
