using System;
using System.Collections.Generic;

namespace AOC2022
{
    /// <summary>
    /// Day 14: Regolith Reservoir
    /// </summary>
    public class D14
    {
        private readonly AocHttpClient _client = new AocHttpClient(14);

        private readonly Vector2 Left = new Vector2(-1, 0);
        private readonly Vector2 Right = new Vector2(1, 0);
        private readonly Vector2 Down = new Vector2(0, 1);

        public void Execute1()
        {
            string input = _client.RetrieveFile();
            //input = "498,4 -> 498,6 -> 496,6\r\n503,4 -> 502,4 -> 502,9 -> 494,9";
            string[] split = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            HashSet<Vector2> hashSet = InitializeHashSet(split);
            int initialSize = hashSet.Count;

            var size = GetWidthAndHeight(hashSet);
            for (int i = 0; i < 1000; i++)
                TryPlace(new Vector2('S', 500, 0), hashSet, size);

            int grains = hashSet.Count - initialSize;
            Console.WriteLine(grains);
        }

        private void TryPlace(Vector2 position, HashSet<Vector2> hashSet, (int MinX, int MaxX, int MinY, int MaxY) size)
        {
            Vector2 nextPosition = position;

            while (true)
            {
                if (nextPosition.Y > size.MaxY || nextPosition.X <= size.MinX || nextPosition.X >= size.MaxX)
                    return;

                if (hashSet.Contains(nextPosition))
                {
                    Vector2 leftDown = nextPosition + Left;
                    Vector2 rightDown = nextPosition + Right;
                    if (!hashSet.Contains(leftDown))
                    {
                        nextPosition = leftDown;
                    }
                    else if (!hashSet.Contains(rightDown))
                    {
                        nextPosition = rightDown;
                    }
                    else
                    {
                        hashSet.Add(new Vector2('O', nextPosition.X, nextPosition.Y - 1));
                        return;
                    }
                }

                nextPosition += Down;
            }
        }

        private HashSet<Vector2> InitializeHashSet(string[] split)
        {
            HashSet<Vector2> hashSet = new HashSet<Vector2>();
            foreach (string line in split)
            {
                string[] coordinate = line.Split("->");
                for (int i = 0; i < coordinate.Length - 1; i += 1)
                {
                    string[] c1 = coordinate[i].Split(',');
                    int x1 = int.Parse(c1[0]);
                    int y1 = int.Parse(c1[1]);

                    string[] c2 = coordinate[i + 1].Split(',');
                    int x2 = int.Parse(c2[0]);
                    int y2 = int.Parse(c2[1]);

                    List<Vector2> points = new Vector2('#', x1, y1).GetAllPointsToVector(x2, y2);
                    foreach (Vector2 p in points)
                        hashSet.Add(p);
                }
            }
            return hashSet;
        }

        private (int MinX, int MaxX, int MinY, int MaxY) GetWidthAndHeight(HashSet<Vector2> hashSet)
        {
            int minX = int.MaxValue, maxX = 0;
            int minY = int.MaxValue, maxY = 0;

            foreach (Vector2 item in hashSet)
            {
                if (item.X < minX)
                    minX = item.X;

                if (item.X > maxX)
                    maxX = item.X;

                if (item.Y < minY)
                    minY = item.Y;

                if (item.Y > maxY)
                    maxY = item.Y;
            }
            return (minX, maxX, minY, maxY);
        }

        public void Execute2()
        {
            string input = _client.RetrieveFile();
            //input = "498,4 -> 498,6 -> 496,6\r\n503,4 -> 502,4 -> 502,9 -> 494,9";
            string[] split = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            HashSet<Vector2> hashSet = InitializeHashSet(split);
            var size = GetWidthAndHeight(hashSet);

            bool isDone = false;
            int grains = 0;
            while (!isDone)
            {
                isDone = TryPlace(new Vector2('S', 500, 0), hashSet, size.MaxY + 2);
                grains++;
            }

            Console.WriteLine(grains);
        }

        private bool TryPlace(Vector2 position, HashSet<Vector2> hashSet, int floorY)
        {
            Vector2 nextPosition = position;

            while (true)
            {
                if (nextPosition.Y == floorY)
                {
                    hashSet.Add(new Vector2('O', nextPosition.X, nextPosition.Y - 1));
                    return false;
                }

                if (!hashSet.Contains(nextPosition))
                {
                    nextPosition += Down;
                    continue;
                }

                Vector2 leftDown = nextPosition + Left;
                Vector2 rightDown = nextPosition + Right;
                if (!hashSet.Contains(leftDown))
                {
                    nextPosition = leftDown;
                    continue;
                }

                if (!hashSet.Contains(rightDown))
                {
                    nextPosition = rightDown;
                    continue;
                }

                hashSet.Add(new Vector2('O', nextPosition.X, nextPosition.Y - 1));

                if (nextPosition == new Vector2(500, 1))
                {
                    return true;
                }

                return false;
            }
        }

    }

    public class Vector2
    {
        public int X { get; set; }

        public int Y { get; set; }

        public char Type { get; set; } = '#';

        public Vector2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Vector2(char type, int x, int y)
        {
            Type = type;
            X = x;
            Y = y;
        }


        public static Vector2 operator +(Vector2 vectorA, Vector2 vectorB)
        {
            return new Vector2(vectorA.Type, vectorA.X + vectorB.X, vectorA.Y + vectorB.Y);
        }

        public static Vector2 operator -(Vector2 vectorA, Vector2 vectorB)
        {
            if (vectorA.Type != vectorB.Type)
                throw new ArgumentException();
            return new Vector2(vectorA.Type, vectorA.X - vectorB.X, vectorA.Y - vectorB.Y);
        }

        public static bool operator ==(Vector2 vectorA, Vector2 vectorB)
        {
            return Equals(vectorA, vectorB);
        }

        public static bool operator !=(Vector2 vector, Vector2 vectorB)
        {
            return !Equals(vector, vectorB);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (ReferenceEquals(obj, this))
                return false;

            if (obj.GetType() != GetType())
                return false;
            Vector2 vector = obj as Vector2;
            return X == vector.X && Y == vector.Y;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        public override string ToString()
        {
            return $"{X} {Y} {Type}";
        }

        public List<Vector2> GetAllPointsToVector(int x2, int y2)
        {
            List<Vector2> result = new List<Vector2>() { this };
            (int X, int Y) normalizedVector = GetNormalizedVector(x2, y2);

            int x = X, y = Y;
            while (x != x2 || y != y2)
            {
                x += normalizedVector.X;
                y += normalizedVector.Y;
                result.Add(new Vector2(x, y));
            }

            return result;
        }

        private (int X, int Y) GetNormalizedVector(int x2, int y2)
        {
            (int X, int Y) length = GetLength(x2, y2);
            if (length.X == 0 && length.Y == 0)
                return (0, 0);

            if (length.X == 0)
                return ( 0, (y2 - Y) / length.Y);

            if (length.Y == 0)
                return ((x2 - X) / length.X, 0);

            return ((x2 - X) / length.X, (y2 - Y) / length.Y);
        }

        private (int X, int Y) GetLength(int x2, int y2)
        {
            return (Math.Abs(x2 - X), Math.Abs(y2 - Y));
        }
    }
}