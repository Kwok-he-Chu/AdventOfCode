using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2022
{
    public class D12
    {
        private readonly AocHttpClient _client = new AocHttpClient(12);

        private readonly List<(int X, int Y)> directions = new List<(int X, int Y)>() { (1, 0), (0, 1), (-1, 0), (0, -1) };
        private bool[,] boolArray;

        public void Execute1()
        {
            string input = _client.RetrieveFile().GetAwaiter().GetResult();
            //input = "Sabqponm\r\nabcryxxl\r\naccszExk\r\nacctuvwj\r\nabdefghi";
            HeightModel[,] heightModels = GetHeightModels(input.ConvertToCharArray());
            boolArray = new bool[heightModels.GetLength(0), heightModels.GetLength(1)]; // For visualisation purposes.

            HeightModel result = BFS(heightModels, FindStartNode(heightModels), FindEndNode(heightModels));
            int sum = GetSteps(result);
            Console.WriteLine(sum);
        }


        public void Execute2()
        {
            string input = _client.RetrieveFile().GetAwaiter().GetResult();
            //input = "Sabqponm\r\nabcryxxl\r\naccszExk\r\nacctuvwj\r\nabdefghi";
            HeightModel[,] heightModels = GetHeightModels(input.ConvertToCharArray());
            boolArray = new bool[heightModels.GetLength(0), heightModels.GetLength(1)]; // For visualisation purposes.

            var endNode = FindEndNode(heightModels);

            List<HeightModel> startPositions = FindAllStartingNodes(heightModels);
            List<int> list = new List<int>();
            foreach (var pos in startPositions)
            {
                HeightModel result = BFS(heightModels, pos, endNode);
                if (result == null)
                    continue;

                int sum = GetSteps(result);
                list.Add(sum);
                Clear(heightModels);
            }
            Console.WriteLine(list.Min());
        }


        private int GetSteps(HeightModel current)
        {
            int sum = 0;
            while (current.Parent != null)
            {
#if DEBUG
                boolArray[current.X, current.Y] = true;
                //boolArray.PrintArray();
#endif 
                sum++;
                current = current.Parent;
            }
            return sum;
        }

        private void Clear(HeightModel[,] array)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                for (int i = 0; i < array.GetLength(0); i++)
                {
                    array[i, j].IsSeen = false;
                    array[i, j].Parent = null;
                }
            }
        }

        private HeightModel[,] GetHeightModels(char[,] array)
        {
            var result = new HeightModel[array.GetLength(0), array.GetLength(1)];
            for (int j = 0; j < array.GetLength(1); j++)
            {
                for (int i = 0; i < array.GetLength(0); i++)
                {
                    var heightModel = new HeightModel()
                    {
                        Elevation = array[i, j],
                        X = i,
                        Y = j
                    };
                    result[i, j] = heightModel;
                }
            }

            for (int j = 0; j < result.GetLength(1); j++)
            {
                for (int i = 0; i < result.GetLength(0); i++)
                {
                    foreach (var dir in directions)
                    {
                        if (array.IsWithinBounds(i + dir.X, j + dir.Y))
                        {
                            result[i, j].Neighbours.Add(result[i + dir.X, j + dir.Y]);
                        }
                    }
                }
            }
            return result;
        }

        private HeightModel FindEndNode(HeightModel[,] array)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                for (int i = 0; i < array.GetLength(0); i++)
                {
                    if (array[i, j].Elevation == 'E')
                    {
                        array[i, j].Elevation = 'z';
                        return array[i, j];
                    }
                }
            }
            throw new KeyNotFoundException();
        }


        private HeightModel FindStartNode(HeightModel[,] array)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                for (int i = 0; i < array.GetLength(0); i++)
                {
                    if (array[i, j].Elevation == 'S')
                    {
                        array[i, j].Elevation = 'a';
                        return array[i, j];
                    }
                }
            }
            throw new KeyNotFoundException();
        }

        private List<HeightModel> FindAllStartingNodes(HeightModel[,] array)
        {
            var result = new List<HeightModel>();
            for (int j = 0; j < array.GetLength(1); j++)
            {
                for (int i = 0; i < array.GetLength(0); i++)
                {
                    if (array[i, j].Elevation == 'a')
                    {
                        result.Add(array[i, j]);
                    }
                }
            }
            return result;
        }

        private HeightModel BFS(HeightModel[,] heightModels, HeightModel start, HeightModel end)
        {
            Queue<HeightModel> queue = new Queue<HeightModel>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (current.IsSeen)
                    continue;

                current.IsSeen = true;

                foreach (var neighbour in current.Neighbours)
                {
                    if (neighbour.IsSeen)
                        continue;

                    if (current.Elevation >= neighbour.Elevation - 1)
                    {
                        neighbour.Parent = current;

                        if (neighbour == end)
                        {
                            return end;
                        }
                        queue.Enqueue(neighbour);
                    }
                }
            }

            return null;
        }
    }

    public class HeightModel
    {
        public int X { get; set; }

        public int Y { get; set; }

        public char Elevation { get; set; }

        public List<HeightModel> Neighbours { get; set; } = new List<HeightModel>();

        public HeightModel Parent { get; set; }

        public bool IsSeen { get; set; }

        public override string ToString()
        {
            return $"{Elevation} ({X}, {Y})";
        }
    }
}