using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Timers;

class Entry
{
    public string Date { get; set; }
    public string Prompt { get; set; }
    public string Response { get; set; }

    public Entry(string date, string prompt, string response)
    {
        Date = date;
        Prompt = prompt;
        Response = response;
    }
}

class Journal
{
    private List<Entry> entries = new List<Entry>();
    private static readonly string[] prompts = {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };

    private System.Timers.Timer timer; // Explicit reference to System.Timers.Timer

    public void AddEntry(string response)
    {
        string date = DateTime.Now.ToShortDateString();
        string prompt = GetRandomPrompt();
        entries.Add(new Entry(date, prompt, response));
        Console.WriteLine("Entry added successfully.");
    }

    public void DisplayEntries()
    {
        if (entries.Count == 0)
        {
            Console.WriteLine("No entries found.");
            return;
        }

        foreach (var entry in entries)
        {
            Console.WriteLine($"{entry.Date} - Prompt: {entry.Prompt}\nResponse: {entry.Response}\n");
        }
    }

    public void SaveToFile(string filename)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (var entry in entries)
                {
                    writer.WriteLine($"{entry.Date},{entry.Prompt},{entry.Response}");
                }
            }
            Console.WriteLine("Journal saved successfully.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error saving file: {e.Message}");
        }
    }

    public void LoadFromFile(string filename)
    {
        try
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine($"File not found: {filename}");
                return;
            }

            entries.Clear();
            string[] lines = File.ReadAllLines(filename, System.Text.Encoding.UTF8);
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts.Length >= 3)
                {
                    string response = string.Join(",", parts.Skip(2));
                    entries.Add(new Entry(parts[0], parts[1], response));
                }
                else
                {
                    Console.WriteLine($"Invalid line format: {line}");
                }
            }
            Console.WriteLine("Journal loaded successfully.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error loading file: {e.Message}");
        }
    }

    private string GetRandomPrompt()
    {
        Random random = new Random();
        int index = random.Next(prompts.Length);
        return prompts[index];
    }

    // Daily reminder to write in the journal
    public void SetDailyReminder()
    {
        timer = new System.Timers.Timer(86400000); // 24 hours in milliseconds
        timer.Elapsed += (sender, e) =>
        {
            Console.WriteLine("Lembrete: É hora de escrever no seu diário!");
        };
        timer.Start();
    }

    public void StopReminder()
    {
        timer?.Stop();
        timer?.Dispose();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        journal.SetDailyReminder();
        bool running = true;

        while (running)
        {
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display journal entries");
            Console.WriteLine("3. Save journal to file");
            Console.WriteLine("4. Load journal from file");
            Console.WriteLine("5. Exit");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Your response:");
                    string response = Console.ReadLine();
                    journal.AddEntry(response);
                    break;
                case "2":
                    journal.DisplayEntries();
                    break;
                case "3":
                    Console.WriteLine("Enter filename to save:");
                    string saveFile = Console.ReadLine();
                    journal.SaveToFile(saveFile);
                    break;
                case "4":
                    Console.WriteLine("Enter filename to load:");
                    string loadFile = Console.ReadLine();
                    journal.LoadFromFile(loadFile);
                    break;
                case "5":
                    journal.StopReminder();
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }
}
