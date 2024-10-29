using System;
using System.Collections.Generic;

namespace ExerciseTrackingApp
{
    // Base class for all activities
    public abstract class Activity
    {
        private string _date;
        private int _lengthInMinutes;

        public Activity(string date, int lengthInMinutes)
        {
            _date = date;
            _lengthInMinutes = lengthInMinutes;
        }

        public int LengthInMinutes => _lengthInMinutes;
        public string Date => _date;

        // Virtual methods to be overridden in derived classes
        public abstract double GetDistance(); // Distance in km or miles
        public abstract double GetSpeed(); // Speed in km/h or mph
        public abstract double GetPace(); // Pace in min per km or min per mile

        // Base GetSummary method that calls virtual methods
        public virtual string GetSummary()
        {
            return $"{Date} {GetType().Name} ({_lengthInMinutes} min): Distance {GetDistance():0.0} km, Speed {GetSpeed():0.0} kph, Pace: {GetPace():0.0} min per km";
        }
    }

    // Running activity derived class
    public class Running : Activity
    {
        private double _distance;

        public Running(string date, int lengthInMinutes, double distance)
            : base(date, lengthInMinutes)
        {
            _distance = distance;
        }

        public override double GetDistance() => _distance;
        public override double GetSpeed() => (_distance / LengthInMinutes) * 60;
        public override double GetPace() => LengthInMinutes / _distance;
    }

    // Cycling activity derived class
    public class Cycling : Activity
    {
        private double _speed;

        public Cycling(string date, int lengthInMinutes, double speed)
            : base(date, lengthInMinutes)
        {
            _speed = speed;
        }

        public override double GetDistance() => (LengthInMinutes * _speed) / 60;
        public override double GetSpeed() => _speed;
        public override double GetPace() => 60 / _speed;
    }

    // Swimming activity derived class
    public class Swimming : Activity
    {
        private int _laps;

        public Swimming(string date, int lengthInMinutes, int laps)
            : base(date, lengthInMinutes)
        {
            _laps = laps;
        }

        public override double GetDistance() => (_laps * 50) / 1000.0;
        public override double GetSpeed() => GetDistance() / LengthInMinutes * 60;
        public override double GetPace() => LengthInMinutes / GetDistance();
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Creating instances of each activity type
            var activities = new List<Activity>
            {
                new Running("03 Nov 2022", 30, 4.8),
                new Cycling("04 Nov 2022", 45, 20.5),
                new Swimming("05 Nov 2022", 30, 20)
            };

            // Display summaries for each activity
            foreach (var activity in activities)
            {
                Console.WriteLine(activity.GetSummary());
            }
        }
    }
}
