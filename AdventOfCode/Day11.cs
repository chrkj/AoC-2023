using AoCHelper;

namespace AdventOfCode;

public class Day11 : BaseDay
{
    private readonly List<(int row, int col)> _galaxies = new();
   
    public Day11()
    {
        var inputArray = File.ReadAllText(InputFilePath).Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        InsertColumnIfEmptyDot(inputArray);
        List<string> list = new(inputArray);
        int length = inputArray.Length;
        for (int i = 0; i < length; i++)
        {
            var line = list[i];
            if (line.All(c => c == '.'))
            {
                list.Insert(i, list[i]);
                i++;
                length++;
            }

            var indices = line.Select((c, i) => c == '#' ? i : -1).Where(index => index != -1);
            foreach (var index in indices) 
                _galaxies.Add((i, index));
        }
    }

    public override ValueTask<string> Solve_1()
    {
        var sum = 0;
        for (int i = 0; i < _galaxies.Count - 1; i++)
        {
            (int row, int col) galaxyFrom = _galaxies[i];
            for (int j = i + 1; j < _galaxies.Count; j++)
            {
                (int row, int col) galaxyTo = _galaxies[j];
                var distance = Math.Abs(galaxyFrom.row - galaxyTo.row) + Math.Abs(galaxyFrom.col - galaxyTo.col);
                sum += distance;
            }
        }
        return new ValueTask<string>(sum.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new ValueTask<string>();
    }

    static bool IsColumnEmptyDot(string[] inputArray, int columnIndex)
    {
        foreach (string row in inputArray)
        {
            if (row.Length <= columnIndex || row[columnIndex] != '.')
            {
                return false;
            }
        }
        return true;
    }

    static void InsertColumnIfEmptyDot(string[] inputArray)
    {
        int numColumns = inputArray[0].Length;

        for (int columnIndex = 0; columnIndex < numColumns; columnIndex++)
        {
            if (IsColumnEmptyDot(inputArray, columnIndex))
            {
                for (int rowIndex = 0; rowIndex < inputArray.Length; rowIndex++)
                {
                    string currentRow = inputArray[rowIndex];
                    inputArray[rowIndex] = currentRow.Substring(0, columnIndex + 1) + '.' + currentRow.Substring(columnIndex + 1);
                }
                columnIndex++;
                numColumns++;
                if (columnIndex == numColumns)
                {
                    break;
                }
            }
        }
    }
}
