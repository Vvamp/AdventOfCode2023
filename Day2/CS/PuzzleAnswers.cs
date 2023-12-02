using System.Collections.Generic;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;

namespace AOC
{
    public enum CubeColor
    {
        Blue,
        Red,
        Green

    }
    public class Game
    {
        public Game(int GameId, List<Dictionary<CubeColor, int>> colordict)
        {
            this.GameId = GameId;
            this.CountPerDictionary = colordict;
        }
        public int GameId { get; set; }
        public List<Dictionary<CubeColor, int>> CountPerDictionary { get; set; }
    }
    public static class Part1
    {

        public static async Task<List<Game>> GetGamesByPuzzleInput(string[] puzzleinput)
        {
            List<Game> gameList = new List<Game>();
            Regex gamerx = new Regex(@"Game (\d+)");
            Regex colorrx = new Regex(@"(\d+) (blue|red|green)");
            foreach (var line in puzzleinput)
            {
                // Regex for gameId
                var idmatches = gamerx.Matches(line);
                var digit = Int32.Parse(idmatches.FirstOrDefault()?.Groups[1].Value);
                if (digit == null)
                    continue;
                List<Dictionary<CubeColor, int>> cubeColorList = new List<Dictionary<CubeColor, int>>();

                foreach (var set in line.Split(";"))
                {
                    var colormatches = colorrx.Matches(set);
                    Dictionary<CubeColor, int> countsByCubeColor = new Dictionary<CubeColor, int>(){
                        { CubeColor.Red, 0},
                        { CubeColor.Blue, 0},
                        { CubeColor.Green, 0}
                    };
                    foreach (Match match in colormatches)
                    {
                        var colorCount = Int32.Parse(match.Groups[1].Value);
                        var color = match.Groups[2].Value;
                        switch (color)
                        {
                            case "red":
                                countsByCubeColor[CubeColor.Red] += colorCount;
                                break;
                            case "blue":
                                countsByCubeColor[CubeColor.Blue] += colorCount;
                                break;
                            case "green":
                                countsByCubeColor[CubeColor.Green] += colorCount;
                                break;
                        }
                    }
                    cubeColorList.Add(countsByCubeColor);
                }
                var newGame = new Game(digit, cubeColorList);
                gameList.Add(newGame);

            }
            return gameList;
        }

        public static async Task<bool> IsGamePossible(Dictionary<CubeColor, int> goalPerCubeColor, Game game)
        {
            foreach (var key in goalPerCubeColor.Keys)
            {
                var targetCount = goalPerCubeColor[key];
                foreach (var countPerDictionary in game.CountPerDictionary)
                {
                    var gameCount = countPerDictionary[key];
                    if (gameCount > targetCount)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public static async Task<int> GetSumOfCubes(Dictionary<CubeColor, int> goalPerCubeColor, string[] puzzleinput)
        {
            var games = await GetGamesByPuzzleInput(puzzleinput);
            int sum = 0;
            foreach (var game in games)
            {
                if (await IsGamePossible(goalPerCubeColor, game))
                {
                    sum += game.GameId;
                }
            }
            return sum;
        }
    }

    public static class Part2
    {
        public static async Task<Dictionary<CubeColor, int>> GetMinCubesPerGame(Game game)
        {
            Dictionary<CubeColor, int> minCubesPerColor = new Dictionary<CubeColor, int>(){
                { CubeColor.Red, 0},
                { CubeColor.Blue, 0},
                { CubeColor.Green, 0}
            };
            foreach (var set in game.CountPerDictionary)
            {
                foreach (var key in set.Keys)
                {
                    var count = set[key];
                    if (count > minCubesPerColor[key])
                        minCubesPerColor[key] = count;
                }
            }
            return minCubesPerColor;
        }
        public static async Task<int> GetSumOfPowerOfCubes(string[] puzzleinput)
        {
            var games = await Part1.GetGamesByPuzzleInput(puzzleinput);
            int sum = 0;
            foreach (var game in games)
            {
                var min = await GetMinCubesPerGame(game);
                int curpow = 0;
                foreach (var key in min.Keys)
                {
                    if (curpow == 0)
                        curpow = min[key];
                    else
                        curpow *= min[key];
                    // Console.Write($"{key}: {min[key]}");
                }
                sum += curpow;

            }
            return sum;
        }
    }
}