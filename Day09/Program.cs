using Position = (int x, int y);

var lines = File.ReadAllLines("input.txt");
HashSet<Position> red = [];
HashSet<Position> green = [];
HashSet<Position> gray = [];
HashSet<Position> redcorner = [];
foreach (var line in lines)
{
    var parts = line.Split(',');
    var pos = (int.Parse(parts[0]), int.Parse(parts[1]));
    red.Add(pos);
}

var maxY = red.MaxBy(p => p.y).y + 2;
var maxX = red.MaxBy(p => p.x).x + 2;
long part1 = 0;
foreach (var posA in red)
{
    foreach (var posB in red)
    {
        long area = Multiply(posA, posB);
        if (area > part1)
            part1 = area;
        if (posA.x == posB.x)
        {
            redcorner.Add(posA);
            redcorner.Add(posB);
            green.Add(posA);
            green.Add(posB);
            for (int y = Math.Min(posA.y, posB.y); y < Math.Max(posA.y, posB.y); y++)
                green.Add((posA.x, y));
        }

        if (posA.y == posB.y)
        {
            redcorner.Add(posA);
            redcorner.Add(posB);
            green.Add(posA);
            green.Add(posB);
            for (int x = Math.Min(posA.x, posB.x); x < Math.Max(posA.x, posB.x); x++)
                green.Add((x, posA.y));
        }
    }
}

Console.WriteLine($"Part 1: {part1}");

var topCorner = redcorner.OrderBy(p => p.x).MinBy(p => p.y);

PaintGreenTiles((topCorner.x + 1, topCorner.y + 1));

Console.WriteLine("Done painting");

long part2 = 0;

foreach (var posA in redcorner)
{
    foreach (var posB in redcorner)
    {
        if (posA == posB)
            continue;
        if (PointsInArea(posA, posB).Any(p => !green.Contains(p)))
            continue;
        long area = Multiply(posA, posB);
        if (area > part2)
            part2 = area;
    }
}

Console.WriteLine($"Part 2: {part2}");

long Multiply(Position posA, Position posB) =>
    ((long)(Math.Abs(posA.x - posB.x) + 1)) * ((long)(Math.Abs(posA.y - posB.y) + 1));

void PaintGreenTiles(Position position)
{
    Queue<Position> queue = [];
    queue.Enqueue(position);
    while (queue.Count > 0)
    {
        var pos = queue.Dequeue();
        green.Add(pos);
        foreach (var p in Neighbours(pos))
        {
            if (green.Contains(p))
                continue;
            queue.Enqueue(p);
        }
    }
}

static IEnumerable<Position> PointsInArea(Position posA, Position posB)
{
    for (int x = Math.Min(posA.x, posB.x); x < Math.Max(posA.x, posB.x); x++)
    {
        for (int y = Math.Min(posA.y, posB.y); y < Math.Max(posA.y, posB.y); y++)
        {
            yield return (x, y);
        }
    }
}

IEnumerable<Position> Neighbours(Position pos)
{
    Position[] rels = [(1, 0), (0, 1), (-1, 0), (0, -1)];
    foreach (var (x, y) in rels)
    {
        Position npos = (pos.x + x, pos.y + y);
        if (npos.x >= 0 && npos.x <= maxX && npos.y >= 0 && npos.y <= maxY)
            yield return npos;
    }
}
