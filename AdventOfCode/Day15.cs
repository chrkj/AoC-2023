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
        foreach (var sequence in _input)
        {
            int value = 0;
            foreach (var ch in sequence)
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
        foreach (var sequence in _input)
        {
            CalculateHash(sequence, out int box, out string label);
            if (sequence[^2] == '=')
                InsertOrSwapLens(boxes, sequence, box, label);
            else
                RemoveLens(boxes, box, label);
        }
        long lensPower = CalculateLenspower(boxes);
        return new ValueTask<string>(lensPower.ToString());
    }

    private static void CalculateHash(char[] sequence, out int box, out string label)
    {
        box = 0;
        label = "";
        foreach (var ch in sequence)
        {
            if (ch < 'a')
                break;
            box += ch;
            box *= 17;
            box %= 256;
            label += ch;
        }
    }

    private static long CalculateLenspower(Dictionary<int, List<(string label, int focalLength)>> boxes)
    {
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
        return lensPower;
    }

    private static void RemoveLens(Dictionary<int, List<(string label, int focalLength)>> boxes, int box, string label)
    {
        if (boxes.TryGetValue(box, out List<(string label, int focalLength)> lenses))
        {
            int index = lenses.FindIndex(item => item.label == label);
            if (index != -1)
                lenses.RemoveAt(index);
        }
    }

    private static void InsertOrSwapLens(Dictionary<int, List<(string label, int focalLength)>> boxes, char[] line, int box, string label)
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
}
