namespace AOC_2023.Solutions;

public class RLParser
{
    private string _line = "";
    private int _offset;
    
    public void Reset(string line)
    {
        _line = line;
        _offset = line.Length-1;
    }

    public int EatNumber()
    {
        var length = 0;
        while ((_offset - length >= 0) && char.IsDigit(_line[_offset-length]))
            length++;
        var value = int.Parse(_line[(_offset - length)..(_offset+1)]);
        _offset -= length;
        return value;
    }

    public bool TryEat(char match)
    {
        if (_offset >= 0 && _line[_offset] == match)
        {
            _offset -= 1;
            return true;
        }
        return false;
    }

    public void EatWhitespace()
    {
        while (_offset>=0 && char.IsWhiteSpace(_line[_offset]))
            _offset--;
    }
}