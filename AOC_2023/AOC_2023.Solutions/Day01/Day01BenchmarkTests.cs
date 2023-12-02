using BenchmarkDotNet.Attributes;

namespace AOC_2023.Solutions;

[MemoryDiagnoser(true)]
public class Day01BenchmarkTests
{
    [Benchmark]
    public void Maps_Part1()
    {
        var solver = new Day01();
        solver.Part1("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day01/input.txt");
    }
    
    [Benchmark]
    public void Unrolled_Part1()
    {
        var solver = new Day01_Unrolled();
        solver.Part1("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day01/input.txt");
    }
    
    [Benchmark]
    public void Maps_Part2()
    {
        var solver = new Day01();
        solver.Part2("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day01/input.txt");
    }
    
    [Benchmark]
    public void Unrolled_Part2()
    {
        var solver = new Day01_Unrolled();
        solver.Part2("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day01/input.txt");
    }
}