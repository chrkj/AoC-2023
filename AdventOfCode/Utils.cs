using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public static class Utils
    {
        public static int FindFirstNumericSequence(string input)
        {
            Match match = Regex.Match(input, @"\d+");

            if (match.Success)
                return Convert.ToInt32(match.Value);
            throw new InvalidOperationException("No numeric sequence found.");
        }
    }
}
