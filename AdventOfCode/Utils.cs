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

        public static List<int> FindIndicesOfSymbols(string input, string symbols)
        {
            List<int> indices = new List<int>();

            foreach (char symbol in symbols)
            {
                int index = input.IndexOf(symbol);
                while (index != -1)
                {
                    indices.Add(index);
                    index = input.IndexOf(symbol, index + 1);
                }
            }
            return indices;
        }

        public static int ExtractValueAroundIndex(string input, int index)
        {
            int startIndex = index;

            // Move left in the string until a non-digit character is encountered
            while (startIndex >= 0 && char.IsDigit(input[startIndex]))
            {
                startIndex--;
            }
            
            // Move right in the string until a non-digit character is encountered
            int endIndex = index;
            while (endIndex < input.Length && char.IsDigit(input[endIndex]))
            {
                endIndex++;
            }

            // Use Substring to extract the value
            return int.Parse(input.Substring(startIndex + 1, endIndex - startIndex - 1));
        }

        public static void PrintList<T>(List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
                Console.WriteLine(list[i]);
        }

        public static void PrintArray<T>(T[] array)
        {
            for (int i = 0; i < array.Length; i++)
                Console.WriteLine(array[i]);
        }

        public static bool IsWithinBounds<T>(T[,] array, int row, int col)
        {
            return row >= 0 && row < array.GetLength(0) && col >= 0 && col < array.GetLength(1);
        }

        public static bool IsWithinBounds(string[] array, int row, int col)
        {
            if (row > array[0].Length - 1 || row < 0 || col > array.Length - 1 || col < 0)
                return true;
            return false;
        }

        public static IEnumerable<int> ExtractInts(string str)
        {
            return Regex.Matches(str, "-?\\d+").Select(m => int.Parse(m.Value));
        }

        public static IEnumerable<long> ExtractLongs(string str)
        {
            return Regex.Matches(str, "-?\\d+").Select(m => long.Parse(m.Value));
        }

    }
}
