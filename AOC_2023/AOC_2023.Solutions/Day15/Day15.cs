namespace AOC_2023.Solutions;

public class Day15
{
    public long Part1(string filename)
    {
        var input = File.ReadAllText(filename);
        var result = 0;

        var parser = new LRParserSpan(input);
        do
        {
            var currentValue = 0;
            do
            {
                currentValue += parser.EatChar();
                currentValue *= 17;
                currentValue %= 256;
            } while (!parser.TryEat(',') && !parser.EOF);
            result += currentValue;
        } while (!parser.EOF);

        return result;
    }
}