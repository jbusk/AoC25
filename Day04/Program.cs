using Position = (int x, int y);

var lines = File.ReadAllLines("input.txt");
var max = lines.Length;
HashSet<Position> grid = [];
for (int y = 0; y < max; y++)
for (int x = 0; x < max; x++)
    if (lines[x][y] == '@')
        grid.Add(new(x, y));

Console.WriteLine("Part 1: " + grid.Count(Accessible));
var total = grid.Count();
int its = 0;
var (last, current) = (total, 0);
while (true)
{
    its++;
    grid.RemoveWhere(Accessible);
    current = grid.Count();
    if (last == current)
        break;
    last = current;
}

Console.WriteLine(its);
Console.WriteLine("Part 2: " + (total - last));

bool Accessible(Position pos)
{
    int count = 0;
    Position[] relPos =
    [
        (0, 1), (1, 0), (0, -1), (-1, 0),
        (1, 1), (-1, 1), (1, -1), (-1, -1)
    ];
    foreach (var rel in relPos)
    {
        if (grid.Contains((pos.x + rel.x, pos.y + rel.y)))
            count++;
        if (count == 4)
            return false;
    }

    return true;
}