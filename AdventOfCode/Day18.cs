using AoCHelper;

namespace AdventOfCode;

public class Day18 : BaseDay
{
    private readonly string[] _input;

    struct Point(long x, long y)
    {
        public long X = x;
        public long Y = y;
    }

    public Day18()
    {
        string inputString = File.ReadAllText(InputFilePath);
        _input = inputString.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
    }

    public override ValueTask<string> Solve_1()
    {
        (long row, long col) coordinate = (0, 0);
        var polygon = new List<Point>();
        long edgeLength = 0;
        foreach (var instruction in _input) 
        {
            var instructionArr = instruction.Split(' ');
            char direction = instructionArr[0].ToCharArray()[0];
            long meters = int.Parse(instructionArr[1]);

            polygon.Add(new Point(coordinate.row, coordinate.col));
            coordinate = Move(coordinate, direction, meters);
            edgeLength += meters;
        }
        long area = ShoelaceAreaAlgorithm(polygon, edgeLength);
        return new ValueTask<string>(area.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        (long row, long col) coordinate = (0, 0);
        var polygon = new List<Point>();
        long edgeLength = 0;
        foreach (var instruction in _input)
        {
            var hex = instruction.Split(' ')[2];
            char direction = hex[^2];
            long meters = long.Parse(hex.Substring(2, 5), System.Globalization.NumberStyles.HexNumber);

            polygon.Add(new Point(coordinate.row, coordinate.col));
            coordinate = Move(coordinate, direction, meters);
            edgeLength += meters;
        }
        long area = ShoelaceAreaAlgorithm(polygon, edgeLength);
        return new ValueTask<string>(area.ToString());
    }

    private static long ShoelaceAreaAlgorithm(List<Point> polygons, long edgeLength)
    {
        long area = 0;
        int n = polygons.Count;
        for (int i = 0; i < n - 1; i++)
            area += polygons[i].X * polygons[i + 1].Y - polygons[i + 1].X * polygons[i].Y;
        return (edgeLength / 2) + (Math.Abs(area + polygons[n - 1].X * polygons[0].Y - polygons[0].X * polygons[n - 1].Y) / 2) + 1;
    }

    private static (long row, long col) Move((long row, long col) coordinate, char direction, long distance)
    {
        return direction switch
        {
            'U' or '3' => (coordinate.row - distance, coordinate.col),
            'D' or '1' => (coordinate.row + distance, coordinate.col),
            'L' or '2' => (coordinate.row, coordinate.col - distance),
            'R' or '0' => (coordinate.row, coordinate.col + distance),
            _ => throw new ArgumentException($"{direction} is not a valid direction."),
        };
    }

}
