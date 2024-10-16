using System;
using System.Collections.Generic;
using System.Threading;

public class Program
{
    public static void Main(string[] args)
    {
        Menu menu = new Menu();
        menu.Run();
    }
}

public class Menu
{
    public void Run()
    {
        int choice = 0;

        while (choice != 4)
        {
            Console.WriteLine("Welcome to the Mindfulness Program!");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflecting Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Quit");
            Console.Write("Please choose an option (1-4): ");

            choice = int.Parse(Console.ReadLine() ?? "4");

            switch (choice)
            {
                case 1:
                    BreathingActivity breathingActivity = new BreathingActivity();
                    breathingActivity.Start();
                    break;
                case 2:
                    ReflectingActivity reflectingActivity = new ReflectingActivity();
                    reflectingActivity.Start();
                    break;
                case 3:
                    ListingActivity listingActivity = new ListingActivity();
                    listingActivity.Start();
                    break;
                case 4:
                    Console.WriteLine("Thank you for using the Mindfulness Program! Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please choose again.");
                    break;
            }
        }
    }
}

public abstract class Activity
{
    private string activityName;
    private string description;
    protected int duration;

    public Activity(string name, string desc)
    {
        activityName = name;
        description = desc;
    }

    public void Start()
    {
        Console.WriteLine($"Starting {activityName}: {description}");
        Console.Write("Enter duration in seconds: ");
        duration = int.Parse(Console.ReadLine() ?? "0");

        Console.WriteLine("Get ready to begin...");
        Pause(3);

        ExecuteActivity();
        
        Console.WriteLine("Good job! You have completed the activity.");
        Pause(3);
    }

    protected abstract void ExecuteActivity();

    protected void Pause(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            Console.WriteLine($"{seconds - i}...");
            Thread.Sleep(1000);
        }
    }
}

public class BreathingActivity : Activity
{
    public BreathingActivity() : base("Breathing Activity", "This activity will help you relax by guiding you through breathing in and out slowly.")
    {
    }

    protected override void ExecuteActivity()
    {
        DateTime endTime = DateTime.Now.AddSeconds(duration);
        while (DateTime.Now < endTime)
        {
            Console.WriteLine("Breathe in...");
            Pause(4);
            Console.WriteLine("Breathe out...");
            Pause(4);
        }
    }
}

public class ReflectingActivity : Activity
{
    private List<string> prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectingActivity() : base("Reflecting Activity", "This activity will help you reflect on times in your life when you have shown strength and resilience.")
    {
    }

    protected override void ExecuteActivity()
    {
        DateTime endTime = DateTime.Now.AddSeconds(duration);
        Random random = new Random();

        while (DateTime.Now < endTime)
        {
            int promptIndex = random.Next(prompts.Count);
            Console.WriteLine(prompts[promptIndex]);
            Pause(5);

            for (int i = 0; i < questions.Count; i++)
            {
                int questionIndex = random.Next(questions.Count);
                Console.WriteLine(questions[questionIndex]);
                Pause(5);
            }
        }
    }
}

public class ListingActivity : Activity
{
    private List<string> prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt a strong spiritual influence this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity() : base("Listing Activity", "This activity will help you reflect on the good things in your life by listing as many as you can in a certain area.")
    {
    }

    protected override void ExecuteActivity()
    {
        DateTime endTime = DateTime.Now.AddSeconds(duration);
        Random random = new Random();

        while (DateTime.Now < endTime)
        {
            int promptIndex = random.Next(prompts.Count);
            Console.WriteLine(prompts[promptIndex]);
            Pause(5);
            Console.WriteLine("Start listing items now!");
            Pause(3);
            int count = 0;

            while (DateTime.Now < endTime)
            {
                Console.Write("Type an item (or type 'exit' to finish): ");
                string input = Console.ReadLine();

                if (input.ToLower() == "exit")
                {
                    break;
                }
                count++;
            }

            Console.WriteLine($"You listed {count} items.");
        }
    }
}
