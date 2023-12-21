using AoCHelper;

namespace AdventOfCode;

public class Day21 : BaseDay
{
    private (int row, int col) _start;
    private readonly char[][] _input;
    private readonly int rowLength;
    private readonly int colLength;

    public Day21()
    {
        _input = File.ReadAllText(InputFilePath).Split(new[] { Environment.NewLine }, StringSplitOptions.None).Select(s => s.ToCharArray()).ToArray();
        for (int i = 0; i < _input.Length; i++)
        {
            for (int j = 0; j < _input[i].Length; j++)
            {
                if (_input[i][j] == 'S')
                    _start = (i, j);
            }
        }
        rowLength = _input.Length;
        colLength = _input[0].Length;
    }

    public override ValueTask<string> Solve_1()
    {
        var iterations = 64;
        List<HashSet<(int row, int col)>> queue = [];
        queue.Add([]);
        queue[0].Add(_start);
        for (int i = 0; i < iterations; i++)
        {
            queue.Add([]);
            var currentSet = queue[i].ToArray();
            for (int j = 0; j < currentSet.Length; j++)
            {
                var (row, col) = currentSet[j];
                ExploreNeighbor(_input, queue[i + 1], row - 1, col); // Up
                ExploreNeighbor(_input, queue[i + 1], row + 1, col); // Down
                ExploreNeighbor(_input, queue[i + 1], row, col - 1); // Left
                ExploreNeighbor(_input, queue[i + 1], row, col + 1); // Right
            }
        }
        //PrintGarden(_input, queue[64]);
        return new ValueTask<string>(queue[iterations].Distinct().Count().ToString());
    }

    static void ExploreNeighbor(char[][] array, HashSet<(int, int)> queue, int row, int col)
    {
        int rows = array.Length;
        int cols = array[0].Length;
        if (row >= 0 && row < rows && col >= 0 && col < cols)
        {
            if (array[row][col] == '#')
                return;
            queue.Add((row, col));
        }
    }

    public override ValueTask<string> Solve_2()
    {
        return new ValueTask<string>();
    }

    static void PrintGarden(char[][] arr, HashSet<(int, int)> que)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            for (int j = 0; j < arr[0].Length; j++)
            {
                if (que.Contains((i,j))) 
                {
                    Console.Write('O');
                    continue;
                }
                Console.Write(arr[i][j]);
            }
            Console.WriteLine();
        }
    }

}
