using System.Collections.Generic;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace AOC
{

    public static class Part1
    {
        public static async Task<int> GetCalibrationNumberInString(string line)
        {
            int number = 0;
            try
            {
                string numberFirst = line.First(c => c >= '0' && c <= '9').ToString();
                string numberLast = line.Last(c => c >= '0' && c <= '9').ToString();
                number = Int32.Parse(numberFirst + numberLast);
            }
            catch
            {
                return 0;
            }
            return number;
        }

        public static async Task<int> GetCalibrationNumberSum(string[] lines)
        {
            var tasks = lines.Select(line => GetCalibrationNumberInString(line));
            var results = await Task.WhenAll(tasks);
            return results.Sum();
        }
    }

    public static class Part2
    {
        private static Dictionary<string, int> numberDict = new Dictionary<string, int>(){
            {"one", 1},
            {"two", 2},
            {"three", 3},
            {"four", 4},
            {"five", 5},
            {"six", 6},
            {"seven", 7},
            {"eight", 8},
            {"nine", 9},

        };
        public static async Task<string> ParseNumberTextInString(string line)
        {
            // Only have to parse the first and last numbers
            List<(int, string)> numberstringsByIndex = new List<(int, string)>();
            foreach (var key in numberDict.Keys)
            {
                var string_index = line.IndexOf(key);
                if (string_index != -1)
                    numberstringsByIndex.Add((string_index, key));

                // Grab first and last so I don't only find one if there are duplicate numbers. Duplicates if there are only 1 don't matter anyway
                var string_index2 = line.LastIndexOf(key);
                if (string_index2 != -1)
                    numberstringsByIndex.Add((string_index2, key));
            }
            if (numberstringsByIndex.Count == 0)
                return line;

            var (min_index, min_key) = numberstringsByIndex.MinBy(i => i.Item1);
            var (max_index, max_key) = numberstringsByIndex.MaxBy(i => i.Item1);

            StringBuilder sb = new StringBuilder(line);
            sb[min_index] = numberDict[min_key].ToString()[0];
            sb[max_index] = numberDict[max_key].ToString()[0];
            return sb.ToString();
        }

        public static async Task<int> GetCalibrationNumberSum(string[] lines)
        {
            var tasks = lines.Select(line => ParseNumberTextInString(line));
            var results = await Task.WhenAll(tasks);

            return await Part1.GetCalibrationNumberSum(results);
        }
    }
}