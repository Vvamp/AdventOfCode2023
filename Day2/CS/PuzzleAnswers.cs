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
        public Game(int GameId, List<Dictionary<CubeColor, int>> CubeColorCounts)
        {
            this.GameId = GameId;
            this.CubeColorCounts = CubeColorCounts;
        }
        public int GameId { get; set; }
        public List<Dictionary<CubeColor, int>> CubeColorCounts { get; set; }
    }
    public static class Part1
    {

        public static List<Game> GetGamesByPuzzleInput(string[] puzzleinput)
        {
            List<Game> gameList = new List<Game>();
            Regex gameIdRegex = new Regex(@"Game (\d+)");
            Regex colorRegex = new Regex(@"(\d+) (blue|red|green)");
            foreach (var line in puzzleinput)
            {
                // Regex for GameId
                var idmatches = gameIdRegex.Matches(line);
                var gameNumber = Int32.Parse(idmatches.First().Groups[1].Value); // Prone to errors in input but works for valid input

                List<Dictionary<CubeColor, int>> cubeColorCountsList = new List<Dictionary<CubeColor, int>>();

                // Split by set in each game
                foreach (var set in line.Split(";"))
                {
                    var colormatches = colorRegex.Matches(set);
                    Dictionary<CubeColor, int> cubeColorCounts = new Dictionary<CubeColor, int>(){
                        { CubeColor.Red, 0},
                        { CubeColor.Blue, 0},
                        { CubeColor.Green, 0}
                    };

                    // Grab each color and add the count to the dictionary
                    foreach (Match match in colormatches)
                    {
                        var colorCount = Int32.Parse(match.Groups[1].Value);
                        var color = match.Groups[2].Value;
                        switch (color)
                        {
                            case "red":
                                cubeColorCounts[CubeColor.Red] += colorCount;
                                break;
                            case "blue":
                                cubeColorCounts[CubeColor.Blue] += colorCount;
                                break;
                            case "green":
                                cubeColorCounts[CubeColor.Green] += colorCount;
                                break;
                        }
                    }

                    // Add the current set's dictionary to the list
                    cubeColorCountsList.Add(cubeColorCounts);
                }
                var newGame = new Game(gameNumber, cubeColorCountsList);
                gameList.Add(newGame);

            }
            return gameList;
        }

        public static bool IsGamePossible(Dictionary<CubeColor, int> goalPerCubeColor, Game game)
        {
            foreach (var key in goalPerCubeColor.Keys)
            {
                var targetCount = goalPerCubeColor[key];
                foreach (var countPerDictionary in game.CubeColorCounts)
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
        public static int GetSumOfCubes(Dictionary<CubeColor, int> goalPerCubeColor, string[] puzzleinput)
        {
            var games = GetGamesByPuzzleInput(puzzleinput);
            // int sum = 0;
            // return sum;

            return games
                        .Where(game => IsGamePossible(goalPerCubeColor, game))
                        .Sum(game => game.GameId);

        }
    }

    public static class Part2
    {
        public static Dictionary<CubeColor, int> GetMinCubesPerGame(Game game)
        {
            Dictionary<CubeColor, int> minCubesPerColor = new Dictionary<CubeColor, int>(){
                { CubeColor.Red, 0},
                { CubeColor.Blue, 0},
                { CubeColor.Green, 0}
            };
            foreach (var set in game.CubeColorCounts)
            {
                foreach (var color in set.Keys)
                {
                    var currentColorCount = set[color];
                    if (currentColorCount > minCubesPerColor[color])
                        minCubesPerColor[color] = currentColorCount;
                }
            }
            return minCubesPerColor;
        }
        public static int GetSumOfPowerOfCubes(string[] puzzleinput)
        {
            var games = Part1.GetGamesByPuzzleInput(puzzleinput);
            int sum = 0;
            foreach (var game in games)
            {
                var cubeColorCounts = GetMinCubesPerGame(game);
                int curpow = 0;
                foreach (var color in cubeColorCounts.Keys)
                {
                    if (curpow == 0)
                        curpow = cubeColorCounts[color];
                    else
                        curpow *= cubeColorCounts[color];
                }
                sum += curpow;

            }
            return sum;
        }
    }
}