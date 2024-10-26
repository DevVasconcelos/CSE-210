using System;
using System.Collections.Generic;
using System.IO;

// Base class for goals
public abstract class Goal
{
    protected string _name;
    protected int _points;

    // Constructor to initialize name and points
    public Goal(string name, int points)
    {
        _name = name;
        _points = points;
    }

    // Abstract method to record an event
    public abstract void RecordEvent();

    // Method to get the points
    public int GetPoints()
    {
        return _points;
    }

    // Virtual method to display goal details, can be overridden
    public virtual string GetDetails()
    {
        return $"{_name}, Points: {_points}";
    }

    // Abstract method to return the goal status
    public abstract bool IsComplete();
}

// Simple goal that can be completed once
public class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, int points) : base(name, points)
    {
        _isComplete = false;
    }

    public override void RecordEvent()
    {
        _isComplete = true;
        Console.WriteLine($"Goal completed: {_name}. You earned {_points} points.");
    }

    public override bool IsComplete()
    {
        return _isComplete;
    }

    public override string GetDetails()
    {
        return $"[ {(IsComplete() ? "X" : " ")} ] {_name}, Points: {_points}";
    }
}

// Eternal goal that is never complete but earns points each time
public class EternalGoal : Goal
{
    public EternalGoal(string name, int points) : base(name, points) { }

    public override void RecordEvent()
    {
        Console.WriteLine($"Eternal goal progress: {_name}. You earned {_points} points.");
    }

    public override bool IsComplete()
    {
        return false;
    }

    public override string GetDetails()
    {
        return $"[ ] {_name} (Eternal), Points per completion: {_points}";
    }
}

// Checklist goal that must be repeated multiple times
public class ChecklistGoal : Goal
{
    private int _timesCompleted;
    private int _requiredTimes;
    private int _bonusPoints;

    public ChecklistGoal(string name, int points, int requiredTimes, int bonusPoints) 
        : base(name, points)
    {
        _timesCompleted = 0;
        _requiredTimes = requiredTimes;
        _bonusPoints = bonusPoints;
    }

    public override void RecordEvent()
    {
        _timesCompleted++;
        if (_timesCompleted == _requiredTimes)
        {
            Console.WriteLine($"Checklist completed: {_name}. You earned {_points + _bonusPoints} points (including bonus).");
        }
        else
        {
            Console.WriteLine($"Progress recorded for {_name}. You earned {_points} points. Completed {_timesCompleted}/{_requiredTimes} times.");
        }
    }

    public override bool IsComplete()
    {
        return _timesCompleted >= _requiredTimes;
    }

    public override string GetDetails()
    {
        return $"[ {(IsComplete() ? "X" : " ")} ] {_name} (Checklist), Points: {_points}, Completed {_timesCompleted}/{_requiredTimes}, Bonus: {_bonusPoints} points.";
    }
}

// Class to manage all goals and user progress
public class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _totalPoints;

    public void CreateGoal()
    {
        Console.WriteLine("Choose goal type: 1 - Simple, 2 - Eternal, 3 - Checklist");
        int choice = int.Parse(Console.ReadLine());

        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();
        Console.Write("Enter points: ");
        int points = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                _goals.Add(new SimpleGoal(name, points));
                break;
            case 2:
                _goals.Add(new EternalGoal(name, points));
                break;
            case 3:
                Console.Write("Enter the number of times required to complete: ");
                int requiredTimes = int.Parse(Console.ReadLine());
                Console.Write("Enter bonus points for completing: ");
                int bonusPoints = int.Parse(Console.ReadLine());
                _goals.Add(new ChecklistGoal(name, points, requiredTimes, bonusPoints));
                break;
            default:
                Console.WriteLine("Invalid choice.");
                return;
        }

        Console.WriteLine("Goal created successfully!");
    }

    public void RecordEvent()
    {
        DisplayGoals();
        Console.WriteLine("Choose a goal to record progress:");
        int goalIndex = int.Parse(Console.ReadLine()) - 1;

        if (goalIndex >= 0 && goalIndex < _goals.Count)
        {
            Goal selectedGoal = _goals[goalIndex];
            selectedGoal.RecordEvent();
            _totalPoints += selectedGoal.GetPoints();  // Use GetPoints() to access points
        }
        else
        {
            Console.WriteLine("Invalid goal selection.");
        }
    }

    public void DisplayGoals()
    {
        Console.WriteLine("Your Goals:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetails()}");
        }
        Console.WriteLine($"Total Points: {_totalPoints}");
    }

    public void SaveGoals(string filename)
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            outputFile.WriteLine(_totalPoints);
            foreach (var goal in _goals)
            {
                outputFile.WriteLine(goal.GetDetails());
            }
        }
    }

    public void LoadGoals(string filename)
    {
        if (File.Exists(filename))
        {
            string[] lines = File.ReadAllLines(filename);
            _totalPoints = int.Parse(lines[0]);
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        GoalManager goalManager = new GoalManager();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n1. Create Goal\n2. Record Event\n3. Display Goals\n4. Save Goals\n5. Load Goals\n6. Exit");
            Console.Write("Choose an option: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    goalManager.CreateGoal();
                    break;
                case "2":
                    goalManager.RecordEvent();
                    break;
                case "3":
                    goalManager.DisplayGoals();
                    break;
                case "4":
                    Console.Write("Enter filename to save: ");
                    string saveFile = Console.ReadLine();
                    goalManager.SaveGoals(saveFile);
                    break;
                case "5":
                    Console.Write("Enter filename to load: ");
                    string loadFile = Console.ReadLine();
                    goalManager.LoadGoals(loadFile);
                    break;
                case "6":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}
