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
                Console.WriteLine("Enter a file path:");
                filepath = Console.ReadLine();
                break;
            }
            //ReadDiceFile method
            try
            {
                ReadDiceFile(filepath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            } 
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
                Console.WriteLine("\n");
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
                //reverse the array
                ReverseArray(diceRolls);
                Console.Write("Reversed array (");
                for(int j=0;j<diceRolls.Length;j++)
                {
                    Console.Write(diceRolls[j]+",");
                }
                Console.Write(")");
                Console.WriteLine();
                //check the reversed array is sorted
                if (CheckSorted(diceRolls))
                {
                    Console.WriteLine("The array is sorted");
                }
                else
                {
                    Console.WriteLine("The array is not sorted");
                }
                //search if 201 is in the array
                if(LinearSearch(diceRolls, 15))
                {
                    Console.WriteLine("201 is in the array");
                }
                else
                {
                    Console.WriteLine("201 is not in the array");
                }
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

        static int[] ReverseArray(int[] a)
        {
            int low = 0, high = a.Length - 1, temp = 0;

            while (low < high)
            {
                temp = a[low];
                a[low] = a[high];
                a[high] = temp;
                low++;
                high--;
            }
            return a;
        }//end of ReverseArray

        static bool CheckSorted(int[] a)
        {
            //default b and setup checks for false
            bool b = true;
            for (int i = 0; i < a.Length - 1; i++)
            {
                if (a[i] > a[i + 1])
                {
                    b = false;
                    break;
                }
            }
            return b;
        }

        static bool LinearSearch(int[] a, int x)
        {
            bool b = false;

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == x)
                {
                    b = true;
                    break;
                }
            }
            return b;
        }//end of linear search
    }
}
