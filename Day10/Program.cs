using System.Text.RegularExpressions;
using CSharpItertools;
using Google.OrTools.LinearSolver;

namespace Day10;

internal static class Program
{
    private static readonly Dictionary<(int[] goal, List<HashSet<int>>), long> Cache = [];

    private static readonly Dictionary<(bool[] goal, List<HashSet<int>>), HashSet<(HashSet<HashSet<int>>, long)>>
        SolveCache = [];

    private static readonly Itertools Itertools = new();

    public static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt").Select(LinePattern.Match).ToList();
        var (sumpart1, sumpart2) = (0L, 0L);
        foreach (var line in lines)
        {
            var part1Goal = line.Groups[1].Value.Select(x => x == '#' ? 1 : 0).ToArray();
            var part2Goal = line.Groups[3].Value.Split(',').Select(int.Parse).ToArray();
            var buttons = line.Groups[2].Value
                .Split(") (")
                .Select(x => x.Split(",").Select(int.Parse).ToList())
                .ToList();
            sumpart1 += SolvePart1(part1Goal, buttons);
            sumpart2 += SolvePart2(part2Goal, buttons);
        }

        Console.WriteLine($"Part 1: {sumpart1}");
        Console.WriteLine($"Part 2: {sumpart2}");
    }

    private static long SolvePart2(int[] numGoal, List<List<int>> buttons)
    {
        using var solver = Solver.CreateSolver("SCIP");
        var objective = solver.Objective();
        objective.SetMinimization();
        List<Variable> buttonVariables = [];
        for (int bVar = 0; bVar < buttons.Count; bVar++)
        {
            var bVariable = solver.MakeIntVar(0, 1000, $"b{bVar}");
            buttonVariables.Add(bVariable);
            objective.SetCoefficient(bVariable, 1);
        }

        for (int joltageIndex = 0; joltageIndex < numGoal.Length; joltageIndex++)
        {
            var expectedJoltage = numGoal[joltageIndex];
            List<Variable> variables = [];
            for (int buttonIndex = 0; buttonIndex < buttons.Count; buttonIndex++)
            {
                if (buttons[buttonIndex].Contains(joltageIndex))
                    variables.Add(buttonVariables[buttonIndex]);
            }

            LinearExpr expr = new();
            foreach (var bVar in variables)
                expr += bVar;
            var constraint = expr == expectedJoltage;
            solver.Add(constraint);
        }

        solver.Solve();
        var totalSum = (int)buttonVariables.Sum(bVar => bVar.SolutionValue());
        return totalSum;
    }


    private static long SolvePart1(int[] numGoal, List<List<int>> buttons)
    {
        var length = numGoal.Length;
        var boolgoal = numGoal.Select(x => x % 2 == 1).ToArray();
        for (int presses = 1; presses < buttons.Count; presses++)
        {
            foreach (var combinations in Itertools.Combinations(buttons, presses).Select(x => x.ToHashSet()))
            {
                var lights = new HashSet<int>();
                foreach (var combo in combinations)
                {
                    lights.SymmetricExceptWith(combo);
                }

                var curr = new bool[length];
                foreach (var b in lights)
                    curr[b] = true;

                if (!curr.SequenceEqual(boolgoal)) continue;

                return presses;
            }
        }

        return 0;
    }
}

public static partial class LinePattern
{
    [GeneratedRegex(@"\[([.#]+)\] \(([()\d, ]+)\) \{([\d,]+)\}", RegexOptions.Compiled)]
    private static partial Regex Linematch();

    public static Match Match(string input) => Linematch().Match(input);
}
