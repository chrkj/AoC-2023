using AoCHelper;
using System.Collections.Generic;

namespace AdventOfCode;

public class Day04 : BaseDay
{
    private readonly string[] _input;

    public Day04()
    {
        string inputString = File.ReadAllText(InputFilePath);
        _input = inputString.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
    }

    public override ValueTask<string> Solve_1()
    {
        int totalPoints = 0;

        for (int i = 0; i < _input.Length; i++) 
        {
            int points = 0;
            string[] card = _input[i].Split(new char[] { '|', ':'});
            List<int> winningNumbers = Utils.ExtractIntegers(card[1]);
            List<int> myNumbers = Utils.ExtractIntegers(card[2]);
            
            foreach (int number in myNumbers)
            {
                if (!winningNumbers.Contains(number))
                    continue;
                if (points == 0)
                    points = 1;
                else
                    points *= 2;
            }
            totalPoints += points;
        }
        return new ValueTask<string>(totalPoints.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        List<(int, int)> cardTable = new List<(int, int)>();
        for (int i = 0; i < _input.Length; i++)
        {
            int points = 0;
            string[] card = _input[i].Split(new char[] { '|', ':' });
            List<int> winningNumbers = Utils.ExtractIntegers(card[1]);
            List<int> myNumbers = Utils.ExtractIntegers(card[2]);

            foreach (int number in myNumbers)
            {
                if (!winningNumbers.Contains(number))
                    continue;
                points++;
            }
            cardTable.Add((1, points));
        }

        int numberOfCards = 0;
        for (int i = 0; i < cardTable.Count; i++)
        {
            for (int j = 0; j < cardTable[i].Item1; j++)
            {
                numberOfCards += cardTable[i].Item1;
                for (int k = i + 1; k < i + cardTable[i].Item2 + 1; k++)
                {
                    if (k > cardTable.Count - 1)
                        break;
                    cardTable[k] = (cardTable[k].Item1 + 1, cardTable[k].Item2);
                }
            }
        }
        int sum = cardTable.Sum(tuple => tuple.Item1);
        return new ValueTask<string>(sum.ToString());
    }

}
