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


        var seedToSoil = LoadMap(ref lrParser, "seed-to-soil");
        var soilTofFertilizer = LoadMap(ref lrParser, "soil-to-fertilizer");
        var fertilizerToWater = LoadMap(ref lrParser, "fertilizer-to-water");
        var waterToLight = LoadMap(ref lrParser, "water-to-light");
        var lightToTemperature = LoadMap(ref lrParser, "light-to-temperature");
        var temperatureToHumidity = LoadMap(ref lrParser, "temperature-to-humidity");
        var humidityToLocation = LoadMap(ref lrParser, "humidity-to-location");

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
        var text = File.ReadAllText(filename);
        var lrParser = new LRParser(text);

        var seeds = new List<Map>();
        lrParser.Eat("seeds: ");
        do
        {
            var start = lrParser.EatLong();
            lrParser.EatWhitespace();
            var length = lrParser.EatLong();
            lrParser.EatWhitespace();
            seeds.Add(new Map
            {
                SourceStart = start,
                SourceEnd = start + length - 1
            });
        } while (!lrParser.TryEat('\n'));
        lrParser.Eat('\n');

        var maps = new List<Map>();
        foreach (var mapName in new []
                 {
                     "seed-to-soil","soil-to-fertilizer","fertilizer-to-water","water-to-light",
                     "light-to-temperature","temperature-to-humidity","humidity-to-location"
                 })
        {
            maps.Clear();
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
            
            ApplyMap(seeds, maps);
        }

        return seeds.Min(m => m.SourceStart);
    }

    private void ApplyMap(List<Map> mapA, List<Map> mapB)
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
                                                          && map.SourceStart <= r.SourceEnd).ToArray())
            {
                mapA.Add(new Map
                {
                    SourceStart = toSplitSource.SourceStart,
                    SourceEnd = map.SourceStart - 1
                });
                toSplitSource.SourceStart = map.SourceStart;
                break;
            }

            /*
             *       10.20
             *   1..11
             *     10..11  12.20
             */
            foreach (var toSplitTarget in mapA.Where(r => map.SourceEnd > r.SourceStart
                                                          && map.SourceEnd < r.SourceEnd).ToArray())
            {
                mapA.Add(new Map
                {
                    SourceStart = toSplitTarget.SourceStart,
                    SourceEnd = map.SourceEnd
                });
                toSplitTarget.SourceStart = map.SourceEnd + 1;
                break;
            }
        }

        // If range from A falls instead range from B,  then increase A by offset
        foreach (var map in mapA)
        {
            foreach (var target in mapB.Where(b => b.SourceStart <= map.SourceStart
                                                   && b.SourceEnd >= map.SourceEnd))
            {
                map.SourceStart += target.Offset;
                map.SourceEnd += target.Offset;
                break;
            }
        }
    }

    private List<Map> LoadMap(ref LRParser lrParser, string mapName)
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

    private class Map
    {
        public long SourceStart { get; set; }
        public long Offset { get; set; }
        public long SourceEnd { get; set; }
    }
}