using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.Diagnostics.Tracing.StackSources;

namespace AOC_2023.Solutions;

public class Day22
{
    public record Block
    {
        public int StartX { get; set; }
        public int StartY { get; set; }
        public int StartZ { get; set; }
        public int EndX { get; set; }
        public int EndY { get; set; }
        public int EndZ { get; set; }
    }

    public long Part1(string filename)
    {
        var blocks = File.ReadAllLines(filename)
            .Select(ParseBlock)
            .OrderBy(b => Math.Min(b.StartZ, b.EndZ))
            .ToList();
        var minX = blocks.Min(l => Math.Min(l.StartX, l.EndX));
        var maxX = blocks.Max(l => Math.Max(l.StartX, l.EndX));
        var minY = blocks.Min(l => Math.Min(l.StartY, l.EndY));
        var maxY = blocks.Max(l => Math.Max(l.StartY, l.EndY));
        var minZ = blocks.Min(l => Math.Min(l.StartZ, l.EndZ));
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
            for (var x = block.StartX; x <= block.EndX; x++)
                world[x, block.StartY, block.StartZ] = (char)('A' + blockNumber);
            for (var y = block.StartY; y <= block.EndY; y++)
                world[block.StartX, y, block.StartZ] = (char)('A' + blockNumber);
            for (var z = block.StartZ; z <= block.EndZ; z++)
                world[block.StartX, block.StartY, z-offset] = (char)('A' + blockNumber); 
        }

        for (var z = 9; z >= 0; z--)
        {
            for (var x = 0; x < 3; x++)
            {
                var written = false;
                for (var y = 0; y < maxY; y++)
                {
                    if (world[x, y, z] != '\0')
                    {
                        Console.Write(world[x,y,z]);
                        written = true;
                        break;
                    }
                }
                if (!written)
                    Console.Write(".");
            }
            Console.WriteLine($" {z}");
        }
        
        
    // for (var blockNumber = 0; blockNumber < blocks.Count; blockNumber++)
    // {
    //     var block = blocks[blockNumber];
    //     for (var x = block.StartX; x <= block.EndX; x++)
    //         world[x, block.StartY, block.StartZ] = blockNumber;
    //     for (var y = block.StartY; y <= block.EndY; y++)
    //         world[block.StartX, y, block.StartZ] = blockNumber;
    //     for (var z = block.StartZ; z <= block.EndZ; z++)
    //         world[block.StartX, block.StartY, z] = blockNumber;            
    // }

    // // Shuffle blocks down to fill in spaces
    // var offset = 0;
    // for (var z = 0; z < maxZ; z++)
    // {
    //     
    // }



    return 0;
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