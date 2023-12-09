using BenchmarkDotNet.Attributes;

namespace AOC_2023.Solutions;

[MemoryDiagnoser(true)]
public class Day09BenchmarkTests
{
    [Benchmark]
    public void Part1()
    {
        var solver = new Day09();
        solver.Part1("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day09/input.txt");
    }
    
    [Benchmark]
    public void Part1Slim()
    {
        var solver = new Day09Slim();
        solver.Part1("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day09/input.txt");
    }
    
    [Benchmark]
    public void Part2()
    {
        var solver = new Day09();
        solver.Part2("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day09/input.txt");
    }
    
    [Benchmark]
    public void Part2Slim()
    {
        var solver = new Day09Slim();
        solver.Part2("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day09/input.txt");
    }
}