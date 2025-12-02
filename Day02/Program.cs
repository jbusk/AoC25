var lines = File.ReadAllText("sample.txt").Split(',').Select((x => x.Split('-')));
(ulong sumpart1, ulong sumpart2) = (0, 0);

foreach (var line in lines)
{
    ulong from = ulong.Parse(line[0]);
    ulong to = ulong.Parse(line[1]);
    for (ulong i = from; i <= to; i++)
    {
        string id = i.ToString();
        string firsthalf = id.Substring(0, id.Length/2);
        string secondhalf = id.Substring(id.Length/2);
        if (firsthalf == secondhalf)
            sumpart1 += i;
        
        
        
    }

}

Console.WriteLine($"Part 1: {sumpart1}");
Console.WriteLine($"Part 2: {sumpart2}");