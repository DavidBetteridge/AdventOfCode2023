
namespace AOC_2023.Solutions;

public class Day20
{
    private abstract class Module
    {
        private string[] _destinations = default!;
        private readonly Queue<(string source, string destination, bool isHigh)> _queue;
        public string Name { get; }

        public int LowCount { get; private set; }
        public int HighCount { get; private set; }

        public abstract void Receive(Dictionary<string, Module> allModules, string sender, bool isHigh);

        protected Module(Queue<(string source, string destination, bool isHigh)> queue, string name)
        {
            _queue = queue;
            Name = name;
        }
        protected void Send(Dictionary<string, Module> allModules, bool isHigh)
        {
            foreach (var destination in _destinations)
            {
                if (isHigh)
                    HighCount++;
                else
                    LowCount++;
                
                if (allModules.TryGetValue(destination, out var value))
                    _queue.Enqueue((Name, destination, isHigh));
            }
        }

        public void AddDestinations(string[] destinations)
        {
            _destinations = destinations;
        }

        public bool HasDestination(string destination)
        {
            return _destinations.Contains(destination);
        }
    }

    private class Button : Module
    {
        public override void Receive(Dictionary<string, Module> allModules, string sender, bool isHigh)
        {
        }

        public Button(Queue<(string source, string destination, bool isHigh)> queue) : base(queue, "button")
        {
        }

        public void Press(Dictionary<string, Module> allModules)
        {
            Send(allModules, false);
        }
    }
    
    private class Broadcaster : Module
    {
        public override void Receive(Dictionary<string, Module> allModules, string sender, bool isHigh)
        {
            Send(allModules, isHigh);
        }

        public Broadcaster(Queue<(string source, string destination, bool isHigh)> queue) : base(queue, "broadcaster")
        {
        }
    }

    private class Flipflop : Module
    {
        private bool _currentState = false;
        
        public override void Receive(Dictionary<string, Module> allModules, string sender, bool isHigh)
        {
            if (isHigh) return;
            _currentState = !_currentState;
            Send(allModules, _currentState);
        }

        public Flipflop(Queue<(string source, string destination, bool isHigh)> queue, string name) : base(queue, name)
        {
        }
    }
    
    private class Conjunction : Module
    {
        private Dictionary<string, bool>? _lastInputs = null;
        
        public override void Receive(Dictionary<string, Module> allModules, string sender, bool isHigh)
        {
            if (_lastInputs is null)
            {
                // Find all modules which have this as destination
                _lastInputs = new Dictionary<string, bool>();
                foreach (var module in allModules)
                {
                    if (module.Value.HasDestination(Name))
                        _lastInputs.Add(module.Key,false);
                }
            }
            
            _lastInputs[sender] = isHigh;
            Send(allModules, !_lastInputs.All(v => v.Value));
        }

        public Conjunction(Queue<(string source, string destination, bool isHigh)> queue, string name) : base(queue, name)
        {
        }
    }
    
    public long Part1(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var queue = new Queue<(string source, string destination, bool isHigh)>();
        var button = new Button(queue);
        var modules = ParseModules(lines, button, queue);

        for (var i = 0; i < 1000; i++)
        {
            button.Press(modules);
            while (queue.Any())
            {
                var (s, d, l) = queue.Dequeue();
                modules[d].Receive(modules, s, l);
            }
        }
        
        var totalLow = modules.Sum(m => m.Value.LowCount);
        var totalHigh = modules.Sum(m => m.Value.HighCount);
        return totalHigh * totalLow;
    }
    
    public long Part2(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var queue = new Queue<(string source, string destination, bool isHigh)>();
        var button = new Button(queue);
        var modules = ParseModules(lines, button, queue);
        
        // rx goes high when it receives high pulses from qs, sv, pg and sp
        // These are all inverters to we are interested in knowing when they
        // get low pules
        var qs = 0;
        var sv = 0;
        var pg = 0;
        var sp = 0;
        var i = 0;
        do
        {
            button.Press(modules);
            while (queue.Any())
            {
                var (s, d, l) = queue.Dequeue();
                modules[d].Receive(modules, s, l);

                if (d == "qs" && !l)
                    if (qs == 0)
                        qs = i + 1;
                if (d == "sv" && !l)
                    if (sv == 0)
                        sv = i + 1;
                if (d == "pg" && !l)
                    if (pg == 0)
                        pg = i + 1;
                if (d == "sp" && !l)
                    if (sp == 0)
                        sp = i + 1;
            }

            i++;
        } while (qs == 0 || sv == 0 || pg == 0 || sp == 0);
        
        // sp gets sent a low pulse every 3907 steps.  This then sends a high pulse to rx
        // sv gets sent a low pulse every 3919 steps.  This then sends a high pulse to rx
        // pg gets sent a low pulse every 3761 steps.  This then sends a high pulse to rx
        // qs gets sent a low pulse every 4051 steps.  This then sends a high pulse to rx        
        var totalStepsRequired =  Lcm(qs, Lcm( sv, Lcm(pg, sp)));
        return totalStepsRequired;
    }

    private static Dictionary<string, Module> ParseModules(
        string[] lines, 
        Button button,
        Queue<(string source, string destination, bool isHigh)> queue)
    {
        using var markdown = File.CreateText("/Users/davidbetteridge/Personal/AdventOfCode2023/AOC_2023/AOC_2023.Tests/Day20/input.md");
        markdown.WriteLine("```mermaid\n  graph TD;");
        
        var modules = new Dictionary<string, Module>();
        foreach (var line in lines)
        {
            var parts = line.Split(" -> ");

            Module? module = null;
            if (parts[0] == "broadcaster")
            {
                module = new Broadcaster(queue);
            }
            else if (parts[0][0] == '%')
            {
                module = new Flipflop(queue, parts[0][1..]);
            }
            else if (parts[0][0] == '&')
            {
                module = new Conjunction(queue, parts[0][1..]);
            }

            module!.AddDestinations(parts[1].Split(", "));
            modules.Add(module.Name, module);

            // Write
            foreach (var dest in parts[1].Split(", "))
            {
                markdown.WriteLine($"       {module.Name} --> {dest}");
            }
        }

        markdown.WriteLine("```");
        markdown.Close();
        
        button.AddDestinations(new[] { "broadcaster" });
        modules.Add("button", button);
        return modules;
    }


    private long Gcd(long a, long b)
    {
        while (true)
        {
            if (b == 0) return a;
            var a1 = a;
            a = b;
            b = a1 % b;
        }
    }

    private long Lcm(long a, long b)
    {
        if (a > b)
            return (a / Gcd(a, b)) * b;
        else
            return (b / Gcd(a, b)) * a;
    }
    

    
}

