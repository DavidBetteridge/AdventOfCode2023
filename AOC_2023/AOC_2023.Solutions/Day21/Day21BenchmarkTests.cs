using BenchmarkDotNet.Attributes;

namespace AOC_2023.Solutions;

[MemoryDiagnoser(true)]
public class Day21BenchmarkTests
{
    [Benchmark(Baseline = true)]
    public void Part1()
    {
        var solver = new Day21();
        solver.Part1("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day21/input.txt",64);
    }
    
    [Benchmark(Baseline = false)]
    public void Part2()
    {
        var solver = new Day21();
        solver.Part2("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day21/input.txt", 64);
    }
}
