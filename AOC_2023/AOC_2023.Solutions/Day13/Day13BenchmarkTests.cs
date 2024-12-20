using BenchmarkDotNet.Attributes;

namespace AOC_2023.Solutions;

[MemoryDiagnoser(true)]
public class Day13BenchmarkTests
{
    [Benchmark(Baseline = true)]
    public void Day13_Part1()
    {
        var solver = new Day13();
        solver.Part1("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day13/input.txt");
    }
    
    [Benchmark]
    public void Day13_Part2()
    {
        var solver = new Day13Part2();
        solver.Part2("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day13/input.txt");
    }

}
