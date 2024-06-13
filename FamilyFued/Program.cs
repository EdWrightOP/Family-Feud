using System.Globalization;

namespace FamilyFued
{
    internal class Program
    {
        public struct Contestant
        {
            public string firstName,lastName, interest;



        }
        public static Contestant[] details = new Contestant[27];
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
                        one();
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
        static void start()
        {
            Random rand = new Random();
            StreamReader sr = new StreamReader(@"H:\FamilyFeud.txt");

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
        static void one()
        {
            //play game
            
                

            
        }

        static void two()
        {
            //write to file
            for(int i = 0; i < details.Length -1; i++) 
            {
                for (int pos = 0; pos < details.Length - 1; pos++)
                {
                    if(details[pos+1].lastName.CompareTo(details[pos].lastName)<0)
                    {
                        Contestant temp = details[pos+1];
                        details[pos+1] = details[pos];
                        details[pos] = temp;

                    }
                }
                  
            }
            for (int i = 0;i < details.Length;i++) {
                Console.Write(details[i].lastName);
                Console.WriteLine($", {details[i].firstName}");
                Console.WriteLine(details[i].interest);
                Console.WriteLine("----------------------------------------");
            }
            Console.WriteLine("1. Change a Contestants Interest?\n0. Back to Menu");
            int num = Convert.ToInt32(Console.ReadLine());
            if (num == 1) {

                Interest();
            }
            menu();
            
        }

        static void Interest()
        {
            StreamWriter sw = new StreamWriter(@"H:FamilyFeud.txt");
            Console.WriteLine("Which contestant do you want to edit?");
            string change = Console.ReadLine();
            string[] split = change.Split(' ');
            string changeFirstName = split[0];
            string changeLastName = split[1];
            for(int i = 0; i < details.Length; i++) 
            {
                if (details[i].firstName == changeFirstName && details[i].lastName==changeLastName)
                {
                    Console.Write($"What is {details[i].firstName} {details[i].lastName} new interest:");
                    details[i].interest = Console.ReadLine();
                    Console.WriteLine(details[i].interest);
                    Console.ReadLine();



                }
            }

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

