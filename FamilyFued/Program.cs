﻿using System.Globalization;
using System.Runtime.InteropServices;

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
            menu();


        }
        static void menu()
        {
            start();
            int choice;
            do
            {
                Console.Clear();
                Console.Write("The menu options are:\n1. Play 1\n2  View Player\n3. Change Interest\n3. Exit\n\n\n\nYour Choice: ");

                choice = Convert.ToInt32(Console.ReadLine());
                Console.Clear();

                switch (choice)
                {
                    case 0:
                        exit();
                        break;
                    case 1:
                        names();
                        break;
                    case 2:
                        two();
                        break;
                    case 3:
                        Interest();
                        break;
                    case 4:
                        four();
                        break;
                    default:
                        Console.WriteLine("Error. Enter a number 0-4");
                        Console.ReadLine();
                        break;

                }

            } while (choice != 0);
        }
        static void names()
        {
            int[] contestantNumbers = new int[10];
            Random random = new Random();
            for (int i = 0; i< 10; i++)
            {
                int randomNumber;
                do
                {
                    randomNumber = random.Next(1, 26);
                } while (Array.IndexOf(contestantNumbers, randomNumber) != -1);
                contestantNumbers[i] = randomNumber;
                Console.WriteLine($"{details[randomNumber].firstName} {details[randomNumber].lastName}");

            }
            int first = random.Next(1,11);
            Console.WriteLine($"\n\n{details[first].firstName} will go first\n\n\nPress ENTER to continue");
            Console.ReadLine();
            play();



        }
        static void start()
        {
            StreamReader sr = new StreamReader(@"C:\Users\3ddi3\Documents\Family-Feud\FamilyFeud.txt");
            Random rand = new Random();


            int i = 0;
            while (!sr.EndOfStream)
            {

                details[i].firstName = sr.ReadLine();
                details[i].lastName = sr.ReadLine();
                details[i].interest = sr.ReadLine();
                i++;
            }
            sr.Close();
        }
        static void play()
        {//play game

            int points = 0;
            StreamReader sr = new StreamReader(@"C:\Users\3ddi3\Documents\Family-Feud\questions.txt");
            for (int i = 1; i < 3; i++)
            {
                bool[] answerGuessed = new bool[3]; // Track if each answer has been guessed
                Console.Clear();
                Console.WriteLine($"{game[i].question = sr.ReadLine()}");

                game[i].answer1 = sr.ReadLine();
                game[i].points1 = Convert.ToInt32(sr.ReadLine());
                game[i].answer2 = sr.ReadLine();
                game[i].points2 = Convert.ToInt32(sr.ReadLine());
                game[i].answer3 = sr.ReadLine();
                game[i].points3 = Convert.ToInt32(sr.ReadLine());

                int guess = 0;
                int correctAnswersCount = 0;
                Console.WriteLine(game[i].answer1);
                do
                {
                    string anwser = Console.ReadLine().ToLower();

                    if (anwser == game[i].answer1)
                    {
                        if (!answerGuessed[0])
                        {
                            Thread.Sleep(1000);
                            points += game[i].points1;
                            Console.WriteLine(game[i].points1);
                            answerGuessed[0] = true;
                            correctAnswersCount++;
                        }
                        else
                        {
                            Console.WriteLine("you have guessed this already");
                        }

                    }
                    else if (anwser == game[i].answer2 && !answerGuessed[1])
                    {
                        Thread.Sleep(1000);
                        points += game[i].points1;
                        Console.WriteLine(game[i].points2);
                        answerGuessed[0] = true;
                        correctAnswersCount++;

                    }
                    else if (anwser == game[i].answer3 && !answerGuessed[2])
                    {
                        Thread.Sleep(1000);
                        points += game[i].points1;
                        Console.WriteLine(game[i].points3);
                        answerGuessed[0] = true;
                        correctAnswersCount++;
                    }
                    else
                    {
                        guess++;
                        Thread.Sleep(1000);
                        for (int j = 0; j < guess; j++)
                        {
                            Console.Write("X");

                        }
                        Console.WriteLine("");
                    }

                } while (guess != 3&& correctAnswersCount < 3);

                if (guess == 3)
                {
                    i = 10;
                }
                
                else
                {
                    Console.WriteLine("you got them all right!\npress ENTER to move onto the next question");
                    Console.ReadLine();
                }
                
            }
                Console.WriteLine("Game Over");
                Console.ReadLine();
                
                sr.Close();
            






        }

        static void two()
        {

            //write to file
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
            for (int i = 0; i < details.Length; i++)
            {

                Console.Write($"| {details[i].firstName}".PadRight(20));
                Console.Write(details[i].lastName.PadRight(20));
                Console.WriteLine($"{details[i].interest.PadRight(5)} |");
                Console.WriteLine("-----------------------------------------------------------");
            }
            Console.WriteLine("1. Change a Contestants Interest?\n0. Back to Menu");
            int num = Convert.ToInt32(Console.ReadLine());
            if (num == 1)
            {

                Interest();
            }
            menu();

        }

        static void Interest()
        {
            StreamWriter sw = new StreamWriter(@"FamilyFeud.txt");

            Console.WriteLine("Which contestant do you want to edit?");
            string change = Console.ReadLine();
            string[] split = change.Split(' ');
            string changeFirstName = split[0];
            string changeLastName = split[1];
            for (int i = 0; i < details.Length; i++)
            {
                if (details[i].firstName == changeFirstName && details[i].lastName == changeLastName)
                {
                    Console.Write($"What is {details[i].firstName} {details[i].lastName} new interest:");
                    details[i].interest = Console.ReadLine();
                    Console.Write($"{details[i].firstName} {details[i].lastName}'s new interest is {details[i].interest}");
                    sw.WriteLine(details[i].interest);
                    Console.WriteLine(details[i].interest);

                }
            }
            sw.Close();

            Console.ReadLine();
        }

        static void four()
        {
            Console.WriteLine("This is task 4");
            Console.ReadLine();
        }

        static void exit()
        {
            Console.WriteLine("You are exiting.\nGood bye");
            Console.ReadLine();
        }
    }
}
    

