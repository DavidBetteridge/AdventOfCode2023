using Microsoft.Diagnostics.Tracing.Analysis;

namespace AOC_2023.Solutions;

public class Day22
{
    public record Block
    {
        public char BlockId { get; set; }
        public int StartX { get; set; }
        public int StartY { get; set; }
        public int StartZ { get; set; }
        public int EndX { get; set; }
        public int EndY { get; set; }
        public int EndZ { get; set; }
        public HashSet<char> SitsOn { get; set; } = new();
    }

    public long Part1(string filename)
    {
        var blocks = File.ReadAllLines(filename)
            .Select(ParseBlock)
            .OrderBy(b => Math.Min(b.StartZ, b.EndZ))
            .ToList();
        var maxX = blocks.Max(l => Math.Max(l.StartX, l.EndX));
        var maxY = blocks.Max(l => Math.Max(l.StartY, l.EndY));
        var maxZ = blocks.Max(l => Math.Max(l.StartZ, l.EndZ));

        var world = new char[maxX + 1, maxY + 1, maxZ + 1];

        // Blocks are ordered so that we have the bottom block (in z axis) first.
        for (var blockNumber = 0; blockNumber < blocks.Count; blockNumber++)
        {
            var block = blocks[blockNumber];

            // Insert the block into the world as lowest as we can.
            // Start with it's current position,  we keep dropping it until we hit a blockage
            // or the floor.
            var offset = 0;
            var ok = true;
            while (ok)
            {
                offset++;
                if (block.StartZ - offset < 1) break;

                for (var x = block.StartX; x <= block.EndX; x++)
                {
                    if (world[x, block.StartY, block.StartZ - offset] != '\0')
                    {
                        ok = false;
                        break;
                    }
                }

                for (var y = block.StartY; y <= block.EndY; y++)
                {
                    if (world[block.StartX, y, block.StartZ - offset] != '\0')
                    {
                        ok = false;
                        break;
                    }
                }
            }

            offset--;

            // Add to the world
            block.StartZ -= offset;
            block.EndZ -= offset;
            block.BlockId = (char)('A' + blockNumber);
            for (var x = block.StartX; x <= block.EndX; x++)
            {
                world[x, block.StartY, block.StartZ] = block.BlockId;
                if (block.StartZ - 1 >= 0 &&
                    world[x, block.StartY, block.StartZ - 1] != '\0' &&
                    world[x, block.StartY, block.StartZ - 1] != block.BlockId)
                    block.SitsOn.Add(world[x, block.StartY, block.StartZ - 1]);
            }

            for (var y = block.StartY; y <= block.EndY; y++)
            {
                world[block.StartX, y, block.StartZ] = block.BlockId;
                if (block.StartZ - 1 >= 0 &&
                    world[block.StartX, y, block.StartZ - 1] != '\0' &&
                    world[block.StartX, y, block.StartZ - 1] != block.BlockId)
                    block.SitsOn.Add(world[block.StartX, y, block.StartZ - 1]);
            }

            for (var z = block.StartZ; z <= block.EndZ; z++)
            {
                world[block.StartX, block.StartY, z] = block.BlockId;
                if (z - 1 >= 0 &&
                    world[block.StartX, block.StartY, z - 1] != '\0' &&
                    world[block.StartX, block.StartY, z - 1] != block.BlockId
                   )
                    block.SitsOn.Add(world[block.StartX, block.StartY, z - 1]);
            }
        }

        var result = 0;
        for (var blockNumber = 0; blockNumber < blocks.Count; blockNumber++)
        {
            // What sits on us and only us?
            var block = blocks[blockNumber];
            var problem = blocks.Any(b => b.SitsOn.Count == 1 &&
                                          b.SitsOn.Contains((char)('A' + blockNumber)));
            if (!problem)
            {
                Console.WriteLine((char)('A' + blockNumber));
                result++;
            }
        }


        // // Front
        // for (var z = 9; z >= 0; z--)
        // {
        //     for (var x = 0; x < 3; x++)
        //     {
        //         var written = false;
        //         for (var y = 0; y < maxY; y++)
        //         {
        //             if (world[x, y, z] != '\0')
        //             {
        //                 Console.Write(world[x,y,z]);
        //                 written = true;
        //                 break;
        //             }
        //         }
        //         if (!written)
        //             Console.Write(".");
        //     }
        //     Console.WriteLine($" {z}");
        // }
        //
        // // Side
        // Console.WriteLine();
        // for (var z = 9; z >= 0; z--)
        // {
        //     for (var y = 0; y < 3; y++)
        //     {
        //         var written = false;
        //         for (var x = 0; x < maxX; x++)
        //         {
        //             if (world[x, y, z] != '\0')
        //             {
        //                 Console.Write(world[x,y,z]);
        //                 written = true;
        //                 break;
        //             }
        //         }
        //         if (!written)
        //             Console.Write(".");
        //     }
        //     Console.WriteLine($" {z}");
        // }

        return result;
    }

