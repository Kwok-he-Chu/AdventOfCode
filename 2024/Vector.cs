using System;

namespace AOC2024;

public class Vector
{
    public int X { get; }
    public int Y { get; }

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
        if (obj is Vector other)
        {
            return this == other;
        }
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

    public static Vector Zero => new Vector(0, 0);
    
    public static Vector North => new Vector(0, -1);
    public static Vector East => new Vector(1, 0);
    public static Vector South => new Vector(0, 1);
    public static Vector West => new Vector(-1, 0);
}