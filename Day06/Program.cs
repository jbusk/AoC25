var lines = File.ReadAllLines("input.txt");
var operands = lines[^1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
List<List<int>> nums1 = [];
Dictionary<int, List<int>> nums2 = new Dictionary<int, List<int>>();
for (int i = 0; i < operands.Length; i++)
{
    nums1.Add(new List<int>());
    // nums2.Add(new List<int>());
}

foreach (var line in lines[0..^1])
{
    var split = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

    for (int i = 0; i < split.Length; i++)
    {
        nums1[i].Add(int.Parse(split[i]));
    }
}

long part1 = 0;
for (int i = 0; i < operands.Length; i++)
{
    // Console.Write(string.Join(" " + operands[i] + " ", nums1[i]) + " = ");
    if (operands[i] == "*")
    {
        long sum = 1;
        foreach (var num in nums1[i])
        {
            sum *= num;
        }

        // Console.WriteLine(sum);
        part1 += sum;
    }
    else
    {
        var sum = nums1[i].Sum();
        // Console.WriteLine(sum);
        part1 += nums1[i].Sum();
    }
}

Console.WriteLine($"Part 1: {part1}");



// int maxj = lines.Length - 1;
// int maxi = lines[0].Length - 1;
// for (int i = 0; i < maxi; i++)
// {
//     string val = string.Empty;
//     Console.Write(i + " " );
//     for (int j = 0; j < maxj; j++)
//     {
//         var curr = lines[j][i];
//         Console.Write(curr);
//         val += curr;
//     }
//
//     Console.WriteLine();
//     if (val.Trim() != string.Empty)
//     {
//         if (nums2.TryGetValue(i, out var numlist))
//             numlist.Add(int.Parse(val.Trim()));
//         else
//             nums2.Add(i, new(){ int.Parse(val.Trim())});
//         //nums2[i].Add(int.Parse(val.Trim()));
//     }
// }


int dictI = 0;
int maxj = lines.Length - 1;
int maxi = lines[0].Length ;
for (int i = 0; i < maxi; i++)
{
    string val = string.Empty;
    // Console.Write(i + " " );
    for (int j = 0; j < maxj; j++)
    {
        var curr = lines[j][i];
        // Console.Write(curr);
        val += curr;
    }

    // Console.WriteLine();
    if (val.Trim() != string.Empty)
    {
        // Console.WriteLine("Adding " + val + " to " + dictI);
        if (nums2.TryGetValue(dictI, out var numlist))
            numlist.Add(int.Parse(val.Trim()));
        else
            nums2.Add(dictI, new(){ int.Parse(val.Trim())});
        //nums2[i].Add(int.Parse(val.Trim()));
    }
    else
    {
        dictI++;
    }
}

long part2 = 0;
for (int i = 0; i < operands.Length; i++)
{
    // Console.Write(string.Join(" " + operands[i] + " ", nums2[i]) + " = ");
    if (operands[i] == "*")
    {
        long sum = 1;
        foreach (var num in nums2[i])
        {
            sum *= num;
        }

        // Console.WriteLine(sum);
        part2 += sum;
    }
    else
    {
        var sum = nums2[i].Sum();
        // Console.WriteLine(sum);
        part2 += nums2[i].Sum();
    }
}

Console.WriteLine($"Part 2: {part2}");