    public long Part2(string filename)
    {
        var blocks = File.ReadAllLines(filename)
            .Select(ParseBlock)
            .OrderBy(b => Math.Min(b.StartZ, b.EndZ))
            .ToList();
        var maxX = blocks.Max(l => Math.Max(l.StartX, l.EndX));
        var maxY = blocks.Max(l => Math.Max(l.StartY, l.EndY));
        var maxZ = blocks.Max(l => Math.Max(l.StartZ, l.EndZ));

        var world = new char[maxX + 1, maxY + 1, maxZ + 1];

        // Blocks are ordered so that we have the bottom block (in z axis) first.
        for (var blockNumber = 0; blockNumber < blocks.Count; blockNumber++)
        {
            var block = blocks[blockNumber];

            // Insert the block into the world as lowest as we can.
            // Start with it's current position,  we keep dropping it until we hit a blockage
            // or the floor.
            var offset = 0;
            var ok = true;
            while (ok)
            {
                offset++;
                if (block.StartZ - offset < 1) break;

                for (var x = block.StartX; x <= block.EndX; x++)
                {
                    if (world[x, block.StartY, block.StartZ - offset] != '\0')
                    {
                        ok = false;
                        break;
                    }
                }

                for (var y = block.StartY; y <= block.EndY; y++)
                {
                    if (world[block.StartX, y, block.StartZ - offset] != '\0')
                    {
                        ok = false;
                        break;
                    }
                }
            }

            offset--;

            // Add to the world
            block.StartZ -= offset;
            block.EndZ -= offset;
            block.BlockId = (char)('A' + blockNumber);
            for (var x = block.StartX; x <= block.EndX; x++)
            {
                world[x, block.StartY, block.StartZ] = block.BlockId;
                if (block.StartZ - 1 >= 0 &&
                    world[x, block.StartY, block.StartZ - 1] != '\0' &&
                    world[x, block.StartY, block.StartZ - 1] != block.BlockId)
                    block.SitsOn.Add(world[x, block.StartY, block.StartZ - 1]);
            }

            for (var y = block.StartY; y <= block.EndY; y++)
            {
                world[block.StartX, y, block.StartZ] = block.BlockId;
                if (block.StartZ - 1 >= 0 &&
                    world[block.StartX, y, block.StartZ - 1] != '\0' &&
                    world[block.StartX, y, block.StartZ - 1] != block.BlockId)
                    block.SitsOn.Add(world[block.StartX, y, block.StartZ - 1]);
            }

            for (var z = block.StartZ; z <= block.EndZ; z++)
            {
                world[block.StartX, block.StartY, z] = block.BlockId;
                if (z - 1 >= 0 &&
                    world[block.StartX, block.StartY, z - 1] != '\0' &&
                    world[block.StartX, block.StartY, z - 1] != block.BlockId
                   )
                    block.SitsOn.Add(world[block.StartX, block.StartY, z - 1]);
            }
        }

        var result = 0;
        for (var blockNumber = 0; blockNumber < blocks.Count; blockNumber++)
        {
            // What sits on us and only us?
            var destroyed = new HashSet<char> { (char)('A' + blockNumber)};
    
            // Are there any blocks which are only sitting on destroyed blocks?
            var keepChecking = true;
            while (keepChecking)
            {
                keepChecking = false;
                var issues = blocks.Where(b => b.SitsOn.Count > 0 && b.SitsOn.All(s => destroyed.Contains(s)));
                foreach (var issue in issues)
                {
                    if (!destroyed.Contains(issue.BlockId))
                    {
                        destroyed.Add(issue.BlockId);
                        keepChecking = true;
                    }
                }   
            }

            result += destroyed.Count - 1;
        }

        return result;
    }
    
    private Block ParseBlock(string line)
    {
        var ends = line.Split('~');
        return new Block
        {
            StartX = int.Parse(ends[0].Split(',')[0]),
            StartY = int.Parse(ends[0].Split(',')[1]),
            StartZ = int.Parse(ends[0].Split(',')[2]),
            EndX = int.Parse(ends[1].Split(',')[0]),
            EndY = int.Parse(ends[1].Split(',')[1]),
            EndZ = int.Parse(ends[1].Split(',')[2]),
        };
    }
}