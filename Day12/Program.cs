var file = File.ReadAllText("input.txt").Split(Environment.NewLine + Environment.NewLine);
// Doing a check to see if just a 3x3 fits for most first
int sumpart1 = 0;
var regions = file[^1].Split(Environment.NewLine);
foreach (var region in regions)
{
    var split = region.Split(": ");
    var dims = split[0].Split('x');
    var width = int.Parse(dims[0]);
    var height = int.Parse(dims[1]);
    // Zero indexed number of presents
    var total = split[1].Split(' ').Select(int.Parse).Sum();

    Console.Write($"{region} - {width}x{height} = {width * height} total = {total*9}");
    if ((width * height) >= total * 9)
    {
        Console.Write(" fits!");
        sumpart1++;
    }

    Console.WriteLine();
}

Console.WriteLine($"Part 1: {sumpart1}");

// Ok, it seems like the ones where it doesn't fit it's a lot too large to be able to be packed into, so...