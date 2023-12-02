namespace AOC
{
    public static class MainClass
    {
        public static async Task Main(string[] args)
        {
            // Read Input Data
            var inputData = await InputManager.ReadTextByFilename(args);

            // Part 1
            Console.WriteLine($"Puzzle Part 1 Result: {Part1.GetElevatorLevel(inputData)}");

            // Part 2
            Console.WriteLine($"Puzzle Part 2 Result: {Part2.FindFirstElevatorLevel(inputData, -1)}");

        }
    };
}