namespace adventofcode2024.Days;

internal static class Day2
{
    public static void Run()
    {
        var input = Helpers.GetFileAsString("day2_input");
        var lists = ParseInput(input);

        Part1(lists);
        Part2(lists);
    }

    private static void Part1(IList<IEnumerable<int>> lists)
    {
        int safeListCount = 0;

        foreach (var numberList in lists)
        {
            if (IsListSafe(numberList))
                safeListCount++;
        }

        Console.WriteLine("Result of day 2, part 1: ");
        Console.WriteLine(safeListCount);
    }

    private static void Part2(IList<IEnumerable<int>> lists)
    {
        int safeListCount = 0;

        foreach (var numberList in lists)
        {
            if (IsListSafe(numberList))
            {
                safeListCount++;
                continue;
            }

            bool isSafeIfANumberIsRemoved = false;

            // Try removing an item from the list, for every item.
            for (int i = 0; i < numberList.Count(); i++)
            {
                var numberListWithoutItem = numberList.ToList();
                numberListWithoutItem.RemoveAt(i);
                if (IsListSafe(numberListWithoutItem))
                {
                    isSafeIfANumberIsRemoved = true;
                }
            }

            if (isSafeIfANumberIsRemoved)
            {
                safeListCount++;
            }
        }

        Console.WriteLine("Result of day 2, part 2: ");
        Console.WriteLine(safeListCount);
    }

    private static bool IsListSafe(IEnumerable<int> numbers)
    {
        var isAlwaysIncreasing = IsAlwaysIncreasing(numbers);
        var isAlwaysDecreasing = IsAlwaysDecreasing(numbers);
        var isDifferenceSafe = IsDifferenceSafe(numbers);

        return (isAlwaysDecreasing || isAlwaysIncreasing) && isDifferenceSafe;
    }

    private static List<IEnumerable<int>> ParseInput(string input)
    {
        var listOfLists = new List<IEnumerable<int>>();

        using StringReader reader = new(input);
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            var numbers = Array.ConvertAll(line.Split(' '), int.Parse);
            listOfLists.Add(numbers);
        }

        return listOfLists;
    }

    private static bool IsAlwaysIncreasing(IEnumerable<int> numbers)
    {
        int? previousValue = null;
        bool isAlwaysIncreasing = true;

        foreach (var number in numbers)
        {
            if (previousValue == null)
            {
                previousValue = number;
                continue;
            }

            var isIncreasing = number > previousValue;
            if (!isIncreasing)
            {
                isAlwaysIncreasing = false;
                break;
            }

            previousValue = number;
        }

        return isAlwaysIncreasing;
    }

    private static bool IsAlwaysDecreasing(IEnumerable<int> numbers)
    {
        int? previousValue = null;
        bool isAlwaysDecreasing = true;

        foreach (var number in numbers)
        {
            if (previousValue == null)
            {
                previousValue = number;
                continue;
            }

            var isDecreasing = number < previousValue;
            if (!isDecreasing)
            {
                isAlwaysDecreasing = false;
                break;
            }

            previousValue = number;
        }

        return isAlwaysDecreasing;
    }

    private static bool IsDifferenceSafe(IEnumerable<int> numbers)
    {
        int? previousValue = null;
        bool isDifferenceSafe = true;

        foreach (var number in numbers)
        {
            if (previousValue == null)
            {
                previousValue = number;
                continue;
            }

            var difference = Math.Abs(number - previousValue.Value);

            if (difference >= 1 && difference <= 3)
            {
                previousValue = number;
                continue;
            }

            isDifferenceSafe = false;
            break;
        }

        return isDifferenceSafe;
    }
}