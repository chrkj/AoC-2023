using AoCHelper;

namespace AdventOfCode;

public class Day02 : BaseDay
{
    private readonly string[] _input;

    public Day02()
    {
        string inputString = File.ReadAllText(InputFilePath);
        _input = inputString.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
    }

    public override ValueTask<string> Solve_1()
    {
        int sum = 0;
        int redCubesContainedInBag = 12;
        int greenCubesContainedInBag = 13;
        int blueCubesContainedInBag = 14;

        for (int i = 0; i < _input.Length; i++)
        {
            bool possible = true;

            string[] currentDraw = _input[i].Split(new char[]{';', ':'});
            int id = Utils.FindFirstNumericSequence(currentDraw[0]);
            for (int j = 1; j < currentDraw.Length; j++)
            {
                string[] cubesInDraw = currentDraw[j].Split(new char[] { ',' });
                foreach (string cube in cubesInDraw) 
                {
                    string[] cubeAndcolor = cube.Split(new char[] { ' ' });
                    string color = cubeAndcolor[2];
                    int amount = int.Parse(cubeAndcolor[1]);

                    if (color == "red" && amount > redCubesContainedInBag ||
                        color == "green" && amount > greenCubesContainedInBag ||
                        color == "blue" && amount > blueCubesContainedInBag)
                    {
                        possible = false;
                        break;
                    }
                }
                if (!possible)
                    break;
            }
            if (possible)
                sum += id;
        }
        return new ValueTask<string>(sum.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        int powerSum = 0;

        for (int i = 0; i < _input.Length; i++)
        {
            int redLeastAmount = int.MinValue;   
            int greenLeastAmount = int.MinValue; 
            int blueLeastAmount = int.MinValue;

            string[] currentDraw = _input[i].Split(new char[] { ';', ':' });
            for (int j = 1; j < currentDraw.Length; j++)
            {
                string[] cubesInDraw = currentDraw[j].Split(new char[] { ',' });
                foreach (string cube in cubesInDraw)
                {
                    string[] cubeAndcolor = cube.Split(new char[] { ' ' });
                    string color = cubeAndcolor[2];
                    int amount = int.Parse(cubeAndcolor[1]);

                    if (color == "red" && amount > redLeastAmount)
                        redLeastAmount = amount;
                    if (color == "green" && amount > greenLeastAmount)
                        greenLeastAmount = amount;
                    if (color == "blue" && amount > blueLeastAmount)
                        blueLeastAmount = amount;
                }
            }
            powerSum += redLeastAmount * blueLeastAmount * greenLeastAmount;
        }
        return new ValueTask<string>(powerSum.ToString());
    }
}
