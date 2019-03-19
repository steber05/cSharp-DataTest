using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] arr;
            string filepath = "";
            //end of local variables

            //explain the program
            Welcome();
            //check if dice rolls are stored in a file
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Are dice rolls stored in a file? y/n");
                try
                {
                    string answer = Console.ReadLine();
                    if (answer.ToLower().CompareTo("y".ToLower()) == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Enter a file path:");
                        filepath = Console.ReadLine();
                        break;
                    }
                    else if (answer.ToLower().CompareTo("n".ToLower()) == 0)
                    {
                        arr = new string[100];
                        for (int i = 0; i < arr.Length; i++)
                        {
                            Console.Clear();
                            Console.WriteLine("Enter dice roll for dice: {0}", i);
                            arr[i] = Console.ReadLine();
                        }
                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            //ReadDiceFile method
            ReadDiceFile(filepath);
            //Stop app from closing
            Console.ReadLine();
            return;
        }//end of Main

        static void Welcome()
        {
            Console.WriteLine("This program gets dice information and checks if it is a fair dice.\nPress enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }//end of explaining application

        static void ReadDiceFile(string filepath)
        {
            int faces;
            string[] lines = System.IO.File.ReadAllLines(filepath);
            string[] line;
            int[] diceRolls;
            bool isFair;
            //end of local variables

            for (int i = 0; i < lines.Length; i++)
            {
                line = lines[i].Split(',');
                faces = Int32.Parse(line[1]);
                diceRolls = Array.ConvertAll<string, int>(line.Skip(2).ToArray(), int.Parse);
                isFair = CalculateFairDie(diceRolls, faces);
                if (isFair)
                {
                    Console.WriteLine("{0}'s dice is fair", line[0]);
                }
                else
                {
                    Console.WriteLine("{0}'s dice is unfair", line[0]);
                }
                //need to add ReverseArray(), CheckSorted(), BinarySearch
            }
        }//end of ReadDiceFile

        static bool CalculateFairDie(int[] diceRolls, int faces)
        {
            bool b = true;
            int[] rolls = new int[faces];
            int position = 0;
            int squared = 0;
            int sum = 0;
            //end of local variables

            //count for each face value
            for (int i=0;i<diceRolls.Length;i++)
            {
                position = diceRolls[i] - 1;
                rolls[position]++;  
            }
            for (int j = 0; j < rolls.Length; j++)
            {
                rolls[j] = rolls[j] - (100 / faces);
                rolls[j] *= rolls[j];
                squared += rolls[j];
                sum += squared;
            }
            //check if dice is fair
            if (sum <= faces * 2)
            {
                b = true;
            }
            else
            {
                b = false;
            }
            return b;
        }//end of CalculateFairDie
    }
}
