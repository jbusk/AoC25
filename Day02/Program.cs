var lines = File.ReadAllText("input.txt").Split(',').Select((x => x.Split('-')));
(ulong sumpart1, ulong sumpart2) = (0, 0);

foreach (var line in lines)
{
    var from = ulong.Parse(line[0]);
    var to = ulong.Parse(line[1]);

    List<ulong> pt2matches = [];
    for (ulong i = from; i <= to; i++)
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
                pt2matches.Add(i);
            }
        }
    }

    foreach (var match in pt2matches.Distinct())
        sumpart2 += match;
}

Console.WriteLine($"Part 1: {sumpart1}");
Console.WriteLine($"Part 2: {sumpart2}");

static IEnumerable<string> Split(string str, int chunkSize)
{
    return Enumerable.Range(0, str.Length / chunkSize)
        .Select(i => str.Substring(i * chunkSize, chunkSize));
}