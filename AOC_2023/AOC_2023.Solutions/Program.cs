using AOC_2023.Solutions;
using BenchmarkDotNet.Running;
BenchmarkRunner.Run<Day16BenchmarkTests>();


// var solver = new Day21();
//
// var previous = 0L;
// for (int steps = 0; steps < 50; steps++)
// {
//     var visited =
//         solver.Part2("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day21/sample.txt",
//             steps);
//     Console.WriteLine($"{steps} {visited}  ({visited-previous})");
//     previous = visited;
// }


// 42 reachable in sample
// 7307 reachable in input