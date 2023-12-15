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

    public record Lens
    {
        public string Label { get; set; }
        public int Length { get; set; }
    }
    
    public long Part2(string filename)
    {
        var input = File.ReadAllText(filename);

        var parser = new LRParserSpan(input);
        var boxes = new List<Lens>[256];

        for (var i = 0;i < 256; i++)
            boxes[i] = new List<Lens>();
        
        do
        {
            var label = "";
            var boxNumber = 0;
            char c;
            do
            {
                c = parser.EatChar();
                if (char.IsLetter(c))
                {
                    boxNumber += c;
                    boxNumber *= 17;
                    boxNumber %= 256;
                    label += c;
                }
            } while (char.IsLetter(c));

            
            
            
            if (c == '=')
            {
                var length = parser.EatNumber();
                
                var existingLens = boxes[boxNumber].FirstOrDefault(l => l.Label == label);
                if (existingLens is null)
                    boxes[boxNumber].Add(new Lens { Label = label, Length = length });
                else
                    existingLens.Length = length;
            }
            else
            {
                // Remove the existing lens from the box
                boxes[boxNumber].RemoveAll(l => l.Label == label);
            }
        } while (parser.TryEat(',') && !parser.EOF);

        var result = 0;
        for (var box = 0; box < 256; box++)
        {
            for (var slot = 0; slot < boxes[box].Count; slot++)
            {
                result += ((slot + 1) * (box+1) * boxes[box][slot].Length);
            }
        }
        
        return result;
    }
}