namespace adventofcode2024;
internal class Day1
{
    private readonly IList<int> LeftList;
    private readonly IList<int> RightList;

    public Day1()
    {
        const string localCachePath = "./data/day1_input";
        string workingDirectory = Environment.CurrentDirectory;

        string projectDirectory = Directory.GetParent(workingDirectory)?.Parent?.Parent?.FullName ?? throw new NullReferenceException("Can't find project dir.");
        string listPath = Path.Combine(projectDirectory, localCachePath);

        (LeftList, RightList) = GetLists(listPath);
    }

    public void Run()
    {
        Part1();
        Part2();
    }

    private void Part1()
    {
        int totalDistance = 0;

        List<int> leftListCopy = new(LeftList);
        List<int> rightListCopy = new(RightList);

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

    private void Part2()
    {
        int similarityScore = 0;

        foreach (var leftListItem in LeftList)
        {
            var count = RightList.Count(rightListItem => rightListItem == leftListItem);
            var score = count * leftListItem;
            similarityScore += score;
        }

        Console.WriteLine("Result for day 1, part 2:");
        Console.WriteLine(similarityScore);
    }

    private static (IList<int> leftList, IList<int> rightList) GetLists(string listPath)
    {
        string input = File.ReadAllText(listPath);

        List<int> leftList = [];
        List<int> rightList = [];

        using StringReader reader = new(input);
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            leftList.Add(Int32.Parse(line.Substring(0, 5)));
            rightList.Add(Int32.Parse(line.Substring(8, 5)));
        }

        return (leftList, rightList);
    }

    private static int FindSmallestNumber(IList<int> list)
        => list.Min();
}
