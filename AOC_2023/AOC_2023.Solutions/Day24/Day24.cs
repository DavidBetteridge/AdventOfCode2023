using System.Numerics;
using CommandLine;

namespace AOC_2023.Solutions;

public class Day24
{
    private record Hailstone
    {
        public long InitialX { get; set; }
        public long InitialY { get; set; }
        public long InitialZ { get; set; }
        public long VelocityX { get; set; }
        public long VelocityY { get; set; }
        public long VelocityZ { get; set; }
    }

    private Hailstone Parse(string line)
    {
        // InitialX, InitialY, InitialZ @ VelocityX, VelocityY, VelocityZ
        // 19, 13, 30 @ -2,  1, -2
        var sides = line.Split(" @ ");
        var initial = sides[0].Split(", ");
        var velocity = sides[1].Split(", ");
        return new Hailstone
        {
            InitialX = long.Parse(initial[0]),
            InitialY = long.Parse(initial[1]),
            InitialZ = long.Parse(initial[2]),
            VelocityX = long.Parse(velocity[0]),
            VelocityY = long.Parse(velocity[1]),
            VelocityZ = long.Parse(velocity[2]),
        };
    }

    public long Part1(string filename, long minZone, long maxZone)
    {
        var hailstones = File.ReadAllLines(filename).Select(Parse).ToArray();

        var result = 0;
        for (var i = 0; i < hailstones.Length-1; i++)
        {
            for (var j = i+1; j < hailstones.Length; j++)
            {
                if (CrossInZone(hailstones[i], hailstones[j], minZone, maxZone))
                    result++;
            }
        }
        
        return result;
    }

    private bool CrossInZone(Hailstone hsA, Hailstone hsB, long minZone, long maxZone)
    {
        //Hailstone A: 19, 13, 30 @ -2, 1, -2
        //Hailstone B: 18, 19, 22 @ -1, -1, -2
        // 19 - 2x = 18 - 1y  ==> 1 = 2x - 1y
        // 13 + 1x = 19 - 1y  ==> 6 = 1x + 1y

        var v1 = new Vector3(-hsA.VelocityX,hsB.VelocityX, hsA.InitialX - hsB.InitialX );
        var v2 = new Vector3(hsA.VelocityY, -hsB.VelocityY, -(hsA.InitialY - hsB.InitialY) );
        
        v1 = v1 / v1[0];
        v2 = v2 - (v2[0] * v1);
        v2 = v2 / v2[1];
        var t2 = v2[2];
        var t1 = v1[2] - (v1[1] * t2);
        var x = hsA.InitialX + (hsA.VelocityX * t1);
        var y = hsA.InitialY + (hsA.VelocityY * t1);

        if (t1 < 0 || t2 < 0)
        {
        //    Console.WriteLine($"{x}, {y} Past");
            return false;
        }
        else
        {
        //    Console.WriteLine($"{x}, {y}");
            return (x >= minZone && x <= maxZone && y >= minZone && y <= maxZone);
        }
    }
}

