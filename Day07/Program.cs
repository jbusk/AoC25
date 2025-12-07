using Position = (int x, int y);

var lines = File.ReadAllLines("sample.txt");
int splits = 0;
Position start = (0, lines[0].IndexOf('S'));
HashSet<Position> beams = [start];
// for (int row = 2; row < lines.Length; row += 2)
for (int row = 1; row < lines.Length; row++)
{
    var line = lines[row];
    for (int col = 0; col < line.Length; col++)
    {
        if (beams.Contains(new Position(row -1, col)))
        {
            if (line[col] == '^') // perform split (add beam to sides, count up)
            {
                splits++;
                Position[] newpositions = [(row, col - 1), (row, col + 1)];
                foreach (var newpos in newpositions)
                {
                    if (!beams.Contains(newpos))
                    {
                        beams.Add(newpos);
                    }
                }
            }
            else // add beam to current position
            {
                beams.Add(new Position(row, col));
            }
        }
    }
}

Console.WriteLine(splits);
//
// int RecurseManifold(Position pos)
// {
//     if (pos.x == lines.Length)
//         return 1;
//     if ()
// }