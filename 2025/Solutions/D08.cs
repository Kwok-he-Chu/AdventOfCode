using System;
using System.Globalization;

namespace AOC2025;

/// <summary>
/// Day 8: Playground
/// </summary>
public class D08
{
    private readonly AOCHttpClient _client = new AOCHttpClient(8);

    public void Part1()
    {
        string input = _client.RetrieveFile();
        
//         input = @"162,817,812
// 57,618,57
// 906,360,560
// 592,479,940
// 352,342,300
// 466,668,158
// 542,29,236
// 431,825,988
// 739,650,466
// 52,470,668
// 216,146,977
// 819,987,18
// 117,168,530
// 805,96,715
// 346,949,466
// 970,615,88
// 941,993,340
// 862,61,35
// 984,92,344
// 425,690,689";

        string[] split = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        List<JunctionBox> list = split
            .Select(line => line.Split(",", StringSplitOptions.RemoveEmptyEntries))
            .Select(number => new JunctionBox(double.Parse(number[0]), double.Parse(number[1]), double.Parse(number[2])))
            .ToList();

        IDictionary<(JunctionBox, JunctionBox), double> distances = ComputeEuclidianDistances(list);
        List<KeyValuePair<(JunctionBox, JunctionBox), double>> sortedDistances = distances.OrderBy(kv => kv.Value).ToList();
        
        Dictionary<JunctionBox, Circuit> lookup = new Dictionary<JunctionBox, Circuit>();
        List<Circuit> circuits = new List<Circuit>();
        foreach (JunctionBox box in list)
        {
            Circuit circuit = new Circuit(box);
            circuits.Add(circuit);
            lookup[box] = circuit;
        }
        
        for (int i = 0; i < 1000; i++)
        {
            (JunctionBox A, JunctionBox B) key = sortedDistances[i].Key;
            Circuit circuitA = lookup[key.A];
            Circuit circuitB = lookup[key.B];
            
            if (circuitA == circuitB) 
                continue;
            
            circuitA.List.AddRange(circuitB.List);
            foreach (JunctionBox box in circuitB.List)
                lookup[box] = circuitA;
            
            circuits.Remove(circuitB);
        }
        
        long total = circuits
            .OrderByDescending(c => c.List.Count)
            .Take(3)    
            .Select(c => c.List.Count)
            .Aggregate(1L, (x, y) => x * y);
        
        Console.WriteLine(total);
    }
    
    private IDictionary<(JunctionBox, JunctionBox), double> ComputeEuclidianDistances(List<JunctionBox> list)
    {
        IDictionary<(JunctionBox, JunctionBox), double> result = new Dictionary<(JunctionBox, JunctionBox), double>();
        
        foreach (JunctionBox a in list)
        {
            foreach (JunctionBox b in list)
            {
                if (a == b) 
                    continue;
                
                double distance = a.GetEuclideanDistanceTo(b);
                (JunctionBox, JunctionBox) key = a.GetHashCode() < b.GetHashCode() ? (a, b) : (b, a);
                
                result.TryAdd(key, distance);
            }
        }
        return result;
    }
    
    private class Circuit
    {
        public List<JunctionBox> List { get; } = new List<JunctionBox>();
        
        public Circuit(JunctionBox box)
        {
            List.Add(box);
        }
        
        public override string ToString()
        {
            return string.Join(" ", List.Select(x => x.ToString()));
        }
    }
    
    private class JunctionBox
    {
        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        public JunctionBox(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        
        public double GetEuclideanDistanceTo(JunctionBox other)
        {
            double dx = other.X - this.X;
            double dy = other.Y - this.Y;
            double dz = other.Z - this.Z;
            
            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }
        
        public override int GetHashCode() => HashCode.Combine(X, Y, Z);
        
        public override bool Equals(object obj)
        {
            return obj is JunctionBox other && X == other.X && Y == other.Y && Z == other.Z;
        }

        public override string ToString()
        {
            return $"({X},{Y},{Z})";
        }
    }
    
    public void Part2()
    {
          string input = _client.RetrieveFile();
          
//         input = @"162,817,812
// 57,618,57
// 906,360,560
// 592,479,940
// 352,342,300
// 466,668,158
// 542,29,236
// 431,825,988
// 739,650,466
// 52,470,668
// 216,146,977
// 819,987,18
// 117,168,530
// 805,96,715
// 346,949,466
// 970,615,88
// 941,993,340
// 862,61,35
// 984,92,344
// 425,690,689";
        
        string[] split = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        List<JunctionBox> list = split
            .Select(line => line.Split(",", StringSplitOptions.RemoveEmptyEntries))
            .Select(number => new JunctionBox(double.Parse(number[0]), double.Parse(number[1]), double.Parse(number[2])))
            .ToList();
        
        IDictionary<(JunctionBox, JunctionBox), double> distances = ComputeEuclidianDistances(list);
        List<KeyValuePair<(JunctionBox, JunctionBox), double>> sortedDistances = distances.OrderBy(kv => kv.Value).ToList();
        
        Dictionary<JunctionBox, Circuit> lookup = new Dictionary<JunctionBox, Circuit>();
        
        List<Circuit> circuits = new List<Circuit>();
        foreach (JunctionBox box in list)
        {
            Circuit circuit = new Circuit(box);
            circuits.Add(circuit);
            lookup[box] = circuit;
        }

        (double aX, double bX)? result = null;
        
        for (int i = 0; true; i++)
        {
            (JunctionBox A, JunctionBox B) key = sortedDistances[i].Key;
            Circuit circuitA = lookup[key.A];
            Circuit circuitB = lookup[key.B];

            if (circuitA == circuitB)
                continue;

            if (circuits.Count == 2)
            {
                result = (key.A.X, key.B.X);
                break;
            }

            circuitA.List.AddRange(circuitB.List);
            foreach (JunctionBox box in circuitB.List)
                lookup[box] = circuitA;

            circuits.Remove(circuitB);
        }

        long total = (long)result.Value.aX * (long)result.Value.bX;
        Console.WriteLine(total);
    }
}