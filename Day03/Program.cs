var lines = File.ReadAllLines("input.txt");
(long sumpart1, long sumpart2) = (0, 0);

foreach (var line in lines)
{
    var digits = line.ToArray().Select(x => int.Parse(x.ToString())).ToList();
    sumpart1 += CalculateMax(digits, 2);
    sumpart2 += CalculateMax(digits, 12);
}

Console.WriteLine($"Part 1: {sumpart1}");
Console.WriteLine($"Part 2: {sumpart2}");

long CalculateMax(List<int> bank, int slots)
{
    var lastIndex = -1;
    var selectedBank = new List<int>();
    while (slots-- > 0)
    {
        var availableBanks = bank.Skip(lastIndex + 1).Take((bank.Count - (lastIndex + 1)) - slots).ToList();
        var maxValue = availableBanks.Max();
        lastIndex = bank.IndexOf(maxValue, lastIndex + 1);
        selectedBank.Add(maxValue);
    }
    return long.Parse(string.Join("", selectedBank));
}