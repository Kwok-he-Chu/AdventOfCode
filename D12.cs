using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2022
{
    public class D12
    {
        private readonly AocHttpClient _client = new AocHttpClient(12);
        private readonly List<(int X, int Y)> directions = new List<(int X, int Y)>() { (1, 0), (0, 1), (-1, 0), (0, -1) };

        private (int X, int Y) StartNodePosition;
        private (int X, int Y) EndNodePosition;

        // For visualization purposes in <see cref="GetSteps(HeightModel)"/>.
        private bool[,] visibilityArray;

        public void Execute1()
        {
            string input = _client.RetrieveFile().GetAwaiter().GetResult();
            //input = "Sabqponm\r\nabcryxxl\r\naccszExk\r\nacctuvwj\r\nabdefghi";
            HeightModel[,] heightModels = GetHeightModels(input.ConvertToCharArray());
            visibilityArray = new bool[heightModels.GetLength(0), heightModels.GetLength(1)]; /// For visualization purposes in <see cref="GetSteps(HeightModel)"/>.

            HeightModel startNode = heightModels[StartNodePosition.X, StartNodePosition.Y];
            HeightModel endNode = heightModels[EndNodePosition.X, EndNodePosition.Y];
            HeightModel result = BFS(heightModels, startNode, endNode);
            int sum = GetSteps(result);
            Console.WriteLine(sum);
        }

        private int GetSteps(HeightModel current)
        {
            int sum = 0;
            while (current.Parent != null)
            {
#if DEBUG
                visibilityArray[current.X, current.Y] = true;
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

                    if (heightModel.Elevation == 'S')
                    {
                        StartNodePosition = (heightModel.X, heightModel.Y);
                        heightModel.Elevation = 'a';
                    }

                    if (heightModel.Elevation == 'E')
                    {
                        EndNodePosition = (heightModel.X, heightModel.Y);
                        heightModel.Elevation = 'z';
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
                HeightModel current = queue.Dequeue();

                if (current.IsSeen)
                    continue;

                current.IsSeen = true;

                foreach (var direction in directions)
                {
                    if (!heightModels.IsWithinBounds(current.X + direction.X, current.Y + direction.Y))
                        continue;

                    HeightModel neighbour = heightModels[current.X + direction.X, current.Y + direction.Y];

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

        public void Execute2()
        {
            string input = _client.RetrieveFile().GetAwaiter().GetResult();
            //input = "Sabqponm\r\nabcryxxl\r\naccszExk\r\nacctuvwj\r\nabdefghi";
            HeightModel[,] heightModels = GetHeightModels(input.ConvertToCharArray());
            visibilityArray = new bool[heightModels.GetLength(0), heightModels.GetLength(1)]; /// For visualization purposes in <see cref="GetSteps(HeightModel)"/>.

            HeightModel endNode = heightModels[EndNodePosition.X, EndNodePosition.Y];

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
    }

    public class HeightModel
    {
        public int X { get; set; }

        public int Y { get; set; }

        public char Elevation { get; set; }

        public HeightModel Parent { get; set; }

        public bool IsSeen { get; set; }

        public override string ToString()
        {
            return $"{Elevation} ({X}, {Y})";
        }
    }
}