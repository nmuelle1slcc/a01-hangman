using System;
using System.Collections.Generic;
using System.Linq;

namespace NMuellerHangman
{
    class Program
    {
        static void Main(string[] args)
        {
            bool Run = true;

            //set default lives
            int Lives = 6;

            //set available words. i don't know how to hook up a dictionary or set random words,
            //so you get to play a finite number of times. sorry!
            List<string> Words = new List<string>()
            {
                "happiness", "polaroid", "nth", "exception", "dynamite", "basque"
            };

            while (Run)
            {
                Hangman game = new Hangman(Lives, Words.FirstOrDefault());

                if (Words.Count > 1)
                {
                    Words.RemoveAt(0);

                    Console.Write("Would you like to play again? Type \"y\" to play again or any other key(s) to quit: ");
                    if (Console.ReadLine() != "y")
                    {
                        Console.WriteLine("Thank you for playing!");
                        Console.Write(Environment.NewLine);

                        return;
                    }
                }
                else
                {
                    Console.WriteLine("Thank you for playing!");
                    Console.Write(Environment.NewLine);

                    return;
                }

            }

        }

    }

    class Hangman
    {
        public bool Playing { get; private set; } = true;
        public int Lives { get; private set; }
        public int CorrectGuesses { get; private set; } = 0;
        public string Word { get; private set; }
        public List<char> Guesses { get; private set; }

        public Hangman(int lives, string word)
        {
            Lives = lives;
            Word = word;
            Guesses = new List<char>();

            Console.WriteLine("Welcome to Hangman!");
            Console.WriteLine("Please enter one letter at a time to try and guess the below word: ");

            DisplayGuess();

            while (Playing)
            {
                Console.Write("Guess a letter: ");
                var input = Console.ReadLine();

                if (input.Length != 1)
                {
                    Console.Write(Environment.NewLine);
                    Console.WriteLine("Please only guess one letter at a time");
                    Console.Write(Environment.NewLine);
                }
                else
                {
                    GuessLetter(input.FirstOrDefault());
                }
            }

            return;
        }

        public void GuessLetter(char letter)
        {
            if (!Playing)
            {
                return;
            }

            //the guess is duplicate
            if (Guesses.Contains(letter))
            {
                Console.Write(Environment.NewLine);
                Console.WriteLine("You have already guessed the letter: " + letter);
                Console.Write("Your previous guesses: ");
                Guesses.ForEach(x => Console.Write("{0} ", x));
                Console.WriteLine(Environment.NewLine + "Please guess again");
                Console.Write(Environment.NewLine);
            }
            else
            {
                Guesses.Add(letter);

                if (Word.Contains(letter))
                {
                    Console.Write(Environment.NewLine);
                    Console.Write("Correct!");
                    CorrectGuesses += Word.Count(x => x == letter);
                }
                else
                {
                    Console.Write(Environment.NewLine);
                    Console.Write("Sorry, not correct");
                    Lives -= 1;
                }

                DisplayGuess();
                CalculateScore();
            }

        }

        private void DisplayGuess()
        {
            if (!Playing)
            {
                return;
            }

            string DisplayWord = "";
            foreach (char Letter in Word)
            {
                if (Guesses.Contains(Letter))
                {
                    DisplayWord += Letter + " ";
                }
                else
                {
                    DisplayWord += "_ ";
                }
            }

            Console.Write(Environment.NewLine);
            Console.WriteLine("Word: " + DisplayWord + "| Lives: " + Lives);
            Console.Write(Environment.NewLine);
        }

        private void CalculateScore()
        {
            if (!Playing)
            {
                return;
            }

            if (Lives <= 0)
            {
                Console.WriteLine("You lose! The word was: " + Word);
                Playing = false;
            }

            if (CorrectGuesses >= Word.Length)
            {
                Console.WriteLine("You win!");
                Playing = false;
            }
        }

    }
}
