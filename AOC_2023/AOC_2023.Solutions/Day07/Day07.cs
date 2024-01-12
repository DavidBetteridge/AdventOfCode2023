namespace AOC_2023.Solutions;

public class Day07
{
    private Dictionary<char, int> Part1Map = new()
    {
        ['2'] = 2, ['3'] = 3, ['4'] = 4, ['5'] = 5, ['6'] = 6, ['7'] = 7, ['8'] = 8, ['9'] = 9,
        ['T'] = 10, ['J'] = 11, ['Q'] = 12, ['K'] = 13, ['A'] = 14
    };

    private Dictionary<char, int> Part2Map = new()
    {
        ['J'] = 1, ['2'] = 2, ['3'] = 3, ['4'] = 4, ['5'] = 5, ['6'] = 6, ['7'] = 7, ['8'] = 8, ['9'] = 9,
        ['T'] = 10, ['Q'] = 12, ['K'] = 13, ['A'] = 14
    };
    
    private record Hand
    {
        public int HandType { get; set; }
        public int Bid { get; set; }
    }
    
    public int Part1(string filename) => Solve(filename, Part1Map);

    public int Part2(string filename) => Solve(filename, Part2Map);

    private int Solve(string filename, Dictionary<char, int> map)
    {
        var lines = File.ReadLines(filename);
        var lrParser = new LRParser();

        var hands = new List<Hand>();
        var values = new int[15];
        
        foreach (var line in lines)
        {
            lrParser.Reset(line);
            Array.Fill(values,0);
            
            var handTotal = 0;
            for (var i = 0; i < 5; i++)
            {
                var card = lrParser.EatChar();
                var value = map[card];
                handTotal = (handTotal << 4) + value;
                values[value]+=1;
            }

            var handType = 0;
            var numberOfJokers = values[1];
            values[1] = 0;

            var longestRun = values.Max();
            var pairs = values.Count(v => v == 2);
            
            if (longestRun + numberOfJokers >= 5)
                handType = 6 << 20;  //Five of a kind  kkkkJ
            
            else if (longestRun + numberOfJokers >= 4)
                handType = 5 << 20;  //Four of a kind  kkkJ?
            
            else if (longestRun == 3 && pairs==1)
                handType = 4 << 20;  //Full house  AAAKK
            
            else if (longestRun + numberOfJokers >= 3 && pairs==2)
                handType = 4 << 20;  //Full house  AAKKJ
            
            else if (longestRun + numberOfJokers >= 3)
                handType = 3 << 20;  //Three of a kind   AAJ45    
            
            else if (pairs + numberOfJokers >= 2)
                handType = 2 << 20;  //Two pair AJKK4
            
            else if (longestRun + numberOfJokers >= 2)
                handType = 1 << 20; //One pair AJ456
            else
                handType = 0;  //High card
            
            lrParser.EatWhitespace();
            var bid = lrParser.EatNumber();

            var hand = new Hand { HandType = handType + handTotal, Bid = bid };
            hands.Add(hand);
        }

        var score = hands.OrderBy(h => h.HandType)
            .Select((h, i) => h.Bid * (i + 1))
            .Sum();
        
        return score;
    }
}