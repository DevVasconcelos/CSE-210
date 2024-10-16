using System;
using System.Collections.Generic;
using System.Threading;

namespace MindfulnessApp
{
    // Base class for all activities
    public abstract class Activity
    {
        protected string activityName;
        protected string activityDescription;
        protected int duration;

        public Activity(string name, string description)
        {
            activityName = name;
            activityDescription = description;
        }

        public virtual void StartActivity()
        {
            Console.WriteLine($"Starting: {activityName}");
            Console.WriteLine(activityDescription);
            Console.Write("Enter duration (in seconds): ");
            duration = int.Parse(Console.ReadLine());
            Console.WriteLine("Get ready to begin...");
            PauseWithAnimation(3); // A short pause before starting
        }

        public virtual void EndActivity()
        {
            Console.WriteLine($"Great job! You have completed {activityName} for {duration} seconds.");
            PauseWithAnimation(3);
        }

        protected void PauseWithAnimation(int seconds)
        {
            for (int i = 0; i < seconds; i++)
            {
                Console.Write("/");
                Thread.Sleep(250);
                Console.Write("\b \b");

                Console.Write("-");
                Thread.Sleep(250);
                Console.Write("\b \b");

                Console.Write("\\");
                Thread.Sleep(250);
                Console.Write("\b \b");

                Console.Write("|");
                Thread.Sleep(250);
                Console.Write("\b \b");
            }
        }
    }

    // Derived class for Breathing Activity
    public class BreathingActivity : Activity
    {
        public BreathingActivity() : base("Breathing Activity", "This activity will help you relax by guiding your breathing.")
        {
        }

        public override void StartActivity()
        {
            base.StartActivity();
            for (int i = 0; i < duration; i += 6)
            {
                Console.WriteLine("Breathe in...");
                PauseWithAnimation(3);
                Console.WriteLine("Breathe out...");
                PauseWithAnimation(3);
            }
            EndActivity();
        }
    }

    // Derived class for Reflection Activity
    public class ReflectionActivity : Activity
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
            "What made this time different than other times?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience?",
            "What did you learn about yourself?",
            "How can you keep this experience in mind in the future?"
        };

        public ReflectionActivity() : base("Reflection Activity", "This activity will help you reflect on your personal strengths and resilience.")
        {
        }

        public override void StartActivity()
        {
            base.StartActivity();
            Random random = new Random();

            for (int i = 0; i < duration; i += 10)
            {
                Console.WriteLine(prompts[random.Next(prompts.Count)]);
                PauseWithAnimation(5);
                Console.WriteLine(questions[random.Next(questions.Count)]);
                PauseWithAnimation(5);
            }

            EndActivity();
        }
    }

    // Derived class for Listing Activity
    public class ListingActivity : Activity
    {
        private List<string> prompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "Who are some of your personal heroes?"
        };

        public ListingActivity() : base("Listing Activity", "This activity will help you reflect on the positive things in your life by listing as many as you can.")
        {
        }

        public override void StartActivity()
        {
            base.StartActivity();
            Random random = new Random();
            Console.WriteLine(prompts[random.Next(prompts.Count)]);
            PauseWithAnimation(5);

            int itemCount = 0;
            DateTime endTime = DateTime.Now.AddSeconds(duration);

            while (DateTime.Now < endTime)
            {
                Console.Write("List an item: ");
                Console.ReadLine();
                itemCount++;
            }

            Console.WriteLine($"You listed {itemCount} items!");
            EndActivity();
        }
    }

    // Main program to run the mindfulness app
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Mindfulness Activities Menu:");
                Console.WriteLine("1. Breathing Activity");
                Console.WriteLine("2. Reflection Activity");
                Console.WriteLine("3. Listing Activity");
                Console.WriteLine("4. Exit");

                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        BreathingActivity breathingActivity = new BreathingActivity();
                        breathingActivity.StartActivity();
                        break;
                    case "2":
                        ReflectionActivity reflectionActivity = new ReflectionActivity();
                        reflectionActivity.StartActivity();
                        break;
                    case "3":
                        ListingActivity listingActivity = new ListingActivity();
                        listingActivity.StartActivity();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please choose 1, 2, 3, or 4.");
                        break;
                }
            }
        }
    }
}
