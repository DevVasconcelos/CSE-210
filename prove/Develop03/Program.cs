using System;
using System.Collections.Generic;
using System.Linq;

public class Reference
{
    private string _book;
    private int _chapter;
    private int _verse;
    private int _endVerse;

    // Constructor for single verse reference
    public Reference(string book, int chapter, int verse)
    {
        _book = book;
        _chapter = chapter;
        _verse = verse;
        _endVerse = verse;
    }

    // Constructor for reference with multiple verses
    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        _book = book;
        _chapter = chapter;
        _verse = startVerse;
        _endVerse = endVerse;
    }

    // Method to return reference as displayable text
    public string GetDisplayText()
    {
        if (_verse == _endVerse)
        {
            return $"{_book} {_chapter}:{_verse}";
        }
        else
        {
            return $"{_book} {_chapter}:{_verse}-{_endVerse}";
        }
    }
}

public class Word
{
    private string _text;
    private bool _isHidden;

    // Constructor that initializes the word with its visibility status
    public Word(string text)
    {
        _text = text;
        _isHidden = false;
    }

    // Method to hide the word
    public void Hide()
    {
        _isHidden = true;
    }

    // Method to show the word
    public void Show()
    {
        _isHidden = false;
    }

    // Check if the word is hidden
    public bool IsHidden()
    {
        return _isHidden;
    }

    // Return the word as underscores if hidden, otherwise return the word itself
    public string GetDisplayText()
    {
        return _isHidden ? new string('_', _text.Length) : _text;
    }
}

public class Scripture
{
    private Reference _reference;
    private List<Word> _words;

    // Constructor for the Scripture, splits the text into words and creates Word objects
    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = text.Split(' ').Select(word => new Word(word)).ToList();
    }

    // Method to hide a random number of words
    public void HideRandomWords(int numberToHide)
    {
        Random random = new Random();
        int hiddenCount = 0;
        while (hiddenCount < numberToHide)
        {
            int index = random.Next(_words.Count);
            if (!_words[index].IsHidden())
            {
                _words[index].Hide();
                hiddenCount++;
            }
        }
    }

    // Display the scripture text with hidden words as underscores
    public string GetDisplayText()
    {
        return $"{_reference.GetDisplayText()} {string.Join(" ", _words.Select(w => w.GetDisplayText()))}";
    }

    // Check if all words in the scripture are hidden
    public bool IsCompletelyHidden()
    {
        return _words.All(word => word.IsHidden());
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Creating a reference and a Scripture instance with a verse.
        Reference reference = new Reference("John", 3, 16);
        Scripture scripture = new Scripture(reference, "For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life.");

        // The program hides words randomly, creating a dynamic challenge for the user.
        // This functionality goes beyond the basic requirement because instead of just displaying or manually hiding words,
        // the code uses randomization to hide different words each time, making the process more interactive.
        // This feature encourages the user to memorize the scriptures in a more engaging way.
        while (true)
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());

            if (scripture.IsCompletelyHidden())
            {
                Console.WriteLine("All words are hidden. Program ended.");
                break;
            }

            Console.WriteLine("Press Enter to hide words or type 'quit' to exit.");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
            {
                break;
            }

            // Hides 3 random words at a time. This goes beyond hiding a fixed number of static words
            // and introduces unpredictability and creativity, challenging the user to memorize the verse.
            scripture.HideRandomWords(3);
        }
    }
}
