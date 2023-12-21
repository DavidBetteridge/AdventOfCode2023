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

    private record Rule
    {
        public string Condition { get; set; }
        public string Action { get; set; }
    }

    private record Workflow
    {
        public string Name { get; set; }
        public List<Rule> Rules { get; set; }
        public string DefaultAction { get; set; }
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
            DefaultAction = rs[^1],
            Rules = rs[..^1].Select(r => new Rule { Action = r.Split(':')[1], Condition = r.Split(':')[0] }).ToList()
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

        var result = 0;
        foreach (var part in parts)
        {
            var toEval = "in";
            do
            {
                toEval = EvaluateWorkflow(workflows[toEval], part);    
            } while (toEval != "A" && toEval != "R");

            if (toEval == "A")
            {
                result += part.X + part.A + part.S + part.M;
            }
        }
        
        return result;
    }

    private string EvaluateWorkflow(Workflow workflow, Part part)
    {
        foreach (var rule in workflow.Rules)
        {
            // z > 11
            var variable = rule.Condition[0];
            var op = rule.Condition[1];
            var value = int.Parse(rule.Condition[2..]);

            var toCheck = variable switch
            {
                'x' => part.X,
                'm' => part.M,
                'a' => part.A,
                _ => part.S
            };

            var result = op switch
            {
                '>' => toCheck > value ,
                _ => toCheck < value
            };

            if (result) return rule.Action;

        }

        return workflow.DefaultAction;
    }


    public long Part2(string filename)
    {
        return 0;
    }
}