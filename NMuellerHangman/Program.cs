using System;
using System.Collections.Generic;

namespace NMuellerHangman
{
    class Program
    {
        static void Main(string[] args)
        {
            Hangman game = new Hangman(6, "happiness");

            game.GuessLetter('a');
            game.GuessLetter('x');
            game.GuessLetter('x');
            game.GuessLetter('h');
            game.GuessLetter('s');
            game.GuessLetter('p');
            game.GuessLetter('s');
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

            DisplayGuess();
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
                Console.WriteLine("You have already guessed the letter: " + letter);
                Console.Write("Your previous guesses: ");
                Guesses.ForEach(x => Console.Write("{0} ", x));
                Console.WriteLine(Environment.NewLine + "Please guess again.");
                Console.Write(Environment.NewLine);
            }
            else
            {
                Guesses.Add(letter);

                if (Word.Contains(letter))
                {
                    CorrectGuesses += 1;
                }
                else
                {
                    Lives -= 1;
                }

                CalculateScore();
                DisplayGuess();
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

            Console.WriteLine("Lives: " + Lives);
            Console.WriteLine("Correct guesses: " + CorrectGuesses);
            Console.WriteLine("Word: " + DisplayWord);
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
