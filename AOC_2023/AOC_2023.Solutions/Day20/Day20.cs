
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

        public Module(Queue<(string source, string destination, bool isHigh)> queue, string name)
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
                
               // Console.WriteLine($"{Name} -{(isHigh?"high":"low")}-> {destination}");
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
        }
        
        var button = new Button(queue);
        button.AddDestinations(new []{"broadcaster"});
        modules.Add("button", button);

        //Console.WriteLine("Press 1");

        for (var i = 0; i < 1000; i++)
        {
            button.Press(modules);
            while (queue.Any())
            {
                var (s, d, l) = queue.Dequeue();
                modules[d].Receive(modules, s, l);
            }
        }

        // Console.WriteLine("Press 1");
        // button.Press(modules);
        //
        // Console.WriteLine();
        // Console.WriteLine("Press 2");
        // button.Press(modules);
        //
        // Console.WriteLine();
        // Console.WriteLine("Press 3");
        // button.Press(modules);
        //
        // Console.WriteLine();
        // Console.WriteLine("Press 4");
        // button.Press(modules);
       
        // for (var i = 0; i < 1000; i++)
        //     button.Press(modules);
        
        var totalLow = modules.Sum(m => m.Value.LowCount);
        var totalHigh = modules.Sum(m => m.Value.HighCount);
        return totalHigh * totalLow;
    }
    public long Part2(string filename)
    {
        return 0;
    }
    
}

