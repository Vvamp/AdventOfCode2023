namespace AOC
{
    /// <summary>
    /// Reads puzzle input
    /// </summary>
    public static class InputManager
    {
        public static async Task<string> ReadFile(string path)
        {
            return await File.ReadAllTextAsync(path);
        }
        public static async Task<string[]> ReadFileLines(string path)
        {
            return await File.ReadAllLinesAsync(path);
        }

        public static async Task<string[]> ReadLinesByFilename(string[] mainArgs)
        {
            var filename = mainArgs.FirstOrDefault();
            string[] lines = new string[0];
            while (filename == null || filename == "")
            {
                Console.Write("$ Enter a puzzle input filename > ");
                filename = Console.ReadLine();
                try
                {
#pragma warning disable CS8604 // Disable possible null ref(it will never be null due to the catch)
                    lines = await InputManager.ReadFileLines(filename);
#pragma warning restore CS8604 // Disable possible null ref(it will never be null due to the catch)
                }
                catch
                {
                    Console.Error.WriteLine($"Invalid path '{filename}'! Please enter a valid file path.");
                    filename = null;
                }
            }
            return lines;
        }

    };
}