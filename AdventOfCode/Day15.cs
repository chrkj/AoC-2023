using AoCHelper;

namespace AdventOfCode;

public class Day15 : BaseDay
{
    private readonly char[][] _input;

    public Day15()
    {
        string inputString = File.ReadAllText(InputFilePath);
        _input = inputString.Split(',').Select(line => line.ToCharArray()).ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        long sum = 0;
        foreach (var line in _input)
        {
            int value = 0;
            foreach (var ch in line)
            {
                value += ch;
                value *= 17;
                value %= 256;
            }
            sum += value;
        }
        return new ValueTask<string>(sum.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        Dictionary<int, List<(string label, int focalLength)>> boxes = new();
        
        foreach (var line in _input)
        {
            int box = 0;
            string label = "";
            foreach (var ch in line)
            {
                if (ch < 'a')
                    break;
                box += ch;
                box *= 17;
                box %= 256;
                label += ch;
            }
            if (line.Contains('='))
            {
                int focalLength = line[^1] - '0';
                if (boxes.TryGetValue(box, out List<(string label, int focalLength)> lenses))
                {
                    int index = lenses.FindIndex(item => item.label == label);
                    if (index != -1)
                        lenses[index] = (label, focalLength);
                    else
                        lenses.Add((label, focalLength));
                }
                else
                    boxes[box] = new List<(string label, int focalLength)> { (label, focalLength) };
            }
            else
            {
                if (boxes.TryGetValue(box, out List<(string label, int focalLength)> lenses))
                {
                    int index = lenses.FindIndex(item => item.label == label);
                    if (index != -1)
                        lenses.RemoveAt(index);
                }
            }
        }
        long lensPower = 0;
        foreach (var box in boxes)
        {
            int slotNr = 1;
            foreach (var (_, focalLength) in box.Value) 
            {
                lensPower += (box.Key + 1) * slotNr * focalLength;
                slotNr++;
            }
        }
        return new ValueTask<string>(lensPower.ToString());
    }
}
