var lines = File.ReadAllLines("input.txt");
(int sumpart1, int sumpart2)  = (0, 0);

foreach (var line in lines)
{
    var digits = line.ToArray().Select(x=> int.Parse(x.ToString())).ToList();
    int highest = 0;
    for (int i = 0; i < digits.Count-1; i++)
    {
        int tens = digits[i] * 10;
        for (int j = i+1; j < digits.Count; j++)
        {
            int ones = digits[j];
            int curr = tens + ones;
            if (curr > highest)
                highest = curr;
        }
    }
    sumpart1 += highest;
}

Console.WriteLine($"Part 1: {sumpart1}");
Console.WriteLine($"Part 2: {sumpart2}");