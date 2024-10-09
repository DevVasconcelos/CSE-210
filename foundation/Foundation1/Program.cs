using System;
using System.Collections.Generic;

public class Comment
{
    public string AuthorName { get; set; }
    public string Text { get; set; }

    public Comment(string authorName, string text)
    {
        AuthorName = authorName;
        Text = text;
    }
}

public class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int LengthInSeconds { get; set; }
    private List<Comment> Comments { get; set; }

    public Video(string title, string author, int lengthInSeconds)
    {
        Title = title;
        Author = author;
        LengthInSeconds = lengthInSeconds;
        Comments = new List<Comment>();
    }

    // Method to add a comment
    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    // Method to return the number of comments
    public int GetCommentCount()
    {
        return Comments.Count;
    }

    // Method to display comments
    public void DisplayComments()
    {
        foreach (var comment in Comments)
        {
            Console.WriteLine($"Comment by {comment.AuthorName}: {comment.Text}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Creating the videos
        Video video1 = new Video("Learn C# in 10 Minutes", "Tech Guru", 600);
        Video video2 = new Video("Mastering OOP Concepts", "Code Master", 1200);
        Video video3 = new Video("Fun with Data Structures", "Coding Fun", 900);

        // Adding comments to video1
        video1.AddComment(new Comment("Alice", "Great video, very helpful!"));
        video1.AddComment(new Comment("Bob", "I learned a lot, thanks!"));
        video1.AddComment(new Comment("Charlie", "Nice explanation!"));

        // Adding comments to video2
        video2.AddComment(new Comment("David", "This really clarified OOP for me!"));
        video2.AddComment(new Comment("Emma", "OOP rocks!"));
        video2.AddComment(new Comment("Frank", "Very well presented."));

        // Adding comments to video3
        video3.AddComment(new Comment("Grace", "Awesome video!"));
        video3.AddComment(new Comment("Hannah", "Loved the examples!"));
        video3.AddComment(new Comment("Ivy", "Very informative!"));

        // Creating the video list
        List<Video> videos = new List<Video> { video1, video2, video3 };

        // Displaying information for each video
        foreach (var video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.LengthInSeconds} seconds");
            Console.WriteLine($"Number of comments: {video.GetCommentCount()}");

            // Displaying comments
            video.DisplayComments();
            Console.WriteLine();
        }
    }
}
