using AOC_2023.Solutions;
using BenchmarkDotNet.Running;

//BenchmarkRunner.Run<Day04BenchmarkTests>();

Console.WriteLine("Running");
var solver = new Day05();
var result = solver.Part2("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day05/input.txt");
Console.WriteLine($"Result is {result}");