using BenchmarkDotNet.Attributes;

namespace AOC_2023.Solutions;

[MemoryDiagnoser(true)]
public class Day02BenchmarkTests
{
    [Benchmark]
    public void Splits_Part1()
    {
        var solver = new Day02();
        solver.Part1("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day02/input.txt");
    }
    
    [Benchmark]
    public void LR_Part1()
    {
        var solver = new Day02_LR();
        solver.Part1("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day02/input.txt");
    }
    
    [Benchmark]
    public void LR2_Part1()
    {
        var solver = new Day02_LR2();
        solver.Part1("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day02/input.txt");
    }
    
    [Benchmark]
    public void Splits_Part2()
    {
        var solver = new Day02();
        solver.Part2("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day02/input.txt");
    }
    
    [Benchmark]
    public void LR_Part2()
    {
        var solver = new Day02_LR();
        solver.Part2("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day02/input.txt");
    }
    
    [Benchmark]
    public void LR2_Part2()
    {
        var solver = new Day02_LR2();
        solver.Part2("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day02/input.txt");
    }
}