using AoCHelper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode;

public class Day19 : BaseDay
{
    private readonly string[] _input;
    private readonly List<Part> _parts = [];
    private readonly Dictionary<string, Workflow> _workflows = [];

    struct Part
    {
        public int x, m, a, s;
    }

    struct Workflow
    {
        public string defaultRule;
        public List<char> compareChar = [];
        public List<char> comparitor = [];
        public List<long> compareValue = [];
        public List<string> toWorkflow = [];

        public Workflow() {}
    }

    public Day19()
    {
        string inputString = File.ReadAllText(InputFilePath);
        _input = inputString.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

        foreach (var line in _input) 
        {
            if (line.StartsWith('{'))
            {
                var partData = line.Split(',');
                var part = new Part();
                foreach (var data in partData) 
                {
                    var idx = data.IndexOf('=');
                    if (data[idx - 1] == 'x')
                        part.x = Convert.ToInt32(data.Substring(idx + 1, data.Length - idx - 1));
                    if (data[idx - 1] == 'm')
                        part.m = Convert.ToInt32(data.Substring(idx + 1, data.Length - idx - 1));
                    if (data[idx - 1] == 'a')
                        part.a = Convert.ToInt32(data.Substring(idx + 1, data.Length - idx - 1));
                    if (data[idx - 1] == 's')
                        part.s = Convert.ToInt32(data.Substring(idx + 1, data.Length - idx - 2));
                }
                _parts.Add(part);
            }
            else
            {
                if (line == "")
                    continue;
                var lineArr = line.Split([',', '{', '}']);
                var workflow = new Workflow { defaultRule = lineArr[^2] };
                for (int i = 1; i < lineArr.Length - 2; i++)
                {
                    workflow.compareChar.Add(lineArr[i].ToCharArray()[0]);
                    workflow.comparitor.Add(lineArr[i].ToCharArray()[1]);
                    var split = lineArr[i].Split(':');
                    workflow.compareValue.Add(Convert.ToInt32(split[0].Substring(2, split[0].Length - 2)));
                    workflow.toWorkflow.Add(split[1]);
                }
                _workflows[lineArr[0]] = workflow;
            }
        }
    }

    public override ValueTask<string> Solve_1()
    {
        var approvedParts = new List<Part>();
        foreach (var part in _parts) 
        {
            var currentWorkflow = "in";
            while (true)
            {
                var workflow = _workflows[currentWorkflow];
                for (int i = 0; i < workflow.comparitor.Count; i++)
                {
                    if (CheckRule(workflow.compareChar[i], workflow.comparitor[i], workflow.compareValue[i], part))
                    {
                        currentWorkflow = workflow.toWorkflow[i];
                        break;
                    }
                    if (i == workflow.comparitor.Count - 1)
                        currentWorkflow = workflow.defaultRule;
                }
                if (currentWorkflow == "A") 
                { 
                    approvedParts.Add(part);
                    break;
                }
                if (currentWorkflow == "R")
                    break;
            }
        }
        return new ValueTask<string>(approvedParts.Select(i => i.a + i.m + i.s + i.x).Sum().ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new ValueTask<string>();
    }

    private static bool CheckRule(char compareChar, char comparitor, long value, Part part)
    {
        switch (comparitor)
        {
        case '>':
            if (compareChar == 'x') return part.x > value;
            if (compareChar == 'm') return part.m > value;
            if (compareChar == 'a') return part.a > value;
            if (compareChar == 's') return part.s > value;
            break;
        case '<':
            if (compareChar == 'x') return part.x < value;
            if (compareChar == 'm') return part.m < value;
            if (compareChar == 'a') return part.a < value;
            if (compareChar == 's') return part.s < value;
            break;
        default:
            throw new ArgumentException($"{comparitor} invalid char.");
        }
        return false;
    }
}