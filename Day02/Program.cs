var lines = File.ReadAllText("input.txt").Split(',').Select((x => x.Split('-')));
(long sumpart1, long sumpart2) = (0, 0);

foreach (var line in lines)
{
    for (long i = long.Parse(line[0]); i <= long.Parse(line[1]); i++)
    {
        string id = i.ToString();
        for (int j = 1; j <= id.Length / 2; j++)
        {
            var chunks = Split(id, j).ToList();
            bool invalid = chunks.All(x => x == chunks.First());
            if (invalid && id.Length == chunks.Count * chunks.First().Length)
            {
                if (chunks.Count == 2)
                    sumpart1 += i;
                sumpart2 += i;
                break;
            }
        }
    }
}

Console.WriteLine($"Part 1: {sumpart1}");
Console.WriteLine($"Part 2: {sumpart2}");
IEnumerable<string> Split(string str, int chunkSize)
{
    return Enumerable.Range(0, str.Length / chunkSize)
        .Select(i => str.Substring(i * chunkSize, chunkSize));
}