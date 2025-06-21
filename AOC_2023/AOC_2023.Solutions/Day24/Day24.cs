using System.Numerics;
using System.Numerics.Tensors;
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
        for (var i = 0; i < hailstones.Length - 1; i++)
        {
            for (var j = i + 1; j < hailstones.Length; j++)
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

        var v1 = new Vector3(-hsA.VelocityX, hsB.VelocityX, hsA.InitialX - hsB.InitialX);
        var v2 = new Vector3(hsA.VelocityY, -hsB.VelocityY, -(hsA.InitialY - hsB.InitialY));

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

    private bool Cross(Hailstone hsA, Hailstone hsB)
    {
        var v1 = new Vector3(-hsA.VelocityX, hsB.VelocityX, hsA.InitialX - hsB.InitialX);
        var v2 = new Vector3(hsA.VelocityY, -hsB.VelocityY, -(hsA.InitialY - hsB.InitialY));

        v1 = v1 / v1[0];
        v2 = v2 - (v2[0] * v1);
        
        var t2 = v2[2] / v2[1];
        var v12 = v1[2];
        var v11 = v1[1];

        var t1 = v12 - (v11 * t2);

        return (t1 >= 0 && t2 >= 0 && t1 == t2);
    }

    public long Part2(string filename)
    {
        var hailstones = File.ReadAllLines(filename).Select(Parse).ToArray();

        for (var t0 = 0; t0 < 10; t0++)
        {
            // Where is hailstone 0 at t0?
            var x0 = hailstones[0].InitialX + (t0 * hailstones[0].VelocityX);
            var y0 = hailstones[0].InitialY + (t0 * hailstones[0].VelocityY);
            var z0 = hailstones[0].InitialZ + (t0 * hailstones[0].VelocityZ);
            
            for (var t1 = 0; t1 < 10; t1++)
            {
                if (t0 == t1) continue;  
                
                // Where is hailstone 1 at t1?
                var x1 = hailstones[1].InitialX + (t1 * hailstones[1].VelocityX);
                var y1 = hailstones[1].InitialY + (t1 * hailstones[1].VelocityY);
                var z1 = hailstones[1].InitialZ + (t1 * hailstones[1].VelocityZ);

                // Assuming t0 and t1 are correct, we now know the Velocity of the stone.
                long velocityX = 0;
                long velocityY = 0;
                long velocityZ = 0;
                if (t1 > t0)
                {
                    // The stone hit t0 and then t1
                    if ( (x1 - x0) %  (t1 - t0) != 0) continue;
                    if ( (y1 - y0) %  (t1 - t0) != 0) continue;
                    if ( (z1 - z0) %  (t1 - t0) != 0) continue;
                    
                    velocityX = (x1 - x0) / (t1 - t0);
                    velocityY = (y1 - y0) / (t1 - t0);
                    velocityZ = (z1 - z0) / (t1 - t0);
                }
                else
                {
                    // The stone hit t1 and then t0
                    if ( (x0 - x1) %  (t0 - t1) != 0) continue;
                    if ( (y0 - y1) %  (t0 - t1) != 0) continue;
                    if ( (z0 - z1) %  (t0 - t1) != 0) continue;
                    
                    velocityX = (x0 - x1) / (t0 - t1);
                    velocityY = (y0 - y1) / (t0 - t1);
                    velocityZ = (z0 - z1) / (t0 - t1);
                }
                
                // and we can work back from either hailstone to get its original position
                var initialX = x0 - (t0 * velocityX);
                var initialY = y0 - (t0 * velocityY);
                var initialZ = z0 - (t0 * velocityZ);

                // Now we want to check that at least 6 of the first 20 hailstones get hit at some point
                var hitCount = 0;
                for (var t2 = 0; t2 < 10; t2++)
                {
                    var x = initialX + (t2 * velocityX);
                    var y = initialY + (t2 * velocityY);
                    var z = initialZ + (t2 * velocityZ);

                    for (var stone = 2; stone < 5; stone++)
                    {
                        var stoneX = hailstones[stone].InitialX + (t2 * hailstones[stone].VelocityX);
                        var stoneY = hailstones[stone].InitialY + (t2 * hailstones[stone].VelocityY);
                        var stoneZ = hailstones[stone].InitialZ + (t2 * hailstones[stone].VelocityZ);

                        if (stoneX == x && stoneY == y && stoneZ == z)
                            hitCount++;
                    }
                }

                if (hitCount >= 3)
                {
                    return initialX + initialY + initialZ;
                }
                
            }
        }
        

        // for (var x = -100; x < 100; x++)
        // {
        //     for (var y = -100; y < 100; y++)
        //     {
        //         for (var vx = -100; vx < 100; vx++)
        //         {
        //             for (var vy = -100; vy < 100; vy++)
        //             {
        //                 var hs = new Hailstone
        //                 {
        //                     InitialX = x,
        //                     InitialY = y,
        //                     VelocityX = vx,
        //                     VelocityY = vy
        //                 };
        //
        //                 var ok = true;
        //                 foreach (var hailstone in hailstones)
        //                 {
        //                     if (!Cross(hs, hailstone))
        //                     {
        //                         ok = false;
        //                         break;
        //                     }
        //                 }
        //
        //                 if (ok)
        //                 {
        //                     Console.WriteLine($"{hs.InitialX},{hs.InitialY} @ {hs.VelocityX},{hs.VelocityY}");
        //                 }
        //             }
        //
        //             
        //         }
        //     }
        // }
        

        // for (var i = 0; i < hailstones.Length; i++)
        // {
        //     var hs = hailstones[i];
        //     Console.WriteLine($"x + t{i}*vx = {hs.InitialX} + t{i}*{hs.VelocityX}");
        //     Console.WriteLine($"x + t{i}*vy = {hs.InitialY} + t{i}*{hs.VelocityY}");
        //     Console.WriteLine($"x + t{i}*vz = {hs.InitialZ} + t{i}*{hs.VelocityZ}");
        // }
        //     
        // Console.WriteLine("");
        return 0;

        // x + t0*vx = 156689809620606 + t0*-26
        // x + t0*vy = 243565579389165 + t0*48
        // x + t0*vz = 455137247320393 + t0*-140

        // x + t0*vx -156689809620606 = t0*-26
        // x + t0*vy -243565579389165 = t0*48
        // x + t0*vz -455137247320393=  t0*-140
        
        // (x + t0*vx -156689809620606) / -26 = t0
        // (x + t0*vy -243565579389165) / 48 = t0
        // (x + t0*vz -455137247320393) / -140 =  t0
        
        // (x + t0*vx -156689809620606) / -26 = (x + t0*vy -243565579389165) / 48 = (x + t0*vz -455137247320393) / -140

        // 40 * (x + t0*vx -156689809620606) = -26 * (x + t0*vy -243565579389165) 
        // 40x + 40*t0*yx -156689809620606*40 = 26x -26*t0*vy +243565579389165 * 26
        
        
        // 40x + 40*t0*yx -6267592384824240 = 26x -26*t0*vy +6332705064118290
        
        
        // 14x + 40*t0*yx = -26*t0*vy + 6332705064118290 + 6267592384824240

        
        
        
        // foreach (var hailstone in hailstones)
        // {
        //     Console.WriteLine($"            {{ pos: {hailstone.InitialX}, vel: {hailstone.VelocityX} }},");
        // }
        //
        //
        // // Possible times
        // const int MaxTime = 100000;
        // var T = new float[MaxTime].AsSpan();
        // for (var i = 0; i < T.Length; i++)
        //     T[i] = i;
        //
        // // Precompute Xn + t.VXn for all Ts for hailstones 0 and 1
        // var X0_RHS = new float[MaxTime].AsSpan();
        // TensorPrimitives.Multiply(T, hailstones[0].VelocityX, X0_RHS);
        // TensorPrimitives.Add(X0_RHS, hailstones[0].InitialX, X0_RHS);
        //
        // var X1_RHS = new float[MaxTime].AsSpan();
        // TensorPrimitives.Multiply(T, hailstones[1].VelocityX, X1_RHS);
        // TensorPrimitives.Add(X1_RHS, hailstones[1].InitialX, X1_RHS);
        //
        // var X2_RHS = new float[MaxTime].AsSpan();
        // TensorPrimitives.Multiply(T, hailstones[2].VelocityX, X2_RHS);
        // TensorPrimitives.Add(X2_RHS, hailstones[2].InitialX, X2_RHS);
        //
        // var X3_RHS = new float[MaxTime].AsSpan();
        // TensorPrimitives.Multiply(T, hailstones[3].VelocityX, X3_RHS);
        // TensorPrimitives.Add(X3_RHS, hailstones[3].InitialX, X3_RHS);
        //
        // var X4_RHS = new float[MaxTime].AsSpan();
        // TensorPrimitives.Multiply(T, hailstones[4].VelocityX, X4_RHS);
        // TensorPrimitives.Add(X4_RHS, hailstones[4].InitialX, X4_RHS);
        //
        // // Check each possible value for vx
        // var X0_LHS = new float[MaxTime].AsSpan();
        // var X1_LHS = new float[MaxTime].AsSpan();
        // var X2_LHS = new float[MaxTime].AsSpan();
        // var X3_LHS = new float[MaxTime].AsSpan();
        // var X4_LHS = new float[MaxTime].AsSpan();
        //
        // var x_T0 = new List<int>(10000);
        // var x_T1 = new List<int>(10000);
        // var x_T2 = new List<int>(10000);
        // var x_T3 = new List<int>(10000);
        // var x_T4 = new List<int>(10000);
        // var x_v = new List<int>(10000);
        // var x_x = new List<int>(10000);
        //
        // for (var vx = -100000; vx < 1000; vx++)
        // {
        //     TensorPrimitives.Multiply(T, vx, X0_LHS);
        //     TensorPrimitives.Subtract(X4_RHS, X0_LHS, X4_LHS);
        //     TensorPrimitives.Subtract(X3_RHS, X0_LHS, X3_LHS);
        //     TensorPrimitives.Subtract(X2_RHS, X0_LHS, X2_LHS);
        //     TensorPrimitives.Subtract(X1_RHS, X0_LHS, X1_LHS);
        //     TensorPrimitives.Subtract(X0_RHS, X0_LHS, X0_LHS);
        //
        //     // Any matching values in X1_LHS / X0_LHS (any position) are possible values for this vx @ t
        //     var d1 = X1_LHS.ToArray().ToHashSet();
        //     
        //     for (var t0 = 0; t0 < MaxTime; t0++)
        //     {
        //         if (d1.Contains(X0_LHS[t0]))
        //         {
        //             var t1 = X1_LHS.IndexOf(X0_LHS[t0]);
        //             if (t1 != -1)
        //             {
        //                 var t2 = X2_LHS.IndexOf(X0_LHS[t0]);
        //                 if (t2 != -1)
        //                 {
        //                     var t3 = X3_LHS.IndexOf(X0_LHS[t0]);
        //                     if (t3 != -1)
        //                     {
        //                         var t4 = X4_LHS.IndexOf(X0_LHS[t0]);
        //                         if (t4 != -1)
        //                         {
        //                             Console.WriteLine($"vx = {vx},  x = {X0_LHS[t0]} @ t = {t0},{t1},{t2},{t3},{t4}");
        //                             x_T0.Add(t0);
        //                             return -1;
        //                             // x_T1.Add(t1);
        //                             // x_T2.Add(t2);
        //                             // x_T3.Add(t3);
        //                             // x_T4.Add(t4);
        //                             // x_v.Add(vx);
        //                             // x_x.Add((int)X0_LHS[t0]);
        //                         }
        //                     }
        //                 }
        //             }
        //         }
        //     }
        // }
        //
        // return x_T0.LongCount();
    }
}


