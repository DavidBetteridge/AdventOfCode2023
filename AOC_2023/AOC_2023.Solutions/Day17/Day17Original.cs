namespace AOC_2023.Solutions;

public class Day17Original
{
    private record State
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public int Heatloss { get; set; }
        public int RunLength { get; set; }
        public char Direction { get; set; }
    }
   
    public long Part1(string filename)
    {
        var bestScore = 1000;// int.MaxValue;
        var map = File.ReadAllLines(filename).Select(line => line.Select(c => c - '0').ToArray()).ToArray();
        var targetRow = map.Length - 1;
        var targetColumn = map[0].Length - 1;
        var final = map[^1][^1];

        var seen = new Dictionary<(int col, int row, char direction, int runLength), int>();  //Score
        
        Step(new State
        {
            Row = 0,
            Direction = ' ',
            Col = 0,
            RunLength = 0,
            Heatloss = 0
        });
        
        void Step(State currentState)
        {
            if (currentState.Row == targetRow && currentState.Col == targetColumn)
            {
                bestScore = Math.Min(bestScore, currentState.Heatloss);
                Console.WriteLine(bestScore);
                return;
            }
            
            var estimate = (targetRow - currentState.Row) + (targetColumn - currentState.Col) -1 + final;
            if (currentState.Heatloss+estimate > bestScore)
                return;
    
            // Have we been here before?
            var h = currentState.RunLength;
            while (h >= 1)
            {
                if (seen.TryGetValue(
                        (currentState.Col, currentState.Row, currentState.Direction, h),
                        out var score))
                {
                    if (currentState.Heatloss >= score) return;
                }
                h--;
            }
            seen[(currentState.Col, currentState.Row, currentState.Direction, currentState.RunLength)] = currentState.Heatloss;
       
            // Down
            if (currentState.Row < targetRow &&
                currentState.Direction != 'U' &&
                (currentState.Direction != 'D' || currentState.RunLength < 3))
            {
                Step(new State
                {
                    Row = currentState.Row + 1,
                    Direction = 'D',
                    Col = currentState.Col,
                    RunLength = currentState.Direction == 'D' ? currentState.RunLength + 1 : 1,
                    Heatloss = currentState.Heatloss + map[currentState.Row + 1][currentState.Col]
                });
            }
            
            // Right
            if (currentState.Col < targetColumn &&
                currentState.Direction != 'L' &&
                (currentState.Direction != 'R' || currentState.RunLength < 3))
            {
                Step(new State
                {
                    Row = currentState.Row ,
                    Direction = 'R',
                    Col = currentState.Col + 1,
                    RunLength = currentState.Direction == 'R' ? currentState.RunLength + 1 : 1,
                    Heatloss = currentState.Heatloss + map[currentState.Row ][currentState.Col + 1]
                });
            }
            
            // Up
            if (currentState.Row > 0 &&
                currentState.Direction != 'D' &&
                (currentState.Direction != 'U' || currentState.RunLength < 3))
            {
                Step(new State
                {
                    Row = currentState.Row - 1,
                    Direction = 'U',
                    Col = currentState.Col,
                    RunLength = currentState.Direction == 'U' ? currentState.RunLength + 1 : 1,
                    Heatloss = currentState.Heatloss + map[currentState.Row - 1][currentState.Col]
                });
            }
            
            // Left
            if (currentState.Col > 0 &&
                currentState.Direction != 'R' &&
                (currentState.Direction != 'L' || currentState.RunLength < 3))
            {
                Step(new State
                {
                    Row = currentState.Row ,
                    Direction = 'L',
                    Col = currentState.Col- 1,
                    RunLength = currentState.Direction == 'L' ? currentState.RunLength + 1 : 1,
                    Heatloss = currentState.Heatloss + map[currentState.Row ][currentState.Col - 1]
                });
            }
        }

        return bestScore;
    }
}