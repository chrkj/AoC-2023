using AoCHelper;

namespace AdventOfCode;

public class Day17 : BaseDay
{
    private readonly int[][] _input;

    public Day17()
    {
        string inputString = File.ReadAllText(InputFilePath);
        var str = inputString.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        _input = str.Select(s => s.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        var cost = Traverse(_input, 1, 3);
        return new ValueTask<string>(cost.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var cost = Traverse(_input, 4, 10);
        return new ValueTask<string>(cost.ToString());
    }

    public int Traverse(int[][] map, int minSteps, int maxSteps)
    {
        PriorityQueue<(int y, int x, Direction direction, int directionMoves), int> queue = new();
        Dictionary<(Direction direction, int directionMoves), int>[][] visited = new Dictionary<(Direction, int), int>[map.Length][];
        for (var y = 0; y < map.Length; y++)
        {
            visited[y] = new Dictionary<(Direction, int), int>[map[0].Length];
            for (var x = 0; x < map[0].Length; x++)
                visited[y][x] = [];
        }

        queue.Enqueue((0, 0, Direction.E, 0), 0);
        queue.Enqueue((0, 0, Direction.S, 0), 0);

        while (queue.Count > 0)
        {
            var (y, x, direction, directionMoves) = queue.Dequeue();
            var currentHeat = visited[y][x].GetValueOrDefault((direction, directionMoves));

            if (directionMoves < maxSteps)
                Move(y, x, direction, currentHeat, directionMoves);

            if (directionMoves >= minSteps)
            {
                Move(y, x, L90(direction), currentHeat, 0);
                Move(y, x, R90(direction), currentHeat, 0);
            }
        }

        var maxY = map.Length - 1;
        var maxX = map[0].Length - 1;

        return visited[maxY][maxX].Min(x => x.Value);

        void Move(int y, int x, Direction direction, int heat, int directionMoves)
        {
            var dy = direction switch
            {
                Direction.N => -1,
                Direction.S => 1,
                _ => 0
            };

            var dx = direction switch
            {
                Direction.E => 1,
                Direction.W => -1,
                _ => 0
            };

            for (var i = 1; i <= maxSteps; i++)
            {
                var newY = y + i * dy;
                var newX = x + i * dx;
                var newDirectionMoves = directionMoves + i;

                if (newY < 0 || newY >= map.Length || newX < 0 || newX >= map[0].Length || newDirectionMoves > maxSteps)
                    return;

                heat += map[newY][newX];

                if (i < minSteps) continue;

                var vlist = visited[newY][newX];

                if (vlist.TryGetValue((direction, newDirectionMoves), out var visitedHeat))
                {
                    if (visitedHeat <= heat)
                        return;
                }

                queue.Enqueue((newY, newX, direction, newDirectionMoves), heat);
                vlist[(direction, newDirectionMoves)] = heat;
            }
        }
    }

    static Direction L90(Direction direction) => direction switch
    {
        Direction.N => Direction.W,
        Direction.W => Direction.S,
        Direction.S => Direction.E,
        Direction.E => Direction.N,
        _ => throw new NotImplementedException(),
    };
    static Direction R90(Direction direction) => direction switch
    {
        Direction.N => Direction.E,
        Direction.E => Direction.S,
        Direction.S => Direction.W,
        Direction.W => Direction.N,
        _ => throw new NotImplementedException(),
    };

    enum Direction
    {
        N = 0,
        W = 1,
        S = 2,
        E = 3
    }
}

