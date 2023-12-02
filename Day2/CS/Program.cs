namespace AOC
{
    public static class MainClass
    {
        public static async Task Main(string[] args)
        {
            // Read Input Data
            var inputData = await InputManager.ReadLinesByFilename(args);

            // Goal
            var goal = new Dictionary<CubeColor, int>(){
                {CubeColor.Red, 12},
                {CubeColor.Green, 13},
                {CubeColor.Blue, 14},
            };

            // Part 1
            Console.WriteLine($"Puzzle Part 1 Result: {await Part1.GetSumOfCubes(goal, inputData)}");

            // Part 2
            Console.WriteLine($"Puzzle Part 2 Result: {await Part2.GetSumOfPowerOfCubes(inputData)}");

        }
    };
}