using BenchmarkDotNet.Attributes;

namespace AOC_2023.Solutions;

[MemoryDiagnoser(true)]
public class Day17BenchmarkTests
{
    [Benchmark(Baseline = true)]
    public void Part1()
    {
        var solver = new Day17();
        solver.Part1("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day17/input.txt");
    }
    
    // [Benchmark(Baseline = false)]
    // public void Part2()
    // {
    //     var solver = new Day17();
    //     solver.Part2("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day17/input.txt");
    // }
}
