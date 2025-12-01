var lines = File.ReadAllLines("input.txt");
(int dial, int sumpart1, int sumpart2)  = (50, 0, 0); // start position
foreach (var line in lines)
{
    var amount = int.Parse(line[1..]);
    int incr = (line[0] == 'R') ? 1 : -1;
    for (var i = 0; i < amount; i++)
    {
        dial += incr;
        if (dial % 100 == 0)
            sumpart2++;
    }
    if (dial % 100 == 0)
        sumpart1++;
}
Console.WriteLine("Part 1: " + sumpart1);
Console.WriteLine("Part 2: " + sumpart2);
