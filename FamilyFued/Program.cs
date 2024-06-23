using System;
using System.IO;
using System.Threading;

namespace FamilyFued
{
    internal class Program
    {
        public struct Contestant
        {
            public string firstName, lastName, interest;
        }

        public struct Questions
        {
            public string question, answer1, answer2, answer3;
            public int points1, points2, points3;
        }

        public static Contestant[] details = new Contestant[27];
        public static Questions[] game = new Questions[21];

        static void Main(string[] args)
        {
            menu(); // Start the menu function
        }

        static void menu()
        {
            start(); // Load contestant details from file

            int choice;
            do
            {
                Console.Clear();
                // Display menu options
                Console.Write("The menu options are:\n1. Play\n2. View Players\n3. Change Interest\n0. Exit\n\nYour Choice: ");
                choice = Convert.ToInt32(Console.ReadLine()); // Read user choice

                Console.Clear();
                switch (choice)
                {
                    case 0:
                        exit(); // Exit the program
                        break;
                    case 1:
                        names(); // Start the game with contestant names
                        break;
                    case 2:
                        two(); // View and sort players
                        break;
                    case 3:
                        Interest(); // Change a contestant's interest
                        break;
                    default:
                        Console.WriteLine("Error. Enter a number 0-4"); // Invalid input
                        Console.ReadLine();
                        break;
                }
            } while (choice != 0); // Repeat until user chooses to exit
        }

        static void names()
        {
            int[] contestantNumbers = new int[10];
            string[] finalists = new string[10];
            Random random = new Random();

            // Select 10 random contestants without repetition
            for (int i = 0; i < 10; i++)
            {
                int randomNumber;
                do
                {
                    randomNumber = random.Next(1, 26);
                } while (Array.IndexOf(contestantNumbers, randomNumber) != -1);

                contestantNumbers[i] = randomNumber;
                finalists[i] = details[randomNumber].firstName;
                Console.WriteLine($"{details[randomNumber].firstName} {details[randomNumber].lastName}");
            }

            int first = random.Next(finalists.Length); // Randomly choose who goes first
            Console.WriteLine($"\n\n{finalists[first]} will go first\n\n\nPress ENTER to continue");
            Console.ReadLine();

            play(); // Start the game
        }

        static void start()
        {
            StreamReader sr = new StreamReader(@"FamilyFeud.txt"); // Read contestant details from file
            int i = 0;
            while (!sr.EndOfStream)
            {
                // Read and store contestant details
                details[i].firstName = sr.ReadLine();
                details[i].lastName = sr.ReadLine();
                details[i].interest = sr.ReadLine();
                i++;
            }
            sr.Close(); // Close the file
        }

