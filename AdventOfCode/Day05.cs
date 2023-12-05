using AoCHelper;

namespace AdventOfCode;

public class Day05 : BaseDay
{
    private readonly string[] _input;
    private readonly List<long> _seeds;
    private readonly List<List<Map>> _maps = new();

    struct Map
    {
        public long to;
        public long from;
        public long offset;
        public Map(long to, long from, long offset) { this.to = to; this.from = from; this.offset = offset; }
    }

    public Day05()
    {
        string inputString = File.ReadAllText(InputFilePath);
        _input = inputString.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        _seeds = _input[0].Split(' ').Skip(1).Select(long.Parse).ToList();

        List<Map> currentMap = new();
        foreach (string line in _input.Skip(2))
        {
            if (line.EndsWith(':'))
                continue;
            if (line.Length == 0)
            {
                _maps?.Add(currentMap);
                currentMap = new();
                continue;
            }
            long[] lineData = line.Split(' ').Select(long.Parse).ToArray();
            currentMap.Add(new Map(lineData[1] + lineData[2], lineData[1], lineData[0] - lineData[1]));
        }
        _maps?.Add(currentMap);
    }

    public override ValueTask<string> Solve_1()
    {
        long lowestLocationNumber = long.MaxValue;
        foreach (long seed in _seeds)
        {
            long value = seed;
            foreach (List<Map> map in _maps) 
            {
                foreach (Map mapping in map)
                {
                    if (value <= mapping.to && value >= mapping.from)
                    {
                        value += mapping.offset;
                        break;
                    }
                }
            }
            lowestLocationNumber = Math.Min(lowestLocationNumber, value);
        }
        return new ValueTask<string>(lowestLocationNumber.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        List<(long from, long to)> seedRanges = new();
        for (int i = 0; i < _seeds.Count; i += 2)
            seedRanges.Add((_seeds[i], _seeds[i] + _seeds[i + 1] - 1));

        foreach (List<Map> map in _maps)
        {
            var orderedMap = map.OrderBy(x => x.from).ToList();
            var newRanges = new List<(long from, long to)>();
            foreach (var seedRange in seedRanges)
            {
                var range = seedRange;
                foreach (Map mapping in orderedMap)
                {
                    if (range.from < mapping.from)
                    {
                        newRanges.Add((range.from, Math.Min(range.to, mapping.from - 1)));
                        range.from = mapping.from;
                        if (range.from > range.to)
                            break;
                    }

                    if (range.from <= mapping.to)
                    {
                        newRanges.Add((range.from + mapping.offset, Math.Min(range.to, mapping.to) + mapping.offset));
                        range.from = mapping.to;
                        if (range.from > range.to)
                            break;
                    }
                }
                if (range.from <= range.to)
                    newRanges.Add(range);
            }
            seedRanges = newRanges;
        }
        long lowestLocationNumber = seedRanges.Min(r => r.from);
        return new ValueTask<string>(lowestLocationNumber.ToString());
    }
}
