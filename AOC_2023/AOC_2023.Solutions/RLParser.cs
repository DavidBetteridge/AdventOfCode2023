namespace AOC_2023.Solutions;

public ref struct RLParser
{
    private ReadOnlySpan<char> _line;

    public void Reset(string line)
    {
        _line = line.AsSpan();
    }

    public int EatNumber()
    {
        var length = 0;
        while ((_line.Length - length >= 0) && char.IsDigit(_line[^(1+length)]))
            length++;
        var value = int.Parse(_line[^length..]);
        _line=_line[..^(1+length)];
        
        while (_line.Length>=0 && char.IsWhiteSpace(_line[^1]))
            _line=_line[..^1];
        
        return value;
    }

    public bool TryNotEat(char match)
    {
        if (_line.Length >= 0 && _line[^1] == match)
        {
            _line=_line[..^1];
            return false;
        }
        return true;
    }
    
    public void EatWhitespace()
    {
        while (_line.Length>=0 && char.IsWhiteSpace(_line[^1]))
            _line=_line[..^1];
    }
}