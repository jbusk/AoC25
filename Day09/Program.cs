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

Console.WriteLine($"{maxX} {maxY}");

//CreateBitmap("01-begin.bmp");

var topCorner = redcorner.OrderBy(p => p.x).MinBy(p => p.y);
//Visualise(topCorner);
//Visualise((topCorner.x +1, topCorner.y +1));
// Recurse((0, 0));
//PaintGrayTiles((0, 0));

PaintGreenTiles((topCorner.x +1, topCorner.y +1));
//Visualise();

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
// Visualise();

// void CreateBitmap(string filename) {
//     RawBitMap bmp = new(maxX, maxY);
//     var red = new RawColor(255,0,0);
//     var green = new RawColor(0,255,0);
//     var gray = new RawColor(100,100,100);
//     for (int x = 0; x < maxX; x++)
//     {
//         for (int y = 0; y < maxY; y++)
//         {
// 	    if (red.Contains(new Position(x, y))) 
// 		bmp.SetPixel(x,y, red);
//             else if (green.Contains(new Position(x, y))) 
// 		bmp.SetPixel(x,y, green);
//             else
// 		bmp.SetPixel(x,y, gray);
//         }
//     }
//     bmp.Save(filename);
// }

void Visualise(Position? mark = null)
{
    for (int x = 0; x < maxX; x++)
    {
        for (int y = 0; y < maxY; y++)
        {
	    if (mark is not null && mark == (x,y))
		Console.Write('¤');
            else if (red.Contains(new Position(x, y)))
                Console.Write('#');
            else if (green.Contains(new Position(x, y)))
                Console.Write('X');
            else if (gray.Contains(new Position(x, y)))
                Console.Write('.');
            else
                Console.Write(' ');
        }

        Console.WriteLine();
    }
}

long Multiply(Position posA, Position posB) =>
    ((long)(Math.Abs(posA.x - posB.x) + 1)) * ((long)(Math.Abs(posA.y - posB.y) + 1));


void Recurse(Position pos)
{
    if (red.Contains(pos) || green.Contains(pos) || gray.Contains(pos))
        return;
    gray.Add(pos);
    foreach (var npos in Neighbours(pos))
    {
        Recurse(npos);
    }
}

void PaintGreenTiles(Position position)
{
    Queue<Position> queue = [];
    queue.Enqueue(position);
    while (queue.Count > 0)
    {
        var pos = queue.Dequeue();
        green.Add(pos);
        foreach (var p in Neighbours(pos)) {
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

// public struct RawColor
// {
//     public readonly byte R, G, B;
//     
//     public RawColor(byte r, byte g, byte b)
//     {
// 	(R, G, B) = (r, g, b);
//     }
//     
//     public static RawColor Random(Random rand)
//     {
// 	byte r = (byte)rand.Next(256);
// 	byte g = (byte)rand.Next(256);
// 	byte b = (byte)rand.Next(256);
// 	return new RawColor(r, g, b);
//     }
//     
//     public static RawColor Gray(byte value)
//     {
// 	return new RawColor(value, value, value);
//     }
// }
// 
// 
// public class RawBitmap
// {
//     public readonly int Width;
//     public readonly int Height;
//     private readonly byte[] ImageBytes;
// 
//     public RawBitmap(int width, int height)
//     {
// 	Width = width;
// 	Height = height;
// 	ImageBytes = new byte[width * height * 4];
//     }
// 
//     public void SetPixel(int x, int y, RawColor color)
//     {
// 	int offset = ((Height - y - 1) * Width + x) * 4;
// 	ImageBytes[offset + 0] = color.B;
// 	ImageBytes[offset + 1] = color.G;
// 	ImageBytes[offset + 2] = color.R;
//     }
// 
//     public byte[] GetBitmapBytes()
//     {
// 	const int imageHeaderSize = 54;
// 	byte[] bmpBytes = new byte[ImageBytes.Length + imageHeaderSize];
// 	bmpBytes[0] = (byte)'B';
// 	bmpBytes[1] = (byte)'M';
// 	bmpBytes[14] = 40;
// 	Array.Copy(BitConverter.GetBytes(bmpBytes.Length), 0, bmpBytes, 2, 4);
// 	Array.Copy(BitConverter.GetBytes(imageHeaderSize), 0, bmpBytes, 10, 4);
// 	Array.Copy(BitConverter.GetBytes(Width), 0, bmpBytes, 18, 4);
// 	Array.Copy(BitConverter.GetBytes(Height), 0, bmpBytes, 22, 4);
// 	Array.Copy(BitConverter.GetBytes(32), 0, bmpBytes, 28, 2);
// 	Array.Copy(BitConverter.GetBytes(ImageBytes.Length), 0, bmpBytes, 34, 4);
// 	Array.Copy(ImageBytes, 0, bmpBytes, imageHeaderSize, ImageBytes.Length);
// 	return bmpBytes;
//     }
// 
//     public void Save(string filename)
//     {
// 	byte[] bytes = GetBitmapBytes();
// 	File.WriteAllBytes(filename, bytes);
//     }
// 
// 
// 
// }
