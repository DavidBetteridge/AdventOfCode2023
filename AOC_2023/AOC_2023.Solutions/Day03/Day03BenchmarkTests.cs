using BenchmarkDotNet.Attributes;

namespace AOC_2023.Solutions;

[MemoryDiagnoser(true)]
public class Day03BenchmarkTests
{
    
    [Benchmark]
    public void FromStar_Part2()
    {
        var solver = new Day03();
        solver.Part2("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day03/input.txt");
    }
    
    [Benchmark]
    public void FromNumber_Part2()
    {
        var solver = new Day03B();
        solver.Part2("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day03/input.txt");
    }
}