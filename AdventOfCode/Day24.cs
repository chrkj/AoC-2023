using AoCHelper;

namespace AdventOfCode;

public class Day24 : BaseDay
{
    private readonly string[] _input;
    private readonly List<Hail> _hail;
    private static readonly char[] separator = ['@', ','];

    public Day24()
    {
        _hail = [];
        _input = File.ReadAllText(InputFilePath).Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        foreach (var line in _input)
        {
            var data = line.Split(separator, StringSplitOptions.TrimEntries).Select(long.Parse).ToArray();
            var hail = new Hail();
            hail.position = [data[0], data[1], data[2]];
            hail.velocity = [data[3], data[4], data[5]];
            hail.a = (double)data[4] / data[3];
            hail.b = data[1] - hail.a * data[0];
            _hail.Add(hail);
        }
    }

    public override ValueTask<string> Solve_1()
    {
        int intersections = 0;
        for (int i = 0; i < _hail.Count; i++)
            for (int j = i + 1; j < _hail.Count; j++)
                if (IsIntersecting(_hail[i], _hail[j], 200000000000000, 400000000000000))
                    intersections++;
        return new ValueTask<string>(intersections.ToString());
    }

    private static bool IsIntersecting(Hail h1, Hail h2, long lowerBoundry, long upperBoundty)
    {
        if (h1.a == h2.a)
            return false;

        double x = (h2.b - h1.b) / (h1.a - h2.a);
        double y = h1.a * x + h1.b;

        if (CheckIntersection(h1.velocity, h1.position, x, y) || CheckIntersection(h2.velocity, h2.position, x, y))
            return false;

        if (y <= lowerBoundry || y >= upperBoundty || x <= lowerBoundry || y >= upperBoundty)
            return false;

        return true;
    }

    public override ValueTask<string> Solve_2()
    {
        return new ValueTask<string>();
    }

    private static bool CheckIntersection(long[] velocity, long[] position, double x, double y)
    {
        return (velocity[0] > 0 && x < position[0]) ||
               (velocity[0] < 0 && x > position[0]) ||
               (velocity[1] > 0 && y < position[1]) ||
               (velocity[1] < 0 && y > position[1]);
    }

    struct Hail
    {
        public long[] position;
        public long[] velocity;
        public double a;
        public double b;
    }

}
