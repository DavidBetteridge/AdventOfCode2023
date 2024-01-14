using BenchmarkDotNet.Attributes;

namespace AOC_2023.Solutions;

[MemoryDiagnoser(true)]
public class Day11BenchmarkTests
{
    [Benchmark(Baseline = true)]
    public void Day11_Part1()
    {
        var solver = new Day11();
        solver.Part1And2(1,"/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day11/input.txt");
    }
   
    [Benchmark]
    public void Day11_Part2()
    {
        var solver = new Day11();
        solver.Part1And2(1000000-1,"/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day11/input.txt");
    }
}