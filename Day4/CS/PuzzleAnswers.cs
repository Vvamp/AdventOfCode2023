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
    public static class Part1
    {

        public static List<int> GetNumbersFromString(string number_string)
        {
            Regex findNumbersRegex = new Regex(@"[\s]*([\d]+)");

            List<int> numbers = new List<int>();
            var numbers_matches = findNumbersRegex.Matches(number_string);
            foreach (Match numbers_match in numbers_matches)
            {
                var number = Int32.Parse(numbers_match.Groups[1].Value);
                numbers.Add(number);
            }
            return numbers;
        }
        public static int GetCardWinnings(string puzzleLine)
        {
            string line_data = puzzleLine.Split(": ")[1];
            string[] split_linedata = line_data.Split(" | ");
            string winning_numbers_string = split_linedata[0];
            string my_numbers_string = split_linedata[1];
            var winning_numbers = GetNumbersFromString(winning_numbers_string);
            var my_numbers = GetNumbersFromString(my_numbers_string);

            var winnings = (int)Math.Pow(2, my_numbers.Count(num => winning_numbers.Contains(num)) - 1);
            return winnings;
        }
        //28
        public static int GetCardWinningsSum(string[] puzzleData)
        {
            int sum = 0;
            foreach (var line in puzzleData)
            {
                var winnings = GetCardWinnings(line);
                sum += winnings;
            }
            return sum;
        }

    }


    public class Scratchcard
    {
        public Scratchcard(int cardNumber, int[] winningNumbers, int[] myNumbers)
        {
            CardNumber = cardNumber;
            WinningNumbers = winningNumbers;
            MyNumbers = myNumbers;
            Count = 1;
        }
        public int CardNumber { get; set; }
        public int[] WinningNumbers { get; set; }
        public int[] MyNumbers { get; set; }
        public int Count { get; set; }

    }
    public static class Part2
    {
        public static int GetScratchcardNumber(string puzzleLine)
        {
            Regex findNumbersRegex = new Regex(@"[\s]*([\d]+):");

            var numbers_matches = findNumbersRegex.Matches(puzzleLine);
            return Int32.Parse(numbers_matches.First().Groups[1].Value);
        }

        public static List<Scratchcard> GetProcessedScratchcards(List<Scratchcard> original_cards)
        {
            List<Scratchcard> processed_cards = original_cards;
            for (int card_index = 0; card_index < processed_cards.Count; card_index++)
            {
                var card = processed_cards[card_index];
                var win_count = card.MyNumbers.Count(num => card.WinningNumbers.Contains(num));
                for (int i = card_index + 1; i <= card_index + win_count; i++)
                {
                    if (i >= processed_cards.Count)
                        break;
                    processed_cards[i].Count += card.Count;
                }
            }
            return processed_cards;
        }
        //20min
        public static int GetTotalScratchCards(string[] puzzleData)
        {
            List<Scratchcard> original_cards = new List<Scratchcard>();
            foreach (var line in puzzleData)
            {
                var cardid = GetScratchcardNumber(line);
                string line_data = line.Split(": ")[1];
                string[] split_linedata = line_data.Split(" | ");
                string winning_numbers_string = split_linedata[0];
                string my_numbers_string = split_linedata[1];
                var winning_numbers = Part1.GetNumbersFromString(winning_numbers_string);
                var my_numbers = Part1.GetNumbersFromString(my_numbers_string);
                var card = new Scratchcard(cardid, winning_numbers.ToArray(), my_numbers.ToArray());
                original_cards.Add(card);
            }

            var cards = GetProcessedScratchcards(original_cards);
            return cards.Sum(card => card.Count);
        }


    }
}