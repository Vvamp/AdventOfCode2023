namespace AOC
{
    public static class MainClass
    {
        public static async Task Main(string[] args)
        {
            // Read Input Data
            var inputData = await InputManager.ReadLinesByFilename(args);
            foreach (var inputline in inputData)
            {
                Console.WriteLine(inputline);
            }

            // Part 1
            Console.WriteLine($"Puzzle Part 1 Result: {await Part1.GetCalibrationNumberSum(inputData)}");

            // Part 2
            Console.WriteLine($"Puzzle Part 2 Result: {await Part2.GetCalibrationNumberSum(inputData)}");

        }
    };
}