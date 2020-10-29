using System;

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
            Console.WriteLine("Write your personal number: ");
            string userInput = Console.ReadLine();
            long personalNumber;

            /* Check the user input string so it is actually convertable to an int
               Otherwise ask the user for another input */
            while (long.TryParse(userInput, out personalNumber) == false)
            {
                
                Console.WriteLine("Something about the personal number is not correct. It should be in the format:\nYYYYMMDDnnnc or YYMMDD-nnnc.\nPlease try again.");
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

                int lastDigit = int.Parse(personalNumber[11].ToString());


                Console.WriteLine(GetDaysOfMonth(month, year));
                // Validate the year of the personal number
                if (ValidateYear(year, month, day) == false)
                {
                    Console.WriteLine("The given year is not valid. It has to be a year between 1753 and 2021. Please try again.");
                    GetUserInput();
                }
                // Validate the month of the personal number
                else if (ValidateMonth(month) == false)
                {
                    Console.WriteLine("The given month is not valid. It has to be a month between 01 and 12. Please try again.");
                    GetUserInput();
                }
                // Validate the the given day of the personal number
                else if (day > GetDaysOfMonth(month, year))
                {
                    Console.WriteLine("The day given in the personal number is not inside the scope of valid days of the month. Please try again.");
                    GetUserInput();
                }

                // TODO: Check the gender of the personal number
                else
                {
                    Console.WriteLine($"The personal number is valid.\nThe sex of the person is {CheckSex(lastDigit)}.");
                }

            }

            catch (Exception e)
            {
                Console.WriteLine($"The personal number is not valid.\nError:\n\n{e}\n\nPlease try again.\n");
                GetUserInput();
            }

        }
        static bool ValidateYear(int year, int month, int day)
        {
            // Check that the year is in the valid span
            if (year <= 2021 && year >= 1753)
            {

                // Only the first of January is the valid day if the year is 2021. Check if its true.
                if (year == 2021)
                {
                    if (month != 01)
                    {
                        return false;
                    }
                    else
                    {
                        if (day != 01)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
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
            switch(month) 
            {
                case 01: case 03: case 05: case 07: case 08: case 10: case 12:
                    Console.WriteLine(month);
                    return 31;
                case 04: case 06: case 09: case 11:
                    Console.WriteLine(month);
                    return 30;
                case 02:
                    if (year % 400 == 0)
                    {
                        Console.WriteLine(month);
                        return 29;
                    }
                    else
                    {
                        if (year % 100 == 0)
                        {
                            Console.WriteLine(month);
                            return 28;
                        }
                        else if (year % 4 == 0)
                        {
                            Console.WriteLine(month);
                            return 29;
                        }
                        else
                        {
                            Console.WriteLine(month);
                            return 28;
                        }
                    }
                default:
                    return 0;
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

        static string CheckSex(int lastDigit)
        {
            if (lastDigit % 2 == 0)
            {
                return "Female";
            }
            else
            {
                return "Male";
            }
        }
    }
}
