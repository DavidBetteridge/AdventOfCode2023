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
        
        foreach (var seedPair in seeds)
        {
            for (ulong seedOffset = 0; seedOffset < seedPair.length; seedOffset++)
            {
                var seed = seedPair.start + seedOffset;
                var soil = Lookup(seedToSoil, seed);
                var fertilizer = Lookup(soilTofFertilizer, soil);
                var water = Lookup(fertilizerToWater, fertilizer);
                var light = Lookup(waterToLight, water);
                var temperature = Lookup(lightToTemperature, light);
                var humidity = Lookup(temperatureToHumidity, temperature);
                var location = Lookup(humidityToLocation, humidity);
                result = Math.Min(result, location);
                
                // Skip any seeds which will give the same answer
              //  seedOffset += rangeRemaining;
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
                var destination = lrParser.EatULong();
                lrParser.EatWhitespace();
                var source = lrParser.EatULong();
                lrParser.EatWhitespace();
                var range = lrParser.EatULong();
                lrParser.EatWhitespace();
                maps.Add(new Map { Source = source, Destination = destination, Range = range });
            } while (!lrParser.TryEat('\n') && !lrParser.EOF);
        } while (!lrParser.TryEat('\n') && !lrParser.EOF);

        return maps;
    }

    private ulong Lookup(List<Map> maps, ulong value)
    {
        foreach (var map in maps)
        {
            if (value >= map.Source)
            {
                var offset = value - map.Source;
                if (offset < map.Range)
                    return map.Destination + offset;
            }
        }

        return value;
    }

    private record Map
    {
        public ulong Source { get; set; }
        public ulong Destination { get; set; }
        public ulong Range { get; set; }
    }
}