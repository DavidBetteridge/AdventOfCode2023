using BenchmarkDotNet.Attributes;

namespace AOC_2023.Solutions;

[MemoryDiagnoser(true)]
public class Day04BenchmarkTests
{
    [Benchmark]
    public void Day04_Part1()
    {
        var solver = new Day04();
        solver.Part1("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day04/input.txt");
    }
    
    [Benchmark]
    public void Day04_Part2()
    {
        var solver = new Day04();
        solver.Part2("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day04/input.txt");
    }
}