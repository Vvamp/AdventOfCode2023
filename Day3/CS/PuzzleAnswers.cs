using System.Collections.Generic;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace AOC
{



    public struct Vector
    {
        public int x { get; set; }
        public int y { get; set; }
    }


    public static class Part1
    {
        public static List<Vector> checkVectors = new List<Vector>(){
            new Vector{x=1,y=0},
            new Vector{x=-1,y=0},
            new Vector{x=0,y=1},
            new Vector{x=0,y=-1},
            new Vector{x=1,y=1},
            new Vector{x=1,y=-1},
            new Vector{x=-1,y=-1},
            new Vector{x=-1,y=1}

        };

        public static Vector AddVectors(Vector a, Vector b)
        {
            return new Vector()
            {
                x = a.x + b.x,
                y = a.y + b.y
            };
        }

        public static int[] FindNumbersAdjacentToIndex(string[] puzzleInput, Vector index)
        {
            List<int> numbers = new List<int>();
            var puzzleInputTemp = (string[])puzzleInput.Clone(); // Needed as to not modify the parameter here
            foreach (var checkVector in checkVectors)
            {
                var vectorToCheck = AddVectors(index, checkVector);
                char targetContent;
                try
                {
                    targetContent = puzzleInputTemp[vectorToCheck.y][vectorToCheck.x];
                }
                catch
                {
                    continue;
                }
                if (targetContent >= '0' && targetContent <= '9')
                {
                    // Number found!
                    // Find left
                    // Keep finding numbers until rightmost
                    Vector currentVector = vectorToCheck;
                    while (targetContent >= '0' && targetContent <= '9')
                    {
                        if (currentVector.x - 1 < 0)
                        {
                            break;
                        }
                        var tempContent = puzzleInputTemp[currentVector.y][currentVector.x - 1];
                        if (tempContent >= '0' && tempContent <= '9')
                        {
                            currentVector = AddVectors(currentVector, new Vector() { x = -1, y = 0 });
                            targetContent = tempContent;
                        }
                        else
                        {
                            break;
                        }
                    }

                    // Now at leftmost. Now keep going right until non exist or non-num
                    string numberString = "";
                    while (targetContent >= '0' && targetContent <= '9')
                    {
                        numberString += targetContent;

                        // Replace number with . as to not find double numbers
                        StringBuilder sb = new StringBuilder(puzzleInputTemp[currentVector.y]);
                        sb[currentVector.x] = '.';
                        puzzleInputTemp[currentVector.y] = sb.ToString();

                        currentVector = AddVectors(currentVector, new Vector() { x = 1, y = 0 });
                        if (currentVector.x >= puzzleInputTemp[currentVector.y].Length)
                        {
                            break;
                        }
                        targetContent = puzzleInputTemp[currentVector.y][currentVector.x];
                    }
                    int number = Int32.Parse(numberString);
                    numbers.Add(number);
                }
            }

            return numbers.ToArray();
        }

        // 56min
        public static int[] GetValidNumbers(string[] input)
        {
            List<int> numbers = new List<int>();
            for (int y = 0; y < input.Length; y++)
            {
                var line = input[y];
                for (int x = 0; x < line.Length; x++)
                {
                    var c = line[x];
                    if (c != '.' && (c < '0' || c > '9'))
                    {
                        var nums = FindNumbersAdjacentToIndex(input, new Vector() { x = x, y = y });
                        foreach (var num in nums)
                        {
                            numbers.Add(num);
                        }
                    }
                }
            }
            return numbers.ToArray();
        }

        public static int GetSumOfValidNumbers(string[] input)
        {
            return GetValidNumbers(input).Sum();
        }
    }

    public static class Part2
    {
        // 8 min
        public static int[] GetGearRatios(string[] input)
        {
            List<int> numbers = new List<int>();
            for (int y = 0; y < input.Length; y++)
            {
                var line = input[y];
                for (int x = 0; x < line.Length; x++)
                {
                    var c = line[x];
                    if (c == '*')
                    {
                        var nums = Part1.FindNumbersAdjacentToIndex(input, new Vector() { x = x, y = y });
                        if (nums.Length == 2)
                        {
                            numbers.Add(nums[0] * nums[1]);
                        }
                    }
                }
            }
            return numbers.ToArray();
        }
        public static int GetSumOfGearRatios(string[] input)
        {
            return GetGearRatios(input).Sum();
        }

    }
}