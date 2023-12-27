namespace AOC_2023.Solutions;

public class Day25Original
{


    
    public long Part1(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var links = new Dictionary<string, List<string>>();   // TODO: Could replace jqt etc with numbers 1...
        
        foreach (var line in lines)
        {
            var sides = line.Split(": ");
            var lhs = sides[0];
            var rhss = sides[1].Split(' ');

            if (!links.ContainsKey(lhs))
            {
                links.Add(lhs, new List<string>());
            }
                
            foreach (var rhs in rhss)
            {
                if (!links.ContainsKey(rhs))
                    links.Add(rhs, new List<string>());
                
                links[lhs].Add(rhs);
                links[rhs].Add(lhs);
            }
        }
        
        var count = 0;
        foreach (var link1 in links)
        {
            count++;
            Console.WriteLine($"{count} of {links.Count}");
            
            var lhs = link1.Key;
            foreach (var rhs in link1.Value)
            {
                if (string.Compare(lhs, rhs, StringComparison.Ordinal) > 0)
                {
                    var path = FindPath(links, lhs, rhs, new[] { (lhs, rhs) });
                    foreach (var step in path!)
                    {
                        var path2 = FindPath(links, lhs, rhs, new[] { (lhs, rhs), step });
                        foreach (var step2 in path2!)
                        {
                            var path3 = FindPath(links, lhs, rhs, new[] { (lhs, rhs), step, step2 });
                            if (path3 is null)
                            {
                                // Possible solution found.
                                Console.WriteLine($"{(lhs, rhs)}");
                                Console.WriteLine($"{(step)}");
                                Console.WriteLine($"{(step2)}");
        
                                var cluster1 = CountConnectedNodes(links.Keys.First(), links, new[] { (lhs, rhs), step, step2 });
                                var cluster2 = links.Count - cluster1;
                                return cluster1 * cluster2;
                                
                                // (ldl, fpg)
                                // (hcf, lhn)
                                // (nxk, dfk)
                                // var cluster1 = CountConnectedNodes(links.Keys.First(), links, new[] { ("ldl", "fpg"), ("hcf", "lhn"), ("nxk", "dfk") });
                                // var cluster2 = links.Count - cluster1;
                                // return cluster1 * cluster2;
                                
                            }
                        }
                    }
                }
            }
        }

        throw new Exception("No solution");
    }

    private int CountConnectedNodes(string start,
                                    Dictionary<string, List<string>> links,
                                    (string lhs, string rhs)[] avoiding)
    {
        var queue = new Queue<string>();
        queue.Enqueue(start);
        var seen = new HashSet<string>();

        while (queue.Any())
        {
            var v = queue.Dequeue();
            if (!seen.Contains(v))
            {
                foreach (var u in links[v])
                {
                    if (!avoiding.Contains((u, v)) && !avoiding.Contains((v, u)))
                    {
                        queue.Enqueue(u);
                    }
                }

                seen.Add(v);
            }
        }

        return seen.Count;
    }

    private List<(string from, string to)>? FindPath(
        Dictionary<string, List<string>> links, 
        string lhs,
        string rhs, 
        (string lhs, string rhs)[] avoiding)
    {
        // Find the shortest path from lhs to rhs, not using any links listed in avoiding.  If
        // no path exists (which is our aim!) then we return null.
        
        // Compute shortest paths from S
        var distances = new Dictionary<string,int>();
        var parents = new Dictionary<string,string>();
        var seen = new Dictionary<string,bool>();
        distances[lhs] = 0;

        for (var n = 0; n < links.Count; n++)
        {
            var v = FindSmallest(seen, distances);
            seen[v] = true;

            // Where can we go from links[v]
            if (distances.GetValueOrDefault(v, int.MaxValue) != int.MaxValue)
            {
                foreach (var u in links[v])
                {
                    if (!avoiding.Contains((u, v)) && !avoiding.Contains((v, u)))
                    {
                        if ((distances[v] + 1) < distances.GetValueOrDefault(u, int.MaxValue))
                        {
                            parents[u] = v; 
                            distances[u] = distances[v] + 1;
                        }
                    }
                }
            }

            if (v == rhs) break;
        }

        var path = new List<(string, string)>();
        var to = rhs;
        while (to != lhs)
        {
            if (!parents.ContainsKey(to)) return null;
            var from = parents[to];
            path.Insert(0, (from,to));
            to = from;
        }
        
        return path;
    }
    
    private string FindSmallest(Dictionary<string,bool> seen, Dictionary<string,int> distances)
    {
        var smallestCost = int.MaxValue;
        var smallestIndex = "";

        foreach (var key in distances.Keys)
        {
            if (distances[key] <= smallestCost && !seen.GetValueOrDefault(key))
            {
                smallestCost = distances[key];
                smallestIndex = key;
            }
        }
        return smallestIndex;
    }

}