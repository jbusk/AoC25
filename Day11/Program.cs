var lines = File.ReadAllLines("input.txt").Select(x => x.Split(": "));
var nodes = new Dictionary<string, string[]>();

foreach (var line in lines)
{
    nodes[line[0]] = line[1].Split(' ');
}

Console.WriteLine("Part 1: " + Recurse("you", "out"));

int Recurse(string current, string goal)
{
    if (current == goal)
        return 1;
    int paths = 0;
    foreach (var node in nodes[current])
    {
        paths += Recurse(node, goal);
    }
    return paths;
}

//
// int Recurse(string current, string goal)
// {
//     if (current == goal)
//         return 1;
//     int paths = 0;
//     foreach (var node in nodes[current])
//     {
//         paths += Recurse(node, goal);
//     }
//     return paths;
// }