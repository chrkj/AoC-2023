using AoCHelper;

namespace AdventOfCode;

public class Day03 : BaseDay
{
    private readonly string[] _input;
    private readonly string _symbols = "#%&/=+-*@$";
    private readonly int[,] _directions = { 
        {-1, -1}, 
        { 0, -1}, 
        { 1, -1}, 
        {-1,  0}, 
        { 1,  0}, 
        {-1,  1}, 
        { 0,  1}, 
        { 1,  1} 
    };

    public Day03()
    {
        string inputString = File.ReadAllText(InputFilePath);
        _input = inputString.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
    }

    public override ValueTask<string> Solve_1()
    {
        int sum = 0;
        for (int i = 0; i < _input.Length; i++)
        {
            List<int> indices = Utils.FindIndicesOfSymbols(_input[i], _symbols);
            foreach (int index in indices)
            {
                List<int> partnumbersSeen = new List<int>();
                for (int j = 0; j < 8; j++)
                {
                    int xOffset = _directions[j, 0];
                    int yOffset = _directions[j, 1];

                    if (Utils.IsWithinBounds(_input, i + xOffset, index + yOffset))
                        continue;

                    if (char.IsDigit(_input[i + xOffset][index + yOffset]))
                    {
                        int partNumber = Utils.ExtractValueAroundIndex(_input[i + xOffset], index + yOffset);
                        if (!partnumbersSeen.Contains(partNumber))
                        {
                            partnumbersSeen.Add(partNumber);
                            sum += partNumber;
                        }
                    }
                }
            }
        }
        return new ValueTask<string>(sum.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        int sum = 0;
        for (int i = 0; i < _input.Length; i++)
        {
            List<int> indices = Utils.FindIndicesOfSymbols(_input[i], "*");
            foreach (int index in indices)
            {
                List<int> partnumbersSeen = new List<int>();
                for (int j = 0; j < 8; j++)
                {
                    int xOffset = _directions[j, 0];
                    int yOffset = _directions[j, 1];

                    if (Utils.IsWithinBounds(_input, i + xOffset, index + yOffset))
                        continue;

                    if (char.IsDigit(_input[i + xOffset][index + yOffset]))
                    {
                        int partNumber = Utils.ExtractValueAroundIndex(_input[i + xOffset], index + yOffset);
                        if (!partnumbersSeen.Contains(partNumber))
                            partnumbersSeen.Add(partNumber);
                    }
                }
                if (partnumbersSeen.Count == 2)
                    sum += partnumbersSeen[0] * partnumbersSeen[1];
            }
        }
        return new ValueTask<string>(sum.ToString());
    }

}
