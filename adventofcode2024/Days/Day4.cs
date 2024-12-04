namespace adventofcode2024.Days;

internal static class Day4
{
    public static void Run()
    {
        var input = Helpers.GetFileAsString("day4_input");

        Part1(input);
        Part2(input);
    }

    private static void Part1(string input)
    {
        char[][] puzzle = ConvertListToGrid(input);

        const string wordToFind = "XMAS";

        var result = FindWord(puzzle, wordToFind);

        Console.WriteLine("Result of day 4, part 1: ");
        Console.WriteLine(result);
    }

    private static void Part2(string input)
    {
        char[][] puzzle = ConvertListToGrid(input);

        int answerCount = 0;

        var height = puzzle.Length;
        var width = puzzle[0].Length;

        for (int yCoord = 0; yCoord < height; yCoord++)
        {
            for (int xCoord = 0; xCoord < width; xCoord++)
            {
                if (SearchForTheXDashMAS(puzzle, xCoord, yCoord))
                {
                    answerCount++;
                }
            }
        }

        Console.WriteLine("Result of day 4, part 2: ");
        Console.WriteLine(answerCount);
    }

    private static char[][] ConvertListToGrid(string input)
    {
        List<char[]> chars = [];

        using StringReader reader = new(input);
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            chars.Add(line.ToCharArray());
        }

        return [.. chars];
    }

    // Credits to this tutorial: https://www.geeksforgeeks.org/search-a-word-in-a-2d-grid-of-characters/
    // For part 1
    private static int FindWord(char[][] grid, string word)
    {
        var height = grid.Length;
        var width = grid[0].Length;

        int answerCount = 0;

        for (int yCoord = 0; yCoord < height; yCoord++)
        {
            for (int xCoord = 0; xCoord < width; xCoord++)
            {
                answerCount += Search2DGrid(grid, xCoord, yCoord, word);
            }
        }

        return answerCount;
    }

    // This returns the amount of possible answers from a given X-Y coordinate.
    // This can range from 0 to 8.
    // For part 1.
    private static int Search2DGrid(char[][] grid, int row, int column, string word)
    {
        int answerCount = 0;

        var maxX = grid.Length;
        var maxY = grid[0].Length;

        // If the first coordinate does not match the first letter, we can immediately return false.
        if (grid[row][column] != word[0])
        {
            return answerCount;
        }

        var wordLength = word.Length;

        int[] xMovements = [-1, -1, -1, 0, 0, 1, 1, 1];
        int[] yMovements = [-1, 0, 1, -1, 1, -1, 0, 1];

        // Search for all 8 possible directions.
        for (var direction = 0; direction < 8; direction++)
        {
            int currentCharacter;
            var currentXPosition = row + xMovements[direction];
            var currentYPosition = column + yMovements[direction];

            for (currentCharacter = 1; currentCharacter < wordLength; currentCharacter++)
            {
                if (currentXPosition >= maxX
                    || currentXPosition < 0
                    || currentYPosition >= maxY
                    || currentYPosition < 0)
                {
                    // we're out of bounds.
                    break;
                }

                if (grid[currentXPosition][currentYPosition] != word[currentCharacter])
                {
                    // letter does not match
                    break;
                }

                // If we are here, the letter does match.
                currentXPosition += xMovements[direction];
                currentYPosition += yMovements[direction];
            }

            if (currentCharacter == wordLength)
            {
                // We found the word 🥳
                answerCount++;
            }
        }

        return answerCount;
    }

    // For part 2
    private static bool SearchForTheXDashMAS(char[][] grid, int row, int column)
    {
        // We'll start at the A this time.

        var maxX = grid.Length;
        var maxY = grid[0].Length;

        // If the first coordinate does not match the first letter, we can immediately return false.
        if (grid[row][column] != char.Parse("A"))
        {
            return false;
        }

        // Prevent out of bounds
        // If one of the letters is out of bounds, we can't make the X-MAS!
        if (row - 1 >= maxX || row - 1 < 0 || column - 1 >= maxY || column - 1 < 0
            || row + 1 >= maxX || row + 1 < 0 || column + 1 >= maxY || column + 1 < 0)
        {
            return false;
        }

        (int row, int column) topLeft = (row - 1, column - 1);
        (int row, int column) topRight = (row - 1, column + 1);
        (int row, int column) bottomLeft = (row + 1, column - 1);
        (int row, int column) bottomRight = (row + 1, column + 1);

        // Here, I basically brute-force the 4 possible options.

        // M.S
        // .A.
        // M.S
        if (grid[topLeft.row][topLeft.column] == char.Parse("M")
            && grid[topRight.row][topRight.column] == char.Parse("S")
            && grid[bottomLeft.row][bottomLeft.column] == char.Parse("M")
            && grid[bottomRight.row][bottomRight.column] == char.Parse("S"))
        {
            return true;
        }

        // S.M
        // .A.
        // S.M
        if (grid[topLeft.row][topLeft.column] == char.Parse("S")
            && grid[topRight.row][topRight.column] == char.Parse("M")
            && grid[bottomLeft.row][bottomLeft.column] == char.Parse("S")
            && grid[bottomRight.row][bottomRight.column] == char.Parse("M"))
        {
            return true;
        }

        // M.M
        // .A.
        // S.S
        if (grid[topLeft.row][topLeft.column] == char.Parse("M")
            && grid[topRight.row][topRight.column] == char.Parse("M")
            && grid[bottomLeft.row][bottomLeft.column] == char.Parse("S")
            && grid[bottomRight.row][bottomRight.column] == char.Parse("S"))
        {
            return true;
        }

        // S.S
        // .A.
        // M.M
        if (grid[topLeft.row][topLeft.column] == char.Parse("S")
            && grid[topRight.row][topRight.column] == char.Parse("S")
            && grid[bottomLeft.row][bottomLeft.column] == char.Parse("M")
            && grid[bottomRight.row][bottomRight.column] == char.Parse("M"))
        {
            return true;
        }

        return false;
    }
}