// for (var t2 = 0; t2 < MaxTime; t2++)
// {
//     if (X0_LHS[t0] == X2_LHS[t2])
//     {
//         Console.WriteLine($"vx = {vx},  x = {X0_LHS[t0]} @ t = {t0},{t1},{t2}");
//         matched = true;
//         break;
//     }
// }
// if (matched) break;



//.  x + t1.vx = X1 + t1.VX1.  // 3 unknowns, x, vx and t1
//.  x + t2.vx = X2 + t2.VX2  // 1 extra unknown,  t2

// given a valid x, vx for t1,  then a valid value for t2 must exist.


//
//
// //
// //. x? + t1?.vx?,  y? + t1?.vy?, z? + t1?.vz? ===. X + t1?.VX,  Y + t1?.VY, Z + t1?.VZ // 7 unknowns
// //. x? + t2?.vx?,  y? + t2?.vy?, z? + t2?.vz? ===. X + t2?.VX,  Y + t2?.VY, Z + t2?.VZ // 1 extra unknown
// //. All ts >= 0
// //
//
//     public long Part2(string filename)
//     {
//         // var s = new VariableInteger("s", 0, 9);
//         // var e = new VariableInteger("e", 0, 9);
//         // var n = new VariableInteger("n", 0, 9);
//         // var d = new VariableInteger("d", 0, 9);
//         // var m = new VariableInteger("m", 1, 9);
//         // var o = new VariableInteger("o", 0, 9);
//         // var r = new VariableInteger("r", 0, 9);
//         // var y = new VariableInteger("y", 0, 9);
//         // var c0 = new VariableInteger("c0", 0, 1);
//         // var c1 = new VariableInteger("c1", 0, 1);
//         // var c2 = new VariableInteger("c2", 0, 1);
//         // var c3 = new VariableInteger("c3", 0, 1);
//         
//
//         
//      //   var variables = new [] { c0, c1, c2, c3, s, e, n, d, m, o, r, y };
//         // var state = new StateInteger(variables, constraints);
//         // var searchResult = state.Search();
//         //
//         // Console.WriteLine($"    {s} {e} {n} {d} ");
//         // Console.WriteLine($"  + {m} {o} {r} {e} ");
//         // Console.WriteLine("  ---------");
//         // Console.WriteLine($"  {m} {o} {n} {e} {y} ");
//         //
//         // Console.WriteLine($"Runtime:\t{state.Runtime}\nBacktracks:\t{state.Backtracks}\n");
//         
//         var hailstones = File.ReadAllLines(filename).Select(Parse).ToArray();
//
//         var x = new VariableInteger("x");
//         var y = new VariableInteger("y");
//         var z = new VariableInteger("z");
//         var vx = new VariableInteger("vx");
//         var vy = new VariableInteger("vy");
//         var vz = new VariableInteger("vz");
//         var t0 = new VariableInteger("t0",0,50000);
//         var t1 = new VariableInteger("t1",0,50000);
//         var t2 = new VariableInteger("t2",0,50000);
//         var t3 = new VariableInteger("t3",0,50000);
//         var t4 = new VariableInteger("t4",0,50000);
//         
//         
//         var constraints = new List<IConstraint>
//         {
//             new ConstraintInteger(x + (t0*vx) == 19 + (-2*t0)),
//             new ConstraintInteger(y + (t0*vy) == 13 + (1*t0)),
//             new ConstraintInteger(z + (t0*vz) == 30 + (-2*t0)),
//             new ConstraintInteger(x + (t1*vx) == 18 + (-1*t1)),
//             new ConstraintInteger(y + (t1*vy) == 19 + (-1*t1)),
//             new ConstraintInteger(z + (t1*vz) == 22 + (-2*t1)),
//             new ConstraintInteger(x + (t2*vx) == 20 + (-2*t2)),
//             new ConstraintInteger(y + (t2*vy) == 25 + (-2*t2)),
//             new ConstraintInteger(z + (t2*vz) == 34 + (-4*t2)),
//             new ConstraintInteger(x + (t3*vx) == 12 + (-1*t3)),
//             new ConstraintInteger(y + (t3*vy) == 31 + (-2*t3)),
//             new ConstraintInteger(z + (t3*vz) == 28 + (-1*t3)),
//             new ConstraintInteger(x + (t4*vx) == 20 + (1*t4)),
//             new ConstraintInteger(y + (t4*vy) == 19 + (-5*t4)),
//             new ConstraintInteger(z + (t4*vz) == 15 + (-3*t4)),
//             new AllDifferentInteger(new [] { x,y,z,vx,vy,vz,t0,t1,t2,t3,t4 }),
//         };
//         var variables = new [] { x,y,z,vx,vy,vz,t0,t1,t2,t3,t4 };
//
//
//                 
//         var state = new StateInteger(variables, constraints);
//         var searchResult = state.Search();
//         
//         Console.WriteLine($"    {x} {y} {z} ");
//         Console.WriteLine($"  @ {vx} {vy} {vz}");
//         
//         Console.WriteLine($"Runtime:\t{state.Runtime}\nBacktracks:\t{state.Backtracks}\n");
//
//
//         var vars = "";
//         Console.WriteLine("var constraints = new List<IConstraint>");
//         Console.WriteLine("{");
//         for (var i = 0; i < hailstones.Length; i++)
//         {
//             var hailstone1 = hailstones[i];
//             Console.WriteLine($"    new ConstraintInteger(x + (t{i}*vx) == {hailstone1.InitialX} + ({hailstone1.VelocityX}*t{i})),");
//             Console.WriteLine($"    new ConstraintInteger(y + (t{i}*vy) == {hailstone1.InitialY} + ({hailstone1.VelocityY}*t{i})),");
//             Console.WriteLine($"    new ConstraintInteger(z + (t{i}*vz) == {hailstone1.InitialZ} + ({hailstone1.VelocityZ}*t{i})),");
//             vars += $",t{i}";
//         }
//         Console.WriteLine($"    new AllDifferentInteger(new [] {{ x,y,z,vx,vy,vz{vars} }}),");
//         Console.WriteLine("};");
//         
//         Console.WriteLine($"var variables = new [] {{ x,y,z,vx,vy,vz{vars} }};");
//
//         
//         // var hailstone2 = hailstones[1];
//         // Console.WriteLine($"new ConstraintInteger(x + (t2*vx) == {hailstone2.InitialX} + ({hailstone2.VelocityY}*t2)),");
//         // Console.WriteLine($"new ConstraintInteger(y + (t2*vy) == {hailstone2.InitialY} + ({hailstone2.VelocityY}*t2)),");
//         // Console.WriteLine($"new ConstraintInteger(z + (t2*vz) == {hailstone2.InitialZ} + ({hailstone2.VelocityZ}*t2)),");
//         //
//         // var hailstone3 = hailstones[2];
//         // Console.WriteLine($"new ConstraintInteger(x + (t3*vx) == {hailstone3.InitialX} + ({hailstone3.VelocityY}*t3)),");
//         // Console.WriteLine($"new ConstraintInteger(y + (t3*vy) == {hailstone3.InitialY} + ({hailstone3.VelocityY}*t3)),");
//         // Console.WriteLine($"new ConstraintInteger(z + (t3*vz) == {hailstone3.InitialZ} + ({hailstone3.VelocityZ}*t3))");
//          return 0;
//     }
//
// }

// x + t1.vx = X1 + t1.VX1  // 3 unknowns, x, vx and t1
// y + t1.vy = Y1 + t1.VY1  // 5 unknowns, y, vy
// z + t1.vz = Z1 + t1.VZ1  // 7 unknowns, z, vz

// x + t2.vx = X2 + t2.VX1  // 8 unknowns t2
// y + t2.vy = Y2 + t2.VY2  // 8 unknowns
// z + t2.vz = Z2 + t2.VZ2  // 8 unknowns

// x + t3.vx = X3 + t3.VX3  // 9 unknowns t3
// y + t3.vy = Y3 + t3.VY3  // 9 unknowns
// z + t3.vz = Z3 + t3.VZ3  // 9 unknowns


// 19, 13, 30 @ -2,  1, -2
// 18, 19, 22 @ -1, -1, -2
// 20, 25, 34 @ -2, -2, -4
// 12, 31, 28 @ -1, -2, -1
// 20, 19, 15 @  1, -5, -3