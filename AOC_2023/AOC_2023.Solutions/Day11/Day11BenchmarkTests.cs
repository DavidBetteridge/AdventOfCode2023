using BenchmarkDotNet.Attributes;

namespace AOC_2023.Solutions;

[MemoryDiagnoser(true)]
public class Day11BenchmarkTests
{
    // [Benchmark]
    // public void Part1()
    // {
    //     var solver = new Day11();
    //     solver.Part1And2(1, "/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day11/input.txt");
    // }
    
    [Benchmark(Baseline = true)]
    public void Part2()
    {
        var solver = new Day11();
        solver.Part1And2(1000000-1,"/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day11/input.txt");
    }
    
    [Benchmark]
    public void Part2Parser()
    {
        var solver = new Day11Parser();
        solver.Part1And2(1000000-1,"/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day11/input.txt");
    }
    [Benchmark]
    public void Part2ParserB()
    {
        var solver = new Day11Parser();
        solver.Part1And2B(1000000-1,"/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day11/input.txt");
    }
}