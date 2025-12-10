using System.Collections.Concurrent;
using Position = (int x, int y);

var lines = File.ReadAllLines("input.txt");
HashSet<Position> redtiles = [];
HashSet<Position> corners = [];
HashSet<Position> green = [];
HashSet<Position> gray = [];

foreach (var line in lines)
{
    var parts = line.Split(',');
    var pos = (int.Parse(parts[0]), int.Parse(parts[1]));
    redtiles.Add(pos);
}

var highestY = redtiles.MaxBy(p => p.y).y + 2;
var highestX = redtiles.MaxBy(p => p.x).x + 2;
var lowestY = redtiles.MinBy(p => p.y).y - 2;
var lowestX = redtiles.MinBy(p => p.x).x - 2;
long part1 = 0;
foreach (var posA in redtiles)
{
    foreach (var posB in redtiles)
    {
        long area = Multiply(posA, posB);
        if (area > part1)
            part1 = area;
        if (posA.x == posB.x) // horizontal edge
        {
            corners.Add(posA);
            corners.Add(posB);
            for (int y = Math.Min(posA.y, posB.y); y <= Math.Max(posA.y, posB.y); y++)
                green.Add((posA.x, y));
        }

        if (posA.y == posB.y) // vertical edge
        {
            corners.Add(posA);
            corners.Add(posB);
            for (int x = Math.Min(posA.x, posB.x); x <= Math.Max(posA.x, posB.x); x++)
                green.Add((x, posA.y));
        }
    }
}

Console.WriteLine($"Part 1: {part1}");

var topgreen = green.MinBy(p => p.y);
PaintGrey((topgreen.x, topgreen.y - 1));

long part2 = 0;
Position largeA = (0, 0);
Position largeB = (0, 0);

foreach (var posA in corners)
{
    ConcurrentBag<long> results = [];
    ConcurrentBag<(int, int, int, int)> seen = [];
    Parallel.ForEach(corners, posB =>
    {
        var (minx, maxx) = (Math.Min(posA.x, posB.x), Math.Max(posA.x, posB.x));
        var (miny, maxy) = (Math.Min(posA.y, posB.y), Math.Max(posA.y, posB.y));
        var allgreen = true;
        if (seen.Contains((minx, miny, maxx, maxy)))
            return;
        seen.Add((minx, miny, maxx, maxy));
        for (int x = minx; x < maxx; x++)
        {
            if (!gray.Contains((x, miny)) && !gray.Contains((x, maxy))) continue;
            allgreen = false;
            break;
        }

        for (int y = miny; y < maxy; y++)
        {
            if (!gray.Contains((minx, y)) && !gray.Contains((maxx, y))) continue;
            allgreen = false;
            break;
        }

        if (allgreen)
            results.Add(Multiply(posA, posB));
    });

    if (results.Count > 0)
    {
        long maxResult = results.Max();
        if (maxResult > part2)
            part2 = maxResult;
    }
}

Console.WriteLine($"Part 2: {part2}");

long Multiply(Position posA, Position posB) =>
    ((long)(Math.Abs(posA.x - posB.x) + 1)) * ((long)(Math.Abs(posA.y - posB.y) + 1));

void PaintGrey(Position position)
{
    Queue<Position> queue = new Queue<Position>();
    queue.Enqueue(position);
    while (queue.Count > 0)
    {
        var pos = queue.Dequeue();
        if (gray.Contains(pos))
            continue;
        if (green.Contains(pos))
            continue;
        var neighbours = Neighbours(pos).ToList();
        if (!neighbours.Any(p => green.Contains(p)))
            continue;
        gray.Add(pos);
        foreach (var npos in neighbours)
            queue.Enqueue(npos);
    }
}

static IEnumerable<Position> Neighbours(Position pos)
{
    Position[] rels = //[(1, 0), (0, 1), (-1, 0), (0, -1)];
    [
        (0, 1), (1, 0), (0, -1), (-1, 0),
        (1, 1), (-1, 1), (1, -1), (-1, -1)
    ];
    foreach (var (x, y) in rels)
        yield return (pos.x + x, pos.y + y);
}