var lines = File.ReadAllLines("input.txt");
var dial = 50; // start position
Func<int, int> tick_lt = (x) => x - 1;
Func<int, int> tick_rt = (x) => x + 1;
var sumpart1 = 0;
var sumpart2 = 0;
foreach (var line in lines)
{
    var amount = int.Parse(line[1..]);
    var tick = tick_lt;
    if (line[0] == 'R')
        tick = tick_rt;

    for (var i = 0; i < amount; i++)
    {
        dial = tick(dial);
        if (dial % 100 == 0)
            sumpart2++;
    }

    if (dial % 100 == 0)
        sumpart1++;
}

Console.WriteLine("Part 1: " + sumpart1);
Console.WriteLine("Part 2: " + sumpart2);