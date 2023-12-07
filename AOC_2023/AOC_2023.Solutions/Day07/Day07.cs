namespace AOC_2023.Solutions;

public class Day07
{
    private Dictionary<char, int> Map = new Dictionary<char, int>()
    {
        ['0'] = 0, ['1'] = 1, ['2'] = 2, ['3'] = 3, ['4'] = 4, ['5'] = 5, ['6'] = 6, ['7'] = 7, ['8'] = 8, ['9'] = 9,
        ['T'] = 10, ['J'] = 11, ['Q'] = 12, ['K'] = 13, ['A'] = 14
    };

    private record Hand
    {
        public int HandTotal { get; set; }
        public int HandType { get; set; }
        public int Bid { get; set; }
    }
    
    public int Part1(string filename)
    {
        var lines = File.ReadLines(filename);
        var lrParser = new LRParserSpan();

        var hands = new List<Hand>();
        foreach (var line in lines)
        {
            lrParser.Reset(line);

            var handTotal = 0;
            var values = new int[15];
            for (var i = 0; i < 5; i++)
            {
                var card = lrParser.EatChar();
                var value = Map[card];
                handTotal = (handTotal * 15) + value;
                values[value]+=1;
            }

            var handType = 0;
            var pairs = values.Count(v => v == 2);
            if (values.Any(v => v == 5))
                handType = 6;  //Five of a kind
            else if (values.Any(v => v == 4))
                handType = 5;  //Four of a kind
            else if (values.Any(v => v == 3) && pairs==1)
                handType = 4;  //Full house
            else if (values.Any(v => v == 3))
                handType = 3;  //Three of a kind      
            else if (pairs == 2)
                handType = 2;  //Two pair
            else if (values.Any(v => v == 2))
                handType = 1; //One pair
            else
                handType = 0;  //High card
            
            lrParser.EatWhitespace();
            var bid = lrParser.EatNumber();

            var hand = new Hand { HandTotal = handTotal, HandType = handType, Bid = bid };
            hands.Add(hand);
        }

        var score = hands.OrderBy(h => h.HandType)
            .ThenBy(h => h.HandTotal)
            .Select((h, i) => h.Bid * (i + 1))
            .Sum();
        
        return score;
    }
}