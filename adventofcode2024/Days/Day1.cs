namespace adventofcode2024.Days;
internal static class Day1
{
    public static void Run()
    {
        const string inputPath = "day1_input";
        var (leftList, rightList) = GetLists(inputPath);

        Part1(leftList, rightList);
        Part2(leftList, rightList);
    }

    private static void Part1(IList<int> leftList, IList<int> rightList)
    {
        int totalDistance = 0;

        List<int> leftListCopy = new(leftList);
        List<int> rightListCopy = new(rightList);

        while (leftListCopy.Count > 0 & rightListCopy.Count > 0)
        {
            var smallestLeftNumber = FindSmallestNumber(leftListCopy);
            var smallestRightNumber = FindSmallestNumber(rightListCopy);

            var distance = Math.Abs(smallestLeftNumber - smallestRightNumber);

            leftListCopy.Remove(smallestLeftNumber);
            rightListCopy.Remove(smallestRightNumber);
            totalDistance += distance;
        }

        Console.WriteLine("Result for day 1, part 1:");
        Console.WriteLine(totalDistance);
    }

    private static void Part2(IList<int> leftList, IList<int> rightList)
    {
        int similarityScore = 0;

        foreach (var leftListItem in leftList)
        {
            var count = rightList.Count(rightListItem => rightListItem == leftListItem);
            var score = count * leftListItem;
            similarityScore += score;
        }

        Console.WriteLine("Result for day 1, part 2:");
        Console.WriteLine(similarityScore);
    }

    private static (IList<int> leftList, IList<int> rightList) GetLists(string fileName)
    {
        var input = Helpers.GetFileAsString(fileName);

        List<int> leftList = [];
        List<int> rightList = [];

        using StringReader reader = new(input);
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            leftList.Add(int.Parse(line.Substring(0, 5)));
            rightList.Add(int.Parse(line.Substring(8, 5)));
        }

        return (leftList, rightList);
    }

    private static int FindSmallestNumber(IList<int> list)
        => list.Min();
}
