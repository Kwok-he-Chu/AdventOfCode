using System;

namespace AOC2025;

public class Vector
{
    public long X { get; }
    public long Y { get; }

    public Vector(long x, long y)
    {
        X = x;
        Y = y;
    }
    
    public Vector(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Vector((int X, int Y) tuple)
    {
        X = tuple.X;
        Y = tuple.Y;
    }
    
    public Vector((long X, long Y) tuple)
    {
        X = tuple.X;
        Y = tuple.Y;
    }
    
    public static Vector operator +(Vector a, Vector b)
    {
        return new Vector(a.X + b.X, a.Y + b.Y);
    }

    public static Vector operator -(Vector a, Vector b)
    {
        return new Vector(a.X - b.X, a.Y - b.Y);
    }

    public static bool operator ==(Vector a, Vector b)
    {
        if (ReferenceEquals(a, b)) return true;
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
        if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;
        return a.X == b.X && a.Y == b.Y;
    }

    public static bool operator !=(Vector a, Vector b)
    {
        return !(a == b);
    }

    public override bool Equals(object obj)
    {
        if (obj is Vector vector)
            return X == vector.X && Y == vector.Y;
        return false;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public double GetLength()
    {
        return Math.Sqrt(X * X + Y * Y);
    }
    
    public Vector Normalize()
    {
        double length = GetLength();
        return length == 0 ? Vector.Zero : new Vector((int)(X / length), (int)(Y / length));
    }

    public override string ToString()
    {
        return $"({X}, {Y})";
    }

    public static Vector Zero      => new Vector(0, 0);
    public static Vector North     => new Vector(0, -1);
    public static Vector NorthEast => new Vector(1, -1);
    public static Vector East      => new Vector(1, 0);
    public static Vector SouthEast => new Vector(1, 1);
    public static Vector South     => new Vector(0, 1);
    public static Vector SouthWest => new Vector(-1, 1);
    public static Vector West      => new Vector(-1, 0);
    public static Vector NorthWest => new Vector(-1, -1);
    
    public static List<Vector> Directions8 =
    [
        Vector.NorthWest, Vector.North, Vector.NorthEast,
        Vector.West,                     Vector.East,
        Vector.SouthWest, Vector.South, Vector.SouthEast
    ];
    
    public static List<Vector> Directions4 =
    [
                        Vector.North, 
        Vector.West,                     Vector.East,
                        Vector.South,
    ];

    public static bool IsVectorInsidePolygon(List<Vector> polygon, Vector point)
    {
        bool result = false;
        int j = polygon.Count - 1;
        for (int i = 0; i < polygon.Count; i++)
        {
            if ((polygon[i].Y > point.Y) != (polygon[j].Y > point.Y) &&
                (point.X < (polygon[j].X - polygon[i].X) * (point.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X))
            {
                result = !result;
            }
            j = i;
        }
        
        return result;
    }

    /// <summary>
    /// Returns a list of vectors from current <see cref="Vector"/> to given vector.
    /// Example:  (1, 0) -> (4, 0)
    /// Returns: [(1, 0), (2, 0), (3, 0), (4, 0)] 
    /// </summary>
    public List<Vector> GetPathTo(Vector to)
    {
        List<Vector> result = new List<Vector>() { this };

        long currentX = this.X;
        long currentY = this.Y;
        
        int stepX = Math.Sign(to.X - this.X);
        while (currentX != to.X)
        {
            result.Add(new Vector(currentX + stepX, currentY));
            currentX += stepX;
        }
        
        int stepY = Math.Sign(to.Y - this.Y);
        while (currentY != to.Y)
        {
            result.Add(new Vector(currentX, currentY + stepY));
            currentY += stepY;
        }
        
        return result;
    }
}