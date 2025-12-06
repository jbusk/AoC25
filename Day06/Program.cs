var lines = File.ReadAllLines("input.txt");
var operands = lines[^1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
var nums1 = new Dictionary<int, List<int>>();
var nums2 = new Dictionary<int, List<int>>();

foreach (var line in lines[0..^1])
{
    var split = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    for (int i = 0; i < split.Length; i++)
        if (nums1.TryGetValue(i, out var list))
            list.Add(int.Parse(split[i]));
        else
            nums1.Add(i, [int.Parse(split[i])]);
}

Console.WriteLine($"Part 1: {Calculate(nums1)}");

var (dictI, maxj, maxi) = (0, lines.Length - 1, lines[0].Length);
for (int i = 0; i < maxi; i++)
{
    var val = string.Empty;
    for (int j = 0; j < maxj; j++)
        val += lines[j][i];
    if (val.Trim() != string.Empty)
        if (nums2.TryGetValue(dictI, out var numlist))
            numlist.Add(int.Parse(val.Trim()));
        else
            nums2.Add(dictI, [int.Parse(val.Trim())]);
    else
        dictI++;
}

Console.WriteLine($"Part 2: {Calculate(nums2)}");

long Calculate(Dictionary<int, List<int>> nums)
{
    long sum = 0;
    for (int i = 0; i < operands.Length; i++)
        if (operands[i] == "*")
            sum += nums[i].Aggregate<int, long>(1, (current, num) => current * num);
        else
            sum += nums[i].Sum();
    return sum;
}