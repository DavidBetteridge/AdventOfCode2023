using BenchmarkDotNet.Attributes;

namespace AOC_2023.Solutions;

[MemoryDiagnoser(true)]
public class Day15BenchmarkTests
{
    [Benchmark(Baseline = true)]
    public void Day15_Part1()
    {
        var solver = new Day15();
        solver.Part1("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day15/input.txt");
    }
    
    [Benchmark(Baseline = false)]
    public void Day15_Part2()
    {
        var solver = new Day15();
        solver.Part2("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day15/input.txt");
    }
}
