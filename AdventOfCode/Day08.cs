using AoCHelper;

namespace AdventOfCode;

public class Day08 : BaseDay
{
    private readonly string[] _input;
    private readonly string _instructioins;
    private readonly Graph _graph;
    private readonly List<string> _startNodes = new();
    private readonly List<string> _endNodes = new();

    public Day08()
    {
        string inputString = File.ReadAllText(InputFilePath);
        _input = inputString.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        _instructioins = _input[0];

        _graph = new Graph();
        foreach (var line in _input.Skip(2))
        {
            var source = line[..3];
            var left = line.Substring(7, 3);
            var right = line.Substring(12, 3);
            _graph.AddVertex(source, left, right);

            if (line[2] == 'A')
                _startNodes.Add(source);
            if (line[2] == 'Z')
                _endNodes.Add(source);
        }
    }

    public override ValueTask<string> Solve_1()
    {
        var step = 0;
        var curr = "AAA";
        while (curr != "ZZZ")
        {
            var instruction = _instructioins[step % _instructioins.Length];
            if (instruction == 'L')
                curr = _graph.GetChild(curr, 'L');
            else
                curr = _graph.GetChild(curr, 'R');
            step++;
        }
        return new ValueTask<string>(step.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new ValueTask<string>();
    }
}

class Graph
{
    private readonly Dictionary<string, (string left, string right)> _adjacencyList;

    public Graph()
    {
        _adjacencyList = new Dictionary<string, (string, string)>();
    }

    public void AddVertex(string vertex, string left, string right)
    {
        if (!_adjacencyList.ContainsKey(vertex))
        {
            _adjacencyList[vertex] = (left, right);
        }
    }

    public string GetChild(string vertex, char dir) 
    {
        if (dir == 'L')
            return _adjacencyList[vertex].left;
        return _adjacencyList[vertex].right;
    }
}
