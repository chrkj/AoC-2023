using AoCHelper;

namespace AdventOfCode;

public class Day14 : BaseDay
{
    private string[] _input1;
    private string[] _input2;

    public Day14()
    {
        string inputString = File.ReadAllText(InputFilePath);
        _input1 = inputString.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        _input2 = inputString.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
    }

    public override ValueTask<string> Solve_1()
    {
        TiltNorth(_input1);
        long sum = 0;
        int index = _input1.Length;

        for (int i = _input1.Length; i > 0; i--)
            sum += _input1[^i].Count(c => c == 'O') * i;
        return new ValueTask<string>(sum.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var cache = new Dictionary<string, int>();
        for (int cycle = 1; cycle <= 1_000_000_000; cycle++)
        {
            TiltNorth(_input2);
            TiltWest(_input2);
            TiltSouth(_input2);
            TiltEast(_input2);

            string currnetPermutation = string.Join(string.Empty, _input2.SelectMany(c => c));

            if (cache.TryGetValue(currnetPermutation, out var cached))
            {
                var remainingCycles = 1_000_000_000 - cycle;
                var loopSize = cycle - cached;

                var cyclesRemainingAfterLooping = remainingCycles % loopSize;
                cycle = 1_000_000_000 - cyclesRemainingAfterLooping;
            }

            cache[currnetPermutation] = cycle;
        }
        long sum = 0;
        for (int i = _input2.Length; i > 0; i--)
            sum += _input2[^i].Count(c => c == 'O') * i;
        return new ValueTask<string>(sum.ToString());
    }

    public static void TiltNorth(string[] arr)
    {
        for (int row = 0; row < arr.Length; row++)
        {
            for (int col = 0; col < arr[0].Length; col++)
            {
                if (arr[row][col] != 'O')
                    continue;
                int currentRow = row;
                while (true)
                {
                    if (!Utils.IsWithinBounds(arr, currentRow - 1, col) || arr[currentRow - 1][col] != '.')
                        break;
                    arr[currentRow] = Utils.ModifyCharacter(arr[currentRow], col, '.');
                    arr[currentRow - 1] = Utils.ModifyCharacter(arr[currentRow - 1], col, 'O');
                    currentRow--;
                }
            }
        }
    }

    public static void TiltSouth(string[] arr)
    {
        for (int row = arr.Length - 1; row >= 0; row--)
        {
            for (int col = 0; col < arr[0].Length; col++)
            {
                if (arr[row][col] != 'O')
                    continue;
                int currentRow = row;
                while (true)
                {
                    if (!Utils.IsWithinBounds(arr, currentRow + 1, col) || arr[currentRow + 1][col] != '.')
                        break;
                    arr[currentRow] = Utils.ModifyCharacter(arr[currentRow], col, '.');
                    arr[currentRow + 1] = Utils.ModifyCharacter(arr[currentRow + 1], col, 'O');
                    currentRow++;
                }
            }
        }
    }

    public static void TiltWest(string[] arr) 
    {
        for (int col = 0; col < arr[0].Length; col++)
        {
            for (int row = 0; row < arr.Length; row++)
            {
                if (arr[row][col] != 'O')
                    continue;
                int currentCol = col;
                while (true)
                {
                    if (!Utils.IsWithinBounds(arr, row, currentCol - 1) || arr[row][currentCol - 1] != '.')
                        break;
                    arr[row] = Utils.ModifyCharacter(arr[row], currentCol, '.');
                    arr[row] = Utils.ModifyCharacter(arr[row], currentCol - 1, 'O');
                    currentCol--;
                }
            }
        }
    }

    public static void TiltEast(string[] arr)
    {
        for (int col = arr[0].Length - 1; col >= 0; col--)
        {
            for (int row = arr.Length - 1; row >= 0; row--)
            {
                if (arr[row][col] != 'O')
                    continue;
                int currentCol = col;
                while (true)
                {
                    if (!Utils.IsWithinBounds(arr, row, currentCol + 1) || arr[row][currentCol + 1] != '.')
                        break;
                    arr[row] = Utils.ModifyCharacter(arr[row], currentCol, '.');
                    arr[row] = Utils.ModifyCharacter(arr[row], currentCol + 1, 'O');
                    currentCol++;
                }
            }
        }
    }
}
