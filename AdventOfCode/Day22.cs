using AoCHelper;
using System.Numerics;

namespace AdventOfCode;

public class Day22 : BaseDay
{
    private readonly string[] _input;
    private readonly List<Brick> _bricks;
    private readonly char[,,] _space;

    public Day22()
    {
        _input = File.ReadAllText(InputFilePath).Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        int i = 0;
        foreach (var line in _input)
        {
            var brick = new Brick();
            var points = line.Split(['~', ',']).Select(float.Parse).ToArray();
            brick.startPos = new Vector3(points[0], points[1], points[2]);
            brick.endPos = new Vector3(points[3], points[4], points[5]);
            brick.id = i;
            _bricks.Add(brick);
            i++;
        }
        _space = new char[10,10,1000];
    }

    public override ValueTask<string> Solve_1()
    {
        return new ValueTask<string>();
    }

    public override ValueTask<string> Solve_2()
    {
        return new ValueTask<string>();
    }

    struct Brick
    {
        public int id;
        public Vector3 startPos;
        public Vector3 endPos;
    }

}
