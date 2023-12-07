using AoCHelper;

namespace AdventOfCode;

public class Day06 : BaseDay
{
    private readonly string[] _input;
    private readonly long[] _raceTime;
    private readonly long[] _recordDistance;

    public Day06()
    {
        string inputString = File.ReadAllText(InputFilePath);
        _input = inputString.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        _raceTime = Utils.ExtractLongs(_input[0]).ToArray();
        _recordDistance = Utils.ExtractLongs(_input[1]).ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        var result = 1;
        for (long race = 0; race < _raceTime.Length; race++)
        { 
            var numWins = 0;
            for (long secondsHold = 1; secondsHold < _raceTime[race]; secondsHold++)
            {
                var distanceTraveled = (_raceTime[race] - secondsHold) * secondsHold;
                if (distanceTraveled > _recordDistance[race])
                    numWins++;
            }
            result *= numWins;
        }
        return new ValueTask<string>(result.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        long racetime = long.Parse(string.Concat(_raceTime));
        long record = long.Parse(string.Concat(_recordDistance));

        var a = -1;
        double discriminant = racetime * racetime - 4 * a * (-record);
        double root1 = (-racetime + Math.Sqrt(discriminant)) / (2 * a);
        double root2 = (-racetime - Math.Sqrt(discriminant)) / (2 * a);



        return new ValueTask<string>((Math.Floor(root2) - Math.Floor(root1)).ToString());
    }
}
