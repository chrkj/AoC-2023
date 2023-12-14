using AoCHelper;

if (args.Length == 0)
{
    await Solver.SolveLast(opt => opt.ClearConsole = true);
}
else if (args.Length == 1 && args[0].Contains("all", StringComparison.CurrentCultureIgnoreCase))
{
    await Solver.SolveAll(opt =>
    {
        opt.ShowConstructorElapsedTime = true;
        opt.ShowTotalElapsedTimePerDay = true;
        opt.ShowOverallResults = true;
    });
}
else if (args.Length == 1)
{
    await Solver.Solve(new List<uint> { uint.Parse(args[0]) });
}
else
{
    var indexes = args.Select(arg => uint.TryParse(arg, out var index) ? index : uint.MaxValue);

    await Solver.Solve(indexes.Where(i => i < uint.MaxValue));
}
