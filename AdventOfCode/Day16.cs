using AoCHelper;

namespace AdventOfCode;

public class Day16 : BaseDay
{
    private readonly char[][] _input;

    public Day16()
    {
        string inputString = File.ReadAllText(InputFilePath);
        var str = inputString.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        _input = str.Select(line => line.ToCharArray()).ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        var seen = FindEnergized((new Pos(0, 0), Dir.Right));
        return new ValueTask<string>(seen.Select(n => n.pos).Distinct().Count().ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var mostEnergized = 0;
        for (int i = 0; i < _input.Length; i++)
        {
            mostEnergized = Math.Max(mostEnergized, FindEnergized((new Pos(i, 0), Dir.Right)).Select(n => n.pos).Distinct().Count());
            mostEnergized = Math.Max(mostEnergized, FindEnergized((new Pos(i, _input.Length - 1), Dir.Left)).Select(n => n.pos).Distinct().Count());
            mostEnergized = Math.Max(mostEnergized, FindEnergized((new Pos(0, i), Dir.Down)).Select(n => n.pos).Distinct().Count());
            mostEnergized = Math.Max(mostEnergized, FindEnergized((new Pos(_input.Length - 1, i), Dir.Up)).Select(n => n.pos).Distinct().Count());
        }
        return new ValueTask<string>(mostEnergized.ToString());
    }

    private HashSet<(Pos pos, Dir dir)> FindEnergized((Pos, Dir) Start)
    {
        var seen = new HashSet<(Pos pos, Dir dir)>();
        var stack = new Stack<(Pos pos, Dir dir)>();
        stack.Push(Start);

        while (stack.TryPop(out var beam))
        {
            if (seen.Contains(beam))
                continue;

            if (!Utils.IsWithinBounds(_input, beam.pos.row, beam.pos.col))
                continue;

            seen.Add(beam);
            switch (_input[beam.pos.row][beam.pos.col])
            {
                case '/':
                    if (beam.dir == Dir.Right)
                        stack.Push((new Pos(beam.pos.row - 1, beam.pos.col), Dir.Up));
                    else if (beam.dir == Dir.Left)
                        stack.Push((new Pos(beam.pos.row + 1, beam.pos.col), Dir.Down));
                    else if (beam.dir == Dir.Up)
                        stack.Push((new Pos(beam.pos.row, beam.pos.col + 1), Dir.Right));
                    else if (beam.dir == Dir.Down)
                        stack.Push((new Pos(beam.pos.row, beam.pos.col - 1), Dir.Left));
                    break;
                case '\\':
                    if (beam.dir == Dir.Right)
                        stack.Push((new Pos(beam.pos.row + 1, beam.pos.col), Dir.Down));
                    else if (beam.dir == Dir.Left)
                        stack.Push((new Pos(beam.pos.row - 1, beam.pos.col), Dir.Up));
                    else if (beam.dir == Dir.Up)
                        stack.Push((new Pos(beam.pos.row, beam.pos.col - 1), Dir.Left));
                    else if (beam.dir == Dir.Down)
                        stack.Push((new Pos(beam.pos.row, beam.pos.col + 1), Dir.Right));

                    break;
                case '-':
                    if (beam.dir == Dir.Up || beam.dir == Dir.Down)
                    {
                        stack.Push((new Pos(beam.pos.row, beam.pos.col - 1), Dir.Left));
                        stack.Push((new Pos(beam.pos.row, beam.pos.col + 1), Dir.Right));
                    }
                    if (beam.dir == Dir.Left)
                        stack.Push((new Pos(beam.pos.row, beam.pos.col - 1), beam.dir));
                    else if (beam.dir == Dir.Right)
                        stack.Push((new Pos(beam.pos.row, beam.pos.col + 1), beam.dir));
                    break;
                case '|':
                    if (beam.dir == Dir.Left || beam.dir == Dir.Right)
                    {
                        stack.Push((new Pos(beam.pos.row - 1, beam.pos.col), Dir.Up));
                        stack.Push((new Pos(beam.pos.row + 1, beam.pos.col), Dir.Down));
                    }
                    else if (beam.dir == Dir.Up)
                        stack.Push((new Pos(beam.pos.row - 1, beam.pos.col), beam.dir));
                    else if (beam.dir == Dir.Down)
                        stack.Push((new Pos(beam.pos.row + 1, beam.pos.col), beam.dir));
                    break;
                default:
                    if (beam.dir == Dir.Left)
                        stack.Push((new Pos(beam.pos.row, beam.pos.col - 1), beam.dir));
                    else if (beam.dir == Dir.Right)
                        stack.Push((new Pos(beam.pos.row, beam.pos.col + 1), beam.dir));
                    else if (beam.dir == Dir.Up)
                        stack.Push((new Pos(beam.pos.row - 1, beam.pos.col), beam.dir));
                    else if (beam.dir == Dir.Down)
                        stack.Push((new Pos(beam.pos.row + 1, beam.pos.col), beam.dir));
                    break;
            }
        }

        return seen;
    }

    struct Pos
    {
        public Pos(int row, int col)
        {
            this.col = col;
            this.row = row;
        }
        public int row;
        public int col;
    }

    enum Dir
    {
        Up = 1,
        Down = 2,
        Left = 3,
        Right = 4
    }

}
