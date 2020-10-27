using System;
using System.Dynamic;
using System.Threading;

namespace Examination
{
    class Program
    {
        static void Main(string[] args)
        {
            GetUserInput();
            Console.ReadKey();
        }

        static void GetUserInput()
        {
            string userInput = "";
            long personalNumber;

            /* Check the user input string so it is actually convertable to an int
               Otherwise ask the user for another input */
            while (long.TryParse(userInput, out personalNumber) == false)
            {
                
                Console.WriteLine("Write your personal number: ");
                userInput = Console.ReadLine();
            }


            // Send the personal number for validation
            ValidatePersonalNumber(userInput);
        }


        static void ValidatePersonalNumber(string personalNumber)
        {

            /* Use a try catch statement so that the program catches errors when trying to validate the personal number
               aswell as restarting the program with the error presented to the user */
            try
            {
                // Validate the length of the personal number by trying to manipulate the last digit
                // Inte säker på att detta behövs. kommer validera sig självt tillslut när jag försöker kolla på den sista siffran.
                // splitPersonalNumber[11].ToString();

                // Declare neccessary variables for coming validation
                int year = int.Parse(personalNumber[0].ToString() + 
                                     personalNumber[1].ToString() + 
                                     personalNumber[2].ToString() + 
                                     personalNumber[3].ToString());

                int month = int.Parse(personalNumber[4].ToString() + 
                                      personalNumber[5].ToString());

                int day = int.Parse(personalNumber[6].ToString() + 
                                    personalNumber[7]);

                int lastDigit = int.Parse(personalNumber[-1].ToString());

                // Validate the year of the personal number
                if (ValidateYear(year) == false)
                {
                    Console.WriteLine("The given year is not valid. It has to be a year between 1753 and 2020. Please try again.");
                    GetUserInput();
                }
                else if (ValidateMonth(month) == false)
                {
                    Console.WriteLine("The given month is not valid. It has to be a month between 01 and 12. Please try again.");
                    GetUserInput();
                }
                // Validate the the given day of the personal number
                else if (month < GetDaysOfMonth())
                {
                    Console.WriteLine("The day given in the personal number is not inside the scope of valid days of the month. Please try again.");
                    GetUserInput();
                }

                // TODO: Check the gender of the personal number

            }

            catch (Exception e)
            {
                Console.WriteLine($"The personal number is not valid.\nError:\n\n{e}\n\nPlease try again.\n");
                GetUserInput();
            }

        }
        static bool ValidateYear(int year)
        {
            if (year < 2020 && year > 1753)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // TODO: Finish the logic for extracting days of the month
        static int GetDaysOfMonth(int month, int year)
        {
            if (month == 01 || 
                month == 03 || 
                month == 05 || 
                month == 07 || 
                month == 08 || 
                month == 10 || 
                month == 12 || )
            {
                return 31;
            }
            else if (month == 04 || 
                     month == 06 || 
                     month == 09 || 
                     month == 11 || )
            {
                return 30;
            }
            else 
            {
            }
            
                
        }

        static bool ValidateMonth(int month)
        {
            if (month > 12 || month == 00)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
