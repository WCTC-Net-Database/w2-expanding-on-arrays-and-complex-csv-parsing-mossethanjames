using System;
using System.IO;

using System;
using System.IO;

class Program
{
    static string[] lines;

    static void Main()
    {
        string filePath = "input.csv";
        lines = File.ReadAllLines(filePath);

        while (true)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Display Characters");
            Console.WriteLine("2. Add Character");
            Console.WriteLine("3. Level Up Character");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DisplayAllCharacters(lines);
                    break;
                case "2":
                    AddCharacter(ref lines);
                    break;
                case "3":
                    LevelUpCharacter(lines);
                    break;
                case "4":
                    File.WriteAllLines(filePath, lines);
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void DisplayAllCharacters(string[] lines)
    {
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            string[] fields = ParseCsvLine(line);

            string name = fields[0];
            string characterClass = fields[1];
            int level = int.Parse(fields[2]);
            int hitPoints = int.Parse(fields[3]);
            string[] equipment = fields[4].Split('|');

            Console.WriteLine($"Name: {name}, Class: {characterClass}, Level: {level}, HP: {hitPoints}, Equipment: {string.Join(", ", equipment)}");
        }
    }

    static void AddCharacter(ref string[] lines)
    {
        Console.Write("Enter character name: ");
        string name = Console.ReadLine();
        Console.Write("Enter character class: ");
        string characterClass = Console.ReadLine();
        Console.Write("Enter character level: ");
        int level = int.Parse(Console.ReadLine());
        Console.Write("Enter character hit points: ");
        int hitPoints = int.Parse(Console.ReadLine());
        Console.Write("Enter character equipment (separated by '|'): ");
        string equipment = Console.ReadLine();

        string newCharacter = $"{name},{characterClass},{level},{hitPoints},{equipment}";
        Array.Resize(ref lines, lines.Length + 1);
        lines[^1] = newCharacter;
    }

    static void LevelUpCharacter(string[] lines)
    {
        Console.Write("Enter the name of the character to level up: ");
        string nameToLevelUp = Console.ReadLine();

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            string[] fields = ParseCsvLine(line);

            if (fields[0].Equals(nameToLevelUp, StringComparison.OrdinalIgnoreCase))
            {
                int level = int.Parse(fields[2]);
                level++;
                fields[2] = level.ToString();
                lines[i] = string.Join(",", fields);
                Console.WriteLine($"Character {fields[0]} leveled up to level {level}!");
                break;
            }
        }
    }

    static string[] ParseCsvLine(string line)
    {
        var fields = new List<string>();
        bool inQuotes = false;
        string field = "";

        foreach (char c in line)
        {
            if (c == '"' && !inQuotes)
            {
                inQuotes = true;
            }
            else if (c == '"' && inQuotes)
            {
                inQuotes = false;
            }
            else if (c == ',' && !inQuotes)
            {
                fields.Add(field);
                field = "";
            }
            else
            {
                field += c;
            }
        }
        fields.Add(field);

        return fields.ToArray();
    }
}
