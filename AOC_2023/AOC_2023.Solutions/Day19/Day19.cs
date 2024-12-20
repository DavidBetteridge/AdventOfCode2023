namespace AOC_2023.Solutions;

public class Day19
{
    private record Part
    {
        public int X { get; set; }
        public int M { get; set; }
        public int A { get; set; }
        public int S { get; set; }
    }

    private record Condition
    {
        public char Variable { get; set; }
        public char Op { get; set; }
        public int Value { get; set; }
    }
    
    private record Rule
    {
        public Condition Condition { get; set; }
        public string Action { get; set; }
    }

    private record Workflow
    {
        public string Name { get; set; }
        public List<Rule> Rules { get; set; }
    }

    private Part LoadPart(string line)
    {
        var bits = line[..^1].Split(',');
        return new Part
        {
            X = int.Parse(bits[0].Split('=')[1]),
            M = int.Parse(bits[1].Split('=')[1]),
            A = int.Parse(bits[2].Split('=')[1]),
            S = int.Parse(bits[3].Split('=')[1]),
        };
    }
    
    private Workflow LoadWorkflow(string line)
    {
        var bits = line[..^1].Split('{');
        var rs = bits[1].Split(',');
        return new Workflow
        {
            Name = bits[0],
            Rules = rs[..^1].Select(r => new Rule
            {
                Action = r.Split(':')[1], 
                Condition = LoadCondition(r.Split(':')[0])
            }).Append(new Rule
            {
                Action = rs[^1],
                Condition = new Condition { Variable = 's', Op ='>', Value =-1}
            })
                .ToList()
        };
    }

    private Condition LoadCondition(string text)
    {
        return new Condition
        {
            Variable = text[0],
            Op = text[1],
            Value = int.Parse(text[2..])
        };
    }

    private Condition ReverseCondition(Condition condition)
    {
        return new Condition
        {
            Variable = condition.Variable,
            Op = condition.Op == '>' ? '<' : '>',
            Value = condition.Op == '>' ? condition.Value+1 : condition.Value-1,
        };
    }
    
    
    public long Part1(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var workflows = new Dictionary<string,Workflow>();
        var parts = new List<Part>();
        var loadWorkflows = true;
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                loadWorkflows = false;
            else if (loadWorkflows)
            {
                var workflow = LoadWorkflow(line);
                workflows.Add(workflow.Name, workflow);
            }
            else
                parts.Add(LoadPart(line));
        }

        // Combine the workflows
        var paths = new List<List<Condition>>();
        Combine(workflows, "in", new Stack<Condition>(), paths);

        var result = 0;
        foreach (var part in parts)
        {
            
            // If any path evals to true,  we have a solution
            // A path is true if all conditions are true
            foreach (var conditions in paths)
            {
                var ok = true;
                foreach (var condition in conditions)
                {
                    var toCheck = condition.Variable switch
                    {
                        'x' => part.X,
                        'm' => part.M,
                        'a' => part.A,
                        _ => part.S
                    };

                    var match = condition.Op switch
                    {
                        '>' => toCheck > condition.Value ,
                        _ => toCheck < condition.Value ,
                    };

                    if (!match)
                    {
                        ok = false;
                        break;
                    }
                }

                if (ok)
                {
                    result += part.X + part.A + part.S + part.M;
                    break;
                }
            }
        }
        
        return result;
    }

    public long Part2(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var workflows = new Dictionary<string,Workflow>();
        var parts = new List<Part>();
        var loadWorkflows = true;
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                loadWorkflows = false;
            else if (loadWorkflows)
            {
                var workflow = LoadWorkflow(line);
                workflows.Add(workflow.Name, workflow);
            }
            else
                parts.Add(LoadPart(line));
        }

        // Combine the workflows
        var paths = new List<List<Condition>>();
        Combine(workflows, "in", new Stack<Condition>(), paths);
        
        // Flatten rules into  minS < S < maxS ....
        var result = 0L;
        foreach (var path in paths)
        {
            var minX = 0;
            var maxX = 4001;
            var minM = 0;
            var maxM = 4001;
            var minA = 0;
            var maxA = 4001;
            var minS = 0;
            var maxS = 4001;

            foreach (var condition in path)
            {
                switch (condition.Variable)
                {
                    case 'x':
                        if (condition.Op == '<')
                            maxX = Math.Min(maxX, condition.Value);
                        else
                            minX = Math.Max(minX, condition.Value);
                        break;
                    
                    case 'm':
                        if (condition.Op == '<')
                            maxM = Math.Min(maxM, condition.Value);
                        else
                            minM = Math.Max(minM, condition.Value);
                        break;
                    
                    case 'a':
                        if (condition.Op == '<')
                            maxA = Math.Min(maxA, condition.Value);
                        else
                            minA = Math.Max(minA, condition.Value);
                        break;
                    
                    case 's':
                        if (condition.Op == '<')
                            maxS = Math.Min(maxS, condition.Value);
                        else
                            minS = Math.Max(minS, condition.Value);
                        break;
                    
                }
            }

            result += ((long)(maxX - minX - 1) * (long)(maxM - minM - 1) * (long)(maxA - minA - 1) * (long)(maxS - minS - 1));

        }

        return result;
    }
    
    private void Combine(Dictionary<string, Workflow> workflows, 
                         string label, 
                         Stack<Condition> conditionsToGetToHere,
                         List<List<Condition>> paths)
    {
        var workflow = workflows[label];
    
        foreach (var rule in workflow.Rules)
        {
            conditionsToGetToHere.Push(rule.Condition);
            if (rule.Action == "A")
            {
                paths.Add(conditionsToGetToHere.ToList());
            }
            else if (rule.Action == "R")
            {
                // Not a solution
            }
            else
            {
                // Need to go deeper
                Combine(workflows, rule.Action, conditionsToGetToHere, paths);
            }
            conditionsToGetToHere.Pop();
            conditionsToGetToHere.Push(ReverseCondition(rule.Condition));
        }

        //Unwind
        for (var i = 0; i < workflow.Rules.Count; i++)
            conditionsToGetToHere.Pop();
    }
}