namespace AOC_2023.Solutions;

public class LRParser
{
    private string _line = "";
    private int _offset;

    public LRParser()
    {
        Reset("");
    }
    
    public LRParser(string line)
    {
        Reset(line);
    }

    public void Reset(string line)
    {
        _line = line;
        _offset = 0;
    }
    
    public void Eat(char match)
    {
        if (_line[_offset] == match)
            _offset += 1;
        else
            throw new Exception($"{match} not found at position {_offset}");
    }
    public void Eat(string match)
    {
        if (_line[_offset..(_offset + match.Length)] == match)
            _offset += match.Length;
        else
            throw new Exception($"{match} not found at position {_offset}");
    }

    public ulong EatULong()
    {
        var length = 0;
        while ((_offset + length < _line.Length) && char.IsDigit(_line[_offset+length]))
            length++;
        var value = ulong.Parse(_line[_offset..(_offset + length)]);
        _offset += length;
        return value;
    }
    public int EatNumber()
    {
        var length = 0;
        while ((_offset + length < _line.Length) && char.IsDigit(_line[_offset+length]))
            length++;
        var value = int.Parse(_line[_offset..(_offset + length)]);
        _offset += length;
        return value;
    }

    public string EatWord()
    {
        var length = 0;
        while ((_offset + length < _line.Length) && char.IsLetter(_line[_offset+length]))
            length++;
        var value = _line[_offset..(_offset + length)];
        _offset += length;
        return value;
    }
        
    public bool EOF => (_offset + 1) > _line.Length;

    public bool TryEat(char match)
    {
        if (!EOF && _line[_offset] == match)
        {
            _offset += 1;
            return true;
        }
        return false;
    }
    
    public bool TryEat(string match)
    {
        if (!EOF && _line[_offset..(_offset + match.Length)] == match)
        {
            _offset += match.Length;
            return true;
        }
        return false;
    }

    public void EatWhitespace()
    {
        while (!EOF && _line[_offset] == ' ')
            _offset++;
    }
}