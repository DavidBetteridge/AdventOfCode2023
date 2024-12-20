using BenchmarkDotNet.Attributes;

namespace AOC_2023.Solutions;

[MemoryDiagnoser(true)]
public class Day06BenchmarkTests
{
    
    [Benchmark]
    public void Day06_Part1()
    {
        var solver = new Day06();
        solver.Part1("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day06/input.txt");
    }
    
    [Benchmark]
    public void Day06_Part2()
    {
        var solver = new Day06();
        solver.Part2("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day06/input.txt");
    }
}