        static void play()
        {
            int points = 0;
            int i = 0;
            StreamReader sr = new StreamReader(@"questions.txt"); // Read game questions from file

            // Loop through 4 questions
            for (i = 1; i < 5; i++)
            {
                bool[] answerGuessed = new bool[3]; // Track if each answer has been guessed
                Console.Clear();
                Console.WriteLine($"{game[i].question = sr.ReadLine()}"); // Display the question

                // Read and display answers and points for current question
                game[i].answer1 = sr.ReadLine();
                game[i].points1 = Convert.ToInt32(sr.ReadLine());
                game[i].answer2 = sr.ReadLine();
                game[i].points2 = Convert.ToInt32(sr.ReadLine());
                game[i].answer3 = sr.ReadLine();
                game[i].points3 = Convert.ToInt32(sr.ReadLine());

                int wrongGuess = 0;
                int correctAnswersCount = 0;

                do
                {
                    string answer = Console.ReadLine().ToLower(); // Read player's answer

                    // Check if answer is correct
                    if (answer == game[i].answer1)
                    {
                        if (!answerGuessed[0]) // If answer hasn't been guessed before
                        {
                            Thread.Sleep(1000);
                            points += game[i].points1; // Add points
                            Console.WriteLine(game[i].points1);
                            answerGuessed[0] = true;
                            correctAnswersCount++;
                        }
                        else
                        {
                            Console.WriteLine("You have guessed this already");
                        }
                    }
                    else if (answer == game[i].answer2)
                    {
                        if (!answerGuessed[1])
                        {
                            Thread.Sleep(1000);
                            points += game[i].points2;
                            Console.WriteLine(game[i].points2);
                            answerGuessed[1] = true;
                            correctAnswersCount++;
                        }
                        else
                        {
                            Console.WriteLine("You have guessed this already");
                        }
                    }
                    else if (answer == game[i].answer3)
                    {
                        if (!answerGuessed[2])
                        {
                            Thread.Sleep(1000);
                            points += game[i].points3;
                            Console.WriteLine(game[i].points3);
                            answerGuessed[2] = true;
                            correctAnswersCount++;
                        }
                        else
                        {
                            Console.WriteLine("You have guessed this already");
                        }
                    }
                    else if (answer == "" || answer == " ")
                    {
                        Console.WriteLine("Please enter a word"); // Invalid input
                    }
                    else
                    {
                        wrongGuess++; // Incorrect answer
                        Thread.Sleep(1000);
                        for (int j = 0; j < wrongGuess; j++)
                        {
                            Console.Write("X"); // Display wrong guesses
                        }
                        Console.WriteLine("");

                        // If 3 wrong guesses, display correct answers and points
                        if (wrongGuess == 3)
                        {
                            if (!answerGuessed[0])
                            {
                                Console.WriteLine("");
                                Console.WriteLine(game[i].answer1);
                                Console.WriteLine(game[i].points1);
                            }
                            if (!answerGuessed[1])
                            {
                                Console.WriteLine("");
                                Console.WriteLine(game[i].answer2);
                                Console.WriteLine(game[i].points2);
                            }
                            if (!answerGuessed[2])
                            {
                                Console.WriteLine("");
                                Console.WriteLine(game[i].answer3);
                                Console.WriteLine(game[i].points3);
                            }
                            Console.WriteLine("Game Over");
                        }
                    }
                } while (wrongGuess != 3 && correctAnswersCount < 3); // Continue until 3 wrong guesses or 3 correct answers

                if (wrongGuess == 3)
                {
                    i = 10; // Exit loop if 3 wrong guesses
                }
                else
                {
                    Console.WriteLine("You got them all right!\nPress ENTER to move onto the next question");
                    Console.ReadLine();
                }
            }

            Console.WriteLine($"You finished the game with {points} Points"); // Display final points
            Console.ReadLine();
            sr.Close(); // Close the file
        }

        static void two()
        {
            // Sort contestants alphabetically by last name
            for (int i = 0; i < details.Length - 1; i++)
            {
                for (int pos = 0; pos < details.Length - 1; pos++)
                {
                    if (details[pos + 1].lastName.CompareTo(details[pos].lastName) < 0)
                    {
                        Contestant temp = details[pos + 1];
                        details[pos + 1] = details[pos];
                        details[pos] = temp;
                    }
                }
            }

            // Display sorted contestant details
            for (int i = 0; i < details.Length; i++)
            {
                Console.Write($"| {char.ToUpper(details[i].firstName[0]) + details[i].firstName.Substring(1).PadRight(20)}");
                Console.Write(char.ToUpper(details[i].lastName[0]) + details[i].lastName.Substring(1).PadRight(20));
                Console.WriteLine($"{char.ToUpper(details[i].interest[0]) + details[i].interest.Substring(1).PadRight(20)}|");
                Console.WriteLine("-------------------------------------------------------------");
            }

            // Prompt user for further action
            Console.WriteLine("1. Change a Contestant's Interest?\n0. Back to Menu");
            int num = Convert.ToInt32(Console.ReadLine());
            if (num == 1)
            {
                Interest(); // Proceed to change contestant's interest
            }
            menu(); // Return to main menu
        }

        static void Interest()
        {
            StreamWriter sw = new StreamWriter(@"FamilyFeud.txt"); // Open file for writing
            Console.WriteLine("Please enter the First then Last name of the contestant you wish to edit");
            string change = Console.ReadLine().ToLower();
            string[] split = change.Split(' ');
            string changeFirstName = split[0];
            string changeLastName = split[1];

            // Update contestant's interest
            for (int i = 0; i < details.Length; i++)
            {
                if (details[i].firstName == changeFirstName && details[i].lastName == changeLastName)
                {
                    Console.Write($"What is {details[i].firstName} {details[i].lastName}'s new interest: ");
                    details[i].interest = Console.ReadLine();
                    Console.Write($"{details[i].firstName} {details[i].lastName}'s new interest is {details[i].interest}");
                }
            }

            // Write updated details to file
            for (int i = 0; i < details.Length; i++)
            {
                sw.WriteLine(details[i].firstName.ToLower());
                sw.WriteLine(details[i].lastName.ToLower());
                sw.WriteLine(details[i].interest.ToLower());
            }
            sw.Close(); // Close the file

            Console.ReadLine();
            start(); // Reload contestant details
        }

        static void exit()
        {
            Console.WriteLine("You are exiting.\nGood bye");
            Console.ReadLine();
        }
    }
}
