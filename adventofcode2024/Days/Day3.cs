using System.Text.RegularExpressions;

namespace adventofcode2024.Days;

internal static class Day3
{
    public static void Run()
    {
        var input = Helpers.GetFileAsString("day3_input");

        // Removing newlines prevents some regex related issues later.
        input = input.Replace("\n", String.Empty);

        Part1(input);
        Part2(input);
    }

    private static void Part1(string input)
    {
        // Find all possible "mul(123,456)" strings in the text, using regex.
        var regexPattern = "mul\\([0-9]{1,3}\\,[0-9]{1,3}\\)";
        Regex regex = new(regexPattern);

        var muls = regex.Matches(input);

        var mulsAsStrings = muls.Select(m => m.Value);

        int totalMultiply = 0;

        var numberRegexPattern = "[0-9]{1,3}";
        Regex numberRegex = new(numberRegexPattern);

        foreach (string mulFunction in mulsAsStrings)
        {
            var numbers = numberRegex.Matches(mulFunction);
            if (numbers.Count != 2)
            {
                throw new Exception("Two numbers expected");
            }

            var parsedNumbers = numbers.Select(m => m.Value).Select(int.Parse);

            totalMultiply += (parsedNumbers.First() * parsedNumbers.Skip(1).First());
        }

        Console.WriteLine("Result of day 3, part 1: ");
        Console.WriteLine(totalMultiply);
    }

    private static void Part2(string input)
    {
        // Remove everything between don't and do.
        var inputWithRemovedParts = RemoveBetweenTags(input, "don't()", "do()");

        // Remove everything after the last do.
        var correctInput = inputWithRemovedParts.Split("don't")[0];

        // Find all possible "mul(123,456)" strings in the text, using regex.
        var regexPattern = "mul\\([0-9]{1,3}\\,[0-9]{1,3}\\)";
        Regex regex = new(regexPattern);

        var muls = regex.Matches(correctInput);

        int totalMultiply = 0;

        var numberRegexPattern = "[0-9]{1,3}";
        Regex numberRegex = new(numberRegexPattern);

        foreach (string mulFunction in muls.Select(m => m.Value))
        {
            var numbers = numberRegex.Matches(mulFunction);
            if (numbers.Count != 2)
            {
                throw new Exception("Two numbers expected");
            }

            var parsedNumbers = numbers.Select(m => m.Value).Select(int.Parse);

            totalMultiply += (parsedNumbers.First() * parsedNumbers.Skip(1).First());
        }

        Console.WriteLine("Result of day 3, part 2: ");
        Console.WriteLine(totalMultiply);
    }

    private static string RemoveBetweenTags(string original, string firstTag, string secondTag)
    {
        string pattern = Regex.Escape(firstTag) + "(.*?)" + Regex.Escape(secondTag);
        Regex regex = new(pattern);

        foreach (Match match in regex.Matches(original))
        {
            var lengthTest = original.Length;
            original = original.Replace(match.Value, string.Empty);
            var lengthTest2 = original.Length;

            if (lengthTest2 >= lengthTest)
            {
                throw new Exception("Not working as intended");
            }
        }

        return original;
    }
}
