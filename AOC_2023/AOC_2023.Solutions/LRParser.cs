namespace AOC_2023.Solutions;

public ref struct LRParser
{
    private ReadOnlySpan<char> _line;

    public LRParser(string line)
    {
        Reset(line);
    }

    public void Reset(string line)
    {
        _line = line.AsSpan();
    }
    
    public void Eat(string match)
    {
        if (_line.StartsWith(match))
            _line = _line[match.Length..];
        else
            throw new Exception($"{match} not found.");
    }

    public void Eat(char match)
    {
        if (_line[0] == match)
            _line = _line[1..];
        else
            throw new Exception($"{match} not found.");
    }
    
    public int EatNumber()
    {
        var length = 0;
        while (length < _line.Length && (char.IsDigit(_line[length]) || _line[length] == '-' ))
            length++;
        var value = int.Parse(_line[..length]);
        _line = _line[length..];
        return value;
    }

    public string EatWord()
    {
        var length = 0;
        while (length < _line.Length && char.IsLetterOrDigit(_line[length]))
            length++;
        var value = _line[..length];
        _line = _line[length..];
        return value.ToString();
    }
     
    public long EatLong()
    {
        var length = 0;
        while (length < _line.Length && char.IsLetterOrDigit(_line[length]))
            length++;
        var value = _line[..length];
        _line = _line[length..];
        return long.Parse(value);
    }
    
    public bool EOF => _line.Length == 0;

    public bool TryEat(char match)
    {
        if (!EOF && _line[0] == match)
        {
            _line = _line[1..];
            return true;
        }
        return false;
    }
    
    public bool TryEat(string match)
    {
        if (_line.StartsWith(match))
        {
            _line = _line[(match.Length)..];
            return true;
        }
        return false;
    }
    
    public void EatWhitespace()
    {
        while (!EOF && _line[0] == ' ')
            _line = _line[1..];
    }

    public char EatChar()
    {
        var c = _line[0];
        _line = _line[1..];
        return c;
    }
}