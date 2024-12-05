using System.Text.RegularExpressions;

namespace adventofcode2024.Days;

internal sealed class Day5
{
    private record Rule(int Before, int After);

    private readonly HashSet<Rule> rules;
    private readonly HashSet<IList<int>> sequences;

    public Day5()
    {
        var input = Helpers.GetFileAsString("day5_input");

        rules = ParseRules(input);
        sequences = ParseSequences(input);
    }

    public void Run()
    {
        Part1();
        Part2();
    }

    private void Part1()
    {
        var validSequences = new List<IList<int>>();

        foreach (var sequence in sequences)
        {
            if (CheckRules(sequence))
            {
                validSequences.Add(sequence);
            }
        }

        var middleNumbersAdded = 0;
        foreach (var sequence in validSequences)
        {
            middleNumbersAdded += FindMiddleElement(sequence);
        }

        Console.WriteLine("Result of day 5, part 1: ");
        Console.WriteLine(middleNumbersAdded);
    }

    private void Part2()
    {
        var invalidSequences = new List<IList<int>>();

        foreach (var sequence in sequences)
        {
            if (!CheckRules(sequence))
            {
                invalidSequences.Add(sequence);
            }
        }

        var fixedSequences = new List<IList<int>>();
        foreach (var sequence in invalidSequences)
        {
            fixedSequences.Add(ApplyRules(sequence));
        }

        var middleNumbersAdded = 0;
        foreach (var sequence in fixedSequences)
        {
            middleNumbersAdded += FindMiddleElement(sequence);
        }

        Console.WriteLine("Result of day 5, part 2: ");
        Console.WriteLine(middleNumbersAdded);
    }

    private static HashSet<Rule> ParseRules(string input)
    {
        HashSet<Rule> rules = [];

        var findRulesRegex = new Regex("[1-9]{2}\\|[1-9]{2}", RegexOptions.Multiline);

        foreach (Match match in findRulesRegex.Matches(input))
        {
            string value = match.Value;
            var numbers = value.Split('|').Select(int.Parse);
            rules.Add(new(numbers.ElementAt(0), numbers.ElementAt(1)));
        }

        return rules;
    }

    private static HashSet<IList<int>> ParseSequences(string input)
    {
        HashSet<IList<int>> sequences = [];

        var findSequencesRegex = new Regex(@"(?m)(^[1-9]{2}(,[1-9]{2})*)\r?$", RegexOptions.Multiline);

        foreach (Match match in findSequencesRegex.Matches(input))
        {
            string value = match.Value;
            var numbers = value.Split(',').Select(int.Parse);
            sequences.Add(numbers.ToList());
        }

        return sequences;
    }

    private bool CheckRules(IList<int> sequence)
    {
        foreach (var rule in rules)
        {
            var indexBefore = sequence.IndexOf(rule.Before);
            var indexAfter = sequence.IndexOf(rule.After);

            // If one of these pages don't exist, the rule is technically valid.
            if (indexBefore == -1 || indexAfter == -1)
            {
                continue;
            }

            // If the rule is valid, go on.
            if (indexBefore < indexAfter)
            {
                continue;
            }

            return false;
        }

        return true;
    }

    private List<int> ApplyRules(IList<int> sequence)
    {
        List<int> orderedList = [.. sequence];

        orderedList.Sort((first, second) =>
        {
            if (rules.FirstOrDefault(rule => rule.Before == first && rule.After == second) != null)
            {
                return -1;
            }

            if (rules.FirstOrDefault(rule => rule.Before == second && rule.After == first) != null)
            {
                return 1;
            }

            return 0;
        });


        if (!CheckRules(orderedList))
        {
            throw new Exception("Sorting failed!");
        }

        return orderedList;
    }

    private static T FindMiddleElement<T>(IList<T> list)
    {
        if (list.Count % 2 == 0)
        {
            throw new Exception("We can't handle even lists!!!");
        }

        return list[list.Count / 2];
    }
}
