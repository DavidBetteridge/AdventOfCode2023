namespace AOC_2023.Solutions;

public class Day05
{
    public long Part1(string filename)
    {
        var result = long.MaxValue;
        var text = File.ReadAllText(filename);

        var lrParser = new LRParser(text);

        var seeds = new List<long>();
        lrParser.Eat("seeds: ");
        do
        {
            seeds.Add(lrParser.EatLong());
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

    public long Part2(string filename)
    {
        var result = long.MaxValue;
        var text = File.ReadAllText(filename);
        var lrParser = new LRParser(text);

        var seeds = new List<(long start, long length)>();
        lrParser.Eat("seeds: ");
        do
        {
            var start = lrParser.EatLong();
            lrParser.EatWhitespace();
            var length = lrParser.EatLong();
            lrParser.EatWhitespace();
            seeds.Add((start, length));
        } while (!lrParser.TryEat('\n'));

        lrParser.Eat('\n');

        // Turn seeds into a map of ranges
        var seedMaps = seeds.Select(seedPair => new Map
        {
            SourceStart = seedPair.start,
            Offset = 0,
            SourceEnd = seedPair.start + seedPair.length - 1
        }).ToList();


        var seedToSoil = LoadMap(lrParser, "seed-to-soil");
        var soilTofFertilizer = LoadMap(lrParser, "soil-to-fertilizer");
        var fertilizerToWater = LoadMap(lrParser, "fertilizer-to-water");
        var waterToLight = LoadMap(lrParser, "water-to-light");
        var lightToTemperature = LoadMap(lrParser, "light-to-temperature");
        var temperatureToHumidity = LoadMap(lrParser, "temperature-to-humidity");
        var humidityToLocation = LoadMap(lrParser, "humidity-to-location");

        var sm = ApplyMap(seedMaps, seedToSoil);  //81
        sm = ApplyMap(sm, soilTofFertilizer);    //81
        sm = ApplyMap(sm, fertilizerToWater);    //81
        sm = ApplyMap(sm, waterToLight);   //74
        sm = ApplyMap(sm, lightToTemperature);  //78
        sm = ApplyMap(sm, temperatureToHumidity); //78
        sm = ApplyMap(sm, humidityToLocation); //82
        
        result = sm.Min(m => m.SourceStart);

        // foreach (var seedPair in seedMaps)
        // {
        //     for (var seed = seedPair.SourceStart; seed <= seedPair.SourceEnd; seed++)
        //     {
        //         var soil = Lookup(seedToSoil, seed);
        //         
        //         var fertilizer = Lookup(soilTofFertilizer, soil);
        //         
        //         var water = Lookup(fertilizerToWater, fertilizer);
        //         
        //         var light = Lookup(waterToLight, water);
        //         
        //         var temperature = Lookup(lightToTemperature, light);
        //         
        //         var humidity = Lookup(temperatureToHumidity, temperature);
        //
        //         var location = Lookup(humidityToLocation, humidity);
        //         result = Math.Min(result, location);
        //     }
        // }

        return result;
    }

    private List<Map> ApplyMap(List<Map> mapA, List<Map> mapB)
    {
        // For each map B, if B.Start or B.End falls in a map A, then split Map A
        // Then for each map A,  increase it, by it's map B offset.

        foreach (var map in mapB)
        {
            /*
             *     1..10
             *        10.20
             *     1..9  10.10
             */
            foreach (var toSplitSource in mapA.Where(r => map.SourceStart > r.SourceStart
                                                       && map.SourceStart <= r.SourceEnd).ToList())
            {
                var lhs = new Map
                {
                    SourceStart = toSplitSource.SourceStart,
                    SourceEnd = map.SourceStart - 1
                };

                var rhs = new Map
                {
                    SourceStart = map.SourceStart,
                    SourceEnd = toSplitSource.SourceEnd
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
            foreach (var toSplitTarget in mapA.Where(r => map.SourceEnd > r.SourceStart
                                                       && map.SourceEnd < r.SourceEnd).ToList())
            {
                var lhs = new Map
                {
                    SourceStart = toSplitTarget.SourceStart,
                    SourceEnd = map.SourceEnd
                };

                var rhs = new Map
                {
                    SourceStart = map.SourceEnd + 1,
                    SourceEnd = toSplitTarget.SourceEnd
                };

                mapA.Remove(toSplitTarget);
                mapA.Add(lhs);
                mapA.Add(rhs);
            }
        }

        // If range from A falls instead range from B,  then increase A by offset
        var result = new List<Map>();
        foreach (var map in mapA)
        {
            var target = mapB.SingleOrDefault(b => b.SourceStart <= map.SourceStart
                                                   && b.SourceEnd >= map.SourceEnd);
            if (target is not null)
            {
                result.Add(new Map
                {
                    SourceStart = map.SourceStart+ target.Offset,
                    SourceEnd = map.SourceEnd+ target.Offset
                });
            }
            else
            {
                result.Add(new Map
                {
                    SourceStart = map.SourceStart,
                    SourceEnd = map.SourceEnd
                });
            }
        }

        return result;
    }

    private List<Map> LoadMap(LRParser lrParser, string mapName)
    {
        var maps = new List<Map>();
        lrParser.Eat(mapName + " map:\n");
        do
        {
            do
            {
                var destination = lrParser.EatLong();
                lrParser.EatWhitespace();
                var source = lrParser.EatLong();
                lrParser.EatWhitespace();
                var range = lrParser.EatLong();
                lrParser.EatWhitespace();
                maps.Add(new Map
                {
                    SourceStart = source, // 10
                    Offset = destination - source, // 11-10 = 1
                    SourceEnd = source + range - 1 // 10 11 12
                });
            } while (!lrParser.TryEat('\n') && !lrParser.EOF);
        } while (!lrParser.TryEat('\n') && !lrParser.EOF);

        return maps;
    }

    private long Lookup(List<Map> maps, long value)
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
        public long SourceStart { get; set; }
        public long Offset { get; set; }
        public long SourceEnd { get; set; }
    }
}