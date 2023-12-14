using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;

namespace AOC_2023.Solutions;

[MemoryDiagnoser(true)]
public class Day14BenchmarkTests
{
    [Benchmark(Baseline = true)]
    public void Part1()
    {
        var solver = new Day14();
        solver.Part1("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day14/input.txt");
    }

    // [Benchmark()]
    // public void Part1()
    // {
    //     var solver = new Day14Slow();
    //     solver.Part1("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day14/input.txt");
    // }
    //
    // [Benchmark()]
    // public void Part1_Flipped()
    // {
    //     var solver = new Day14Flipped();
    //     solver.Part1("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day14/input.txt");
    // }
    
    //
    // [Benchmark]
    // public void Part2()
    // {
    //     var solver = new Day13Part2();
    //     solver.Part2("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day13/input.txt");
    // }
}
