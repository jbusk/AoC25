var lines = File.ReadAllLines("input.txt");
Dictionary<(int x, int y), long> cache = [];
int part1 = 0;
Console.WriteLine("Part 2: " + RecurseManifold(2, lines[0].IndexOf('S')));
Console.WriteLine("Part 1: " + part1);

long RecurseManifold(int x, int y)
{
    if (cache.TryGetValue((x, y), out var value))
        return value;
    if (x == lines.Length)
        return 1L;
    long retval;
    if (lines[x][y] is '^') // splitter
    {
        part1++;
        retval = RecurseManifold(x + 2, y - 1) + RecurseManifold(x + 2, y + 1);
    }
    else
        retval = RecurseManifold(x + 2, y);

    cache.Add((x, y), retval);
    return retval;
}