using Position = (int x, int y);

var lines = File.ReadAllLines("input.txt");
var (part2, max) = (0, lines.Length);
HashSet<Position> grid = [];
for (int y = 0; y < max; y++)
{
    for (int x = 0; x < max; x++)
    {
        Position position = new(x, y);
        if (lines[x][y] == '@')
            grid.Add(position);
    }
}

bool once = true;
while (true)
{
    var accessable = grid.Where(Acessible).ToHashSet();
    var count = accessable.Count();
    if (count == 0)
        break;
    part2 += count;
    if (once)
    {
        Console.WriteLine("Part 1: " + count);
        once = false;
    }
    grid.RemoveWhere(x => accessable.Contains(x));
}

Console.WriteLine("Part 2: " + part2);

bool Acessible(Position pos)
{
    int count = 0;
    Position[] relPos =
    [
        (0, 1), (1, 0), (0, -1), (-1, 0),
        (1, 1), (-1, 1), (1, -1), (-1, -1)
    ];
    foreach (var rel in relPos)
    {
        Position newPos = (pos.x + rel.x, pos.y + rel.y);
        if (grid.Contains(newPos))
            count++;
        if (count == 4)
            return false;
    }

    return true;
}
