using AoCHelper;

namespace AdventOfCode;

public class Day09 : BaseDay
{
    private readonly string[] _input;

    public Day09()
    {
        string inputString = File.ReadAllText(InputFilePath);
        var _input = inputString.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
    }

    public override ValueTask<string> Solve_1()
    {
        return new ValueTask<string>();
    }

    public override ValueTask<string> Solve_2()
    {
        return new ValueTask<string>();
    }
}
