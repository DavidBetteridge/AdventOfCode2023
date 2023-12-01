using BenchmarkDotNet.Attributes;

namespace AOC_2023.Solutions;

[MemoryDiagnoser(true)]
public class BenchmarkTests
{
    [Benchmark]
    public void MapsVersion()
    {
        var solver = new Day01();
        solver.Part2("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day01/input.txt");
    }
    
    [Benchmark]
    public void UnrolledVersion()
    {
        var solver = new Day01_Unrolled();
        solver.Part2("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day01/input.txt");
    }
}