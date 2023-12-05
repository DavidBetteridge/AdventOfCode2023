using System.Management;

namespace AOC_2023.Solutions;

public class Day05
{
    public ulong Part1(string filename)
    {
        var result = ulong.MaxValue;
        var text = File.ReadAllText(filename);

        var lrParser = new LRParser(text);

        var seeds = new List<ulong>();
        lrParser.Eat("seeds: ");
        do
        {
            seeds.Add(lrParser.EatULong());
            lrParser.EatWhitespace();
        } while (!lrParser.TryEat('\n'));

        lrParser.Eat('\n');


        var seedToSoil = LoadMap(lrParser, "seed-to-soil");
        var soilTofFertilizer = LoadMap(lrParser, "soil-to-fertilizer");
        var fertilizerToWater = LoadMap(lrParser, "fertilizer-to-water");
        var waterToLight = LoadMap(lrParser, "water-to-light");
        var lightToTemperature = LoadMap(lrParser, "light-to-temperature");
        var temperatureToHumidity = LoadMap(lrParser, "temperature-to-humidity");
        var humidityToLocation = LoadMap(lrParser, "humidity-to-location");

        foreach (var seed in seeds)
        {
            var soil = Lookup(seedToSoil, seed);
            var fertilizer = Lookup(soilTofFertilizer, soil);
            var water = Lookup(fertilizerToWater, fertilizer);
            var light = Lookup(waterToLight, water);
            var temperature = Lookup(lightToTemperature, light);
            var humidity = Lookup(temperatureToHumidity, temperature);
            var location = Lookup(humidityToLocation, humidity);
            result = Math.Min(result, location);
        }

        return result;
    }

    public ulong Part2(string filename)
    {
        var result = ulong.MaxValue;
        var text = File.ReadAllText(filename);
        var lrParser = new LRParser(text);

        var seeds = new List<(ulong start, ulong length)>();
        lrParser.Eat("seeds: ");
        do
        {
            var start = lrParser.EatULong();
            lrParser.EatWhitespace();
            var length = lrParser.EatULong();
            lrParser.EatWhitespace();
            seeds.Add((start, length));
        } while (!lrParser.TryEat('\n'));

        lrParser.Eat('\n');

        var seedToSoil = LoadMap(lrParser, "seed-to-soil");
        var soilTofFertilizer = LoadMap(lrParser, "soil-to-fertilizer");
        var fertilizerToWater = LoadMap(lrParser, "fertilizer-to-water");
        var waterToLight = LoadMap(lrParser, "water-to-light");
        var lightToTemperature = LoadMap(lrParser, "light-to-temperature");
        var temperatureToHumidity = LoadMap(lrParser, "temperature-to-humidity");
        var humidityToLocation = LoadMap(lrParser, "humidity-to-location");

        // A Seeds 50..100   150...250
        
        // B Fertilizer 40..110  (+10)   160..220 +(20)
        
        // If StartB inside rangeA,  then split rangeA > Start
        //     50..100   150..159  160..250
        
        // If EndB inside rangeA,  then split rangeA
        //    50.100   150..159   160..220  221...250
        
        // If range from A falls instead range from B,  then increase A by offset
     //   var merged = ApplyMap(ApplyMap(ApplyMap(seedToSoil, soilTofFertilizer),fertilizerToWater), waterToLight);
        
        
        foreach (var seedPair in seeds)
        {
            for (ulong seedOffset = 0; seedOffset < seedPair.length; seedOffset++)
            {
                var seed = seedPair.start + seedOffset;
               var soil = Lookup(seedToSoil, seed);
               var fertilizer = Lookup(soilTofFertilizer, soil);
             //   var light = Lookup(merged, seed);
              var water = Lookup(fertilizerToWater, fertilizer);
              var light = Lookup(waterToLight, water);
                var temperature = Lookup(lightToTemperature, light);
                var humidity = Lookup(temperatureToHumidity, temperature);
                
                var location = Lookup(humidityToLocation, humidity);
                result = Math.Min(result, location);
            }
        }

        return result;
    }

    private List<Map> ApplyMap(List<Map> mapA, List<Map> mapB)
    {
                
        // map A
        // map B
        
        // For each map B, if B.Start or B.End falls in a map A, then split Map A
        // Then for each map A,  increase it, by it's map B offset.

        foreach (var map in mapB)
        {
            /*
             *     1..10
             *        10.20
             *     1..9  10.10
             */
            var toSplitSource = mapA.SingleOrDefault(r => r.SourceStart > map.SourceStart 
                                                    && r.SourceEnd <= map.SourceStart);
            if (toSplitSource is not null)
            {
                var lhs = new Map
                {
                    SourceStart = toSplitSource.SourceStart,
                    SourceEnd = map.SourceStart - 1,
                    Offset = toSplitSource.Offset
                };
                
                var rhs = new Map
                {
                    SourceStart = map.SourceStart,
                    SourceEnd = map.SourceEnd,
                    Offset = toSplitSource.Offset
                };

                mapA.Remove(toSplitSource);
                mapA.Add(lhs);
                mapA.Add(rhs);
            }
            
            /*
             *       10.20
             *   1..11    
             *     10..11  12.20
             */
            var toSplitTarget = mapA.SingleOrDefault(r => r.SourceStart >= map.SourceEnd 
                                                          && r.SourceEnd < map.SourceEnd);
            if (toSplitTarget is not null)
            {
                var lhs = new Map
                {
                    SourceStart = toSplitTarget.SourceStart,
                    SourceEnd = map.SourceEnd,
                    Offset = toSplitTarget.Offset
                };
                
                var rhs = new Map
                {
                    SourceStart = map.SourceEnd+1,
                    SourceEnd = toSplitTarget.SourceEnd,
                    Offset = toSplitTarget.Offset
                };
            
                mapA.Remove(toSplitTarget);
                mapA.Add(lhs);
                mapA.Add(rhs);
            }
        }
        
        // If range from A falls instead range from B,  then increase A by offset
        foreach (var map in mapA)
        {
            var target = mapB.SingleOrDefault(b => b.SourceStart <= map.SourceStart 
                                                   && b.SourceEnd >= map.SourceEnd);
            if (target is not null)
            {
                map.SourceStart += target.Offset;
                map.SourceEnd += target.Offset;
            }
        }
        
        return mapA;
    }

    private List<Map> LoadMap(LRParser lrParser, string mapName)
    {
        var maps = new List<Map>();
        lrParser.Eat(mapName + " map:\n");
        do
        {
            do
            {
                var destination = lrParser.EatULong();
                lrParser.EatWhitespace();
                var source = lrParser.EatULong();
                lrParser.EatWhitespace();
                var range = lrParser.EatULong();
                lrParser.EatWhitespace();
                maps.Add(new Map
                {
                    SourceStart = source,   // 10
                    Offset = destination - source,   // 11-10 = 1
                    SourceEnd = source + range - 1 // 10 11 12
                });
            } while (!lrParser.TryEat('\n') && !lrParser.EOF);
        } while (!lrParser.TryEat('\n') && !lrParser.EOF);

        return maps;
    }

    private ulong Lookup(List<Map> maps, ulong value)
    {
        foreach (var map in maps)
        {
            if (value >= map.SourceStart && value <= map.SourceEnd)
            {
                return value + map.Offset;
            }
        }

        return value;
    }

    private record Map
    {
        public ulong SourceStart { get; set; }
        public ulong Offset { get; set; }
        public ulong SourceEnd { get; set; }
    }
}