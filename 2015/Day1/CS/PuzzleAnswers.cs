using System.Collections.Generic;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace AOC
{

    public static class Part1
    {
        public static int GetElevatorLevel(string puzzleInput)
        {
            return puzzleInput.Count(c => c == '(') - puzzleInput.Count(c => c == ')');
        }
    }

    public static class Part2
    {
        public static int FindFirstElevatorLevel(string puzzleInput, int elevatorLevel)
        {
            int currentElevatorLevel = 0;
            for (int i = 0; i < puzzleInput.Length; i++)
            {
                if (puzzleInput[i] == '(')
                    currentElevatorLevel += 1;
                else
                    currentElevatorLevel -= 1;

                if (currentElevatorLevel == elevatorLevel)
                    return i + 1; // Index for puzzle starts at 1 instead of 0
            }
            return -1;
        }
    }
}