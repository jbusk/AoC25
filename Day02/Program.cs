var ranges = File.ReadAllText("input.txt").Split(',').Select((x => x.Split('-')));
(long sumpart1, long sumpart2) = (0, 0);

foreach (var range in ranges)
{
    for (long i = long.Parse(range[0]); i <= long.Parse(range[1]); i++)
    {
        string currentId = i.ToString();
        var idLength = (int)Math.Floor(Math.Log10(i)) + 1;
        var halfLength = idLength / 2;
        if (currentId[..halfLength] == currentId[halfLength..])
            sumpart1 += i;

        for (int substringLength = 1; substringLength <= halfLength; substringLength++)
        {
            var chunks = Enumerable.Range(0, idLength / substringLength).Select(chunksize => currentId.Substring(chunksize * substringLength, substringLength)).ToList();
            if (chunks.All(x => x == chunks.First()) && idLength == chunks.Count * chunks.First().Length)
            {
                sumpart2 += i;
                break;
            }
        }
    }
}

Console.WriteLine($"Part 1: {sumpart1}");
Console.WriteLine($"Part 2: {sumpart2}");
