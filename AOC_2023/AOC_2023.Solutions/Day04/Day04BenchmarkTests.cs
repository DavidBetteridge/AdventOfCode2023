using BenchmarkDotNet.Attributes;

namespace AOC_2023.Solutions;

[MemoryDiagnoser(true)]
public class Day04BenchmarkTests
{
    
    // [Benchmark]
    // public void Part1()
    // {
    //     var solver = new Day04();
    //     solver.Part1("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day04/input.txt");
    // }
    //
    // [Benchmark]
    // public void Part1RL()
    // {
    //     var solver = new Day04RL();
    //     solver.Part1("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day04/input.txt");
    // }
    //
    // [Benchmark]
    // public void Part1RLVector()
    // {
    //     var solver = new Day04RLVector();
    //     solver.Part1("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day04/input.txt");
    // }
    //
    // [Benchmark]
    // public void Part2()
    // {
    //     var solver = new Day04();
    //     solver.Part2("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day04/input.txt");
    // }
    //
    // [Benchmark]
    // public void Part2RL()
    // {
    //     var solver = new Day04RL();
    //     solver.Part2("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day04/input.txt");
    // }
        
    [Benchmark]
    public void Part2RLVector()
    {
        var solver = new Day04RLVector();
        solver.Part2("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day04/input.txt");
    }
    
    // [Benchmark]
    // public void Part2_Peter()
    // {
    //     var solver = new Day04Peter();
    //     solver.Part2("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day04/input.txt");
    // }
    
    [Benchmark]
    public void Part2_Span()
    {
        var solver = new Day04RLVector();
        solver.Part2B("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day04/input.txt");
    }
}