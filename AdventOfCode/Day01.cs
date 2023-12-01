using AoCHelper;

namespace AdventOfCode;

public class Day01 : BaseDay
{
    private readonly string[] _input;
    private readonly string _numbers = "1234567890";
    private readonly Dictionary<string,string> _replacements = new() {
        {"one", "o1e"},
        {"two", "t2o"},
        {"three", "t3e"},
        {"four", "f4r"},
        {"five", "f5e"},
        {"six", "s6x"},
        {"seven", "s7n"},
        {"eight", "e8t"},
        {"nine", "n9e"}
    };

    public Day01()
    {
        string inputString = File.ReadAllText(InputFilePath);
        _input = inputString.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
    }

    public override ValueTask<string> Solve_1()
    {
        int sum = 0;
        foreach (string line in _input)
        {
            string number = "";
            number += line.FirstOrDefault(c => _numbers.Contains(c));
            number += line.LastOrDefault(c => _numbers.Contains(c));
            sum += Convert.ToInt32(number);
        }
        return new ValueTask<string>(sum.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        int sum = 0;
        foreach (string line in _input)
        {
            string fixedLine = line;
            foreach (KeyValuePair<string, string> replacement in _replacements)
                fixedLine = fixedLine.Replace(replacement.Key, replacement.Value);

            string number = "";
            number += fixedLine.FirstOrDefault(c => _numbers.Contains(c));
            number += fixedLine.LastOrDefault(c => _numbers.Contains(c));
            sum += Convert.ToInt32(number);
        }
        return new ValueTask<string>(sum.ToString());
    }
}
