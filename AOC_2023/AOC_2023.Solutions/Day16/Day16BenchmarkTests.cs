using BenchmarkDotNet.Attributes;

namespace AOC_2023.Solutions;

[MemoryDiagnoser(true)]
public class Day16BenchmarkTests
{
    [Benchmark(Baseline = true)]
    public void Day16_Part1()
    {
        var solver = new Day16();
        solver.Part1("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day16/input.txt");
    }
    
    [Benchmark(Baseline = false)]
    public void Day16_Part2()
    {
        var solver = new Day16();
        solver.Part2("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day16/input.txt");
    }
}
