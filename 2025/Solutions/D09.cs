using System;

namespace AOC2025;

/// <summary>
/// Day 9: Movie Theater
/// </summary>
public class D09
{
    private readonly AOCHttpClient _client = new AOCHttpClient(9);

    public D09()
    {
    }

    public void Part1()
    {
        string input = _client.RetrieveFile();

//         input = @"7,1
// 11,1
// 11,7
// 9,7
// 9,5
// 2,5
// 2,3
// 7,3
// ";

        string[] split = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        List<Vector> list = split
            .Select(x =>
            {
                string[] position = x.Split(",", StringSplitOptions.RemoveEmptyEntries);
                return new Vector(int.Parse(position[0]), int.Parse(position[1]));
            })
            .ToList();

        
        IDictionary<(Vector, Vector), Rectangle> rectangles = new Dictionary<(Vector, Vector), Rectangle>();
        for (int i = 0; i < list.Count; i++)
        {
            for (int j = i + 1; j < list.Count; j++)
            {
                Vector a = list[i];
                Vector b = list[j];

                if (a != b)
                {
                    Rectangle rectangle = new Rectangle(a.X, a.Y, b.X, b.Y);
                    rectangles.Add((a, b), rectangle);
                }
            }
        }
        
        Rectangle result = rectangles.MaxBy(x => x.Value.Size).Value;
        Console.WriteLine(result.Size);
    }
    
    public class Rectangle
    {
        public long Left { get; private set; }
        public long Top { get; private set; }
        public long Right { get; private set; }
        public long Bottom { get; private set; }
        
        public long Width { get; private set; }
        public long Height { get; private set; }

        public long Size { get; private set; }
        
        public Rectangle(long left, long top, long right, long bottom)
        {
            this.Left = Math.Min(left, right);
            this.Right = Math.Max(left, right);
            this.Width = Math.Abs(Right - Left) + 1;
            
            this.Top = Math.Min(top, bottom );
            this.Bottom = Math.Max(top, bottom);
            this.Height = Math.Abs(Bottom - Top) + 1;
            
            this.Size = Width * Height;
        }
    }

    public void Part2()
    {
        string input = _client.RetrieveFile();

//         input = @"7,1
// 11,1
// 11,7
// 9,7
// 9,5
// 2,5
// 2,3
// 7,3
// ";

        string[] split = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
    }
}