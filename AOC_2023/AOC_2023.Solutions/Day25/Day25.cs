using Microsoft.Diagnostics.Tracing.Parsers.AspNet;

namespace AOC_2023.Solutions;

public class Day25
{


    
    public long Part1(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var linksAsText = new Dictionary<string, List<string>>();   // TODO: Could replace jqt etc with numbers 1...
        
        foreach (var line in lines)
        {
            var sides = line.Split(": ");
            var lhs = sides[0];
            var rhss = sides[1].Split(' ');

            if (!linksAsText.ContainsKey(lhs))
            {
                linksAsText.Add(lhs, new List<string>());
            }
                
            foreach (var rhs in rhss)
            {
                if (!linksAsText.ContainsKey(rhs))
                    linksAsText.Add(rhs, new List<string>());
                
                linksAsText[lhs].Add(rhs);
                linksAsText[rhs].Add(lhs);
            }
        }

        var mapping = linksAsText.Keys.Select((k, i) => (k, i)).ToDictionary(k => k.k, v => v.i);
        var links = new List<int>[mapping.Count];
        foreach (var lnk in linksAsText)
            links[mapping[lnk.Key]] = lnk.Value.Select(i => mapping[i]).ToList();

        
        var distances = new int[links.Length];
        var parents = new Dictionary<int,int>();
        var seen = new bool[links.Length];
        
        for (var lhs = 0; lhs < links.Length; lhs++)
        {
            Console.WriteLine($"{lhs} of {links.Length}");
            
            foreach (var rhs in links[lhs])
            {
                if (rhs > lhs)
                {
                    var path = FindPath(links, lhs, rhs, new[] { (lhs, rhs) }, distances,parents,seen);
                    foreach (var step in path!)
                    {
                        var path2 = FindPath(links, lhs, rhs, new[] { (lhs, rhs), step }, distances,parents,seen);
                        foreach (var step2 in path2!)
                        {
                            var path3 = FindPath(links, lhs, rhs, new[] { (lhs, rhs), step, step2 }, distances,parents,seen);
                            if (path3 is null)
                            {
                                // Possible solution found.
                                Console.WriteLine($"{(lhs, rhs)}");
                                Console.WriteLine($"{(step)}");
                                Console.WriteLine($"{(step2)}");
        
                                var cluster1 = CountConnectedNodes(0, links, new[] { (lhs, rhs), step, step2 });
                                var cluster2 = linksAsText.Count - cluster1;
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

    private int CountConnectedNodes(int start,
                                    List<int>[] links,
                                    (int lhs, int rhs)[] avoiding)
    {
        var queue = new Queue<int>();
        queue.Enqueue(start);
        var seen = new HashSet<int>();

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

    private List<(int from, int to)>? FindPath(
        List<int>[] links, 
        int lhs,
        int rhs, 
        (int lhs, int rhs)[] avoiding,
        int[] distances,
        Dictionary<int,int> parents,
        bool[] seen)
    {
        // Find the shortest path from lhs to rhs, not using any links listed in avoiding.  If
        // no path exists (which is our aim!) then we return null.
        
        // Compute shortest paths from S
        Array.Fill(distances, int.MaxValue);
        distances[lhs] = 0;
        parents.Clear();
        Array.Fill(seen, false);

        for (var n = 0; n < links.Length; n++)
        {
            var v = FindSmallest(seen, distances);
            seen[v] = true;

            // Where can we go from links[v]
            if (distances[v] != int.MaxValue)
            {
                foreach (var u in links[v])
                {
                    if (!avoiding.Contains((u, v)) && !avoiding.Contains((v, u)))
                    {
                        if ((distances[v] + 1) < distances[u])
                        {
                            parents[u] = v; 
                            distances[u] = distances[v] + 1;
                        }
                    }
                }
            }

            if (v == rhs) break;
        }

        var path = new List<(int, int)>();
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
    
    private int FindSmallest(bool[] seen, int[] distances)
    {
        var smallestCost = int.MaxValue;
        var smallestIndex = -1;

        for (var i = 0; i < distances.Length; i++)
        {
            if (distances[i] <= smallestCost && !seen[i])
            {
                smallestCost = distances[i];
                smallestIndex = i;
            }
        }

        return smallestIndex;
    }

}