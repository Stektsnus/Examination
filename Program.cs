using System;

namespace Examination
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initiate the program by asking for the personal identity number from the user.
            GetUserInput();
        }

        static void GetUserInput()
        {
            string userInput;
            Console.WriteLine("Write your personal identity number: ");
            userInput = Console.ReadLine();

            // These variables are delcared in order to try to parse them later.
            long personalidentityNumber;
            int intChecker;

            /* Check how long the user input is. If it is 11 characters long it
               needs to be transformed into a 12 character long number. */
            if (userInput.Length != 12 && userInput.Length != 11)
            {
                Console.WriteLine("Something about the personal identity number is not correct. It should be in the format:\nYYYYMMDDnnnc, YYMMDD-nnnc or YYMMDD+nnnc.\nPlease try again.");
                GetUserInput();
            }
            else
            {
                if (userInput.Length == 11)
                {
                    if (userInput[6].ToString() == "-" || userInput[6].ToString() == "+")
                    {
                        string[] userInputSplit = userInput.Split(userInput[6].ToString());
                        foreach (string word in userInputSplit)
                        {
                            if (int.TryParse(word, out intChecker) == false)
                            {
                                Console.WriteLine("Something about the personal identity number is not correct. It should be in the format:\nYYYYMMDDnnnc, YYMMDD-nnnc or YYMMDD+nnnc.\nPlease try again.");
                                GetUserInput();
                            }
                        }
                        userInput = TransformpersonalidentityNumber(userInput);
                    }
                    else
                    {
                        Console.WriteLine("Something about the personal identity number is not correct. It should be in the format:\nYYYYMMDDnnnc, YYMMDD-nnnc or YYMMDD+nnnc.\nPlease try again.");
                        GetUserInput();
                    }
                }
            }
            
            /* Check the user input string so it is actually convertable to a long
               Otherwise ask the user for another input. */
            while (long.TryParse(userInput, out personalidentityNumber) == false)
            {
                Console.WriteLine("Something about the personal identity number is not correct. It should be in the format:\nYYYYMM DDnnnc, YYMMDD-nnnc or YYMMDD+nnnc.\nPlease try again.");
                GetUserInput();
            }
            // Send the personal identity number for validation
            ValidatepersonalidentityNumber(userInput);
        }

        static void ValidatepersonalidentityNumber(string personalidentityNumber)
        {
            // This method calls upon the other validation methods to validate the input personal identity number.
            /* Use a try catch statement so that the program catches errors when trying to validate the personalidentity number
               aswell as restarting the program with the error presented to the user. This will prevent the program from crashing. */
            try
            {
                // Declare neccessary variables for coming validation
                int year = int.Parse(personalidentityNumber[0].ToString() + 
                                     personalidentityNumber[1].ToString() + 
                                     personalidentityNumber[2].ToString() + 
                                     personalidentityNumber[3].ToString());

                int month = int.Parse(personalidentityNumber[4].ToString() + 
                                      personalidentityNumber[5].ToString());

                int day = int.Parse(personalidentityNumber[6].ToString() + 
                                    personalidentityNumber[7]);

                int lastDigit = int.Parse(personalidentityNumber[11].ToString());

                // Validate the year of the personal identity number.
                if (ValidateYear(year, month, day) == false)
                {
                    Console.WriteLine("The given year is not valid. It has to be a year between 1753 and 2021. Please try again.");
                    GetUserInput();
                }
                // Validate the month of the personal identity number.
                else if (ValidateMonth(month) == false)
                {
                    Console.WriteLine("The given month is not valid. It has to be a month between 01 and 12. Please try again.");
                    GetUserInput();
                }
                // Validate the the given day of the personal identity number.
                else if (day > GetDaysOfMonth(month, year))
                {
                    Console.WriteLine("The day given in the personalidentity number is not inside the scope of valid days of the month. Please try again.");
                    GetUserInput();
                }
                // Validate the last digit in the number (the control digit) through the Luhn-algorithm.
                else if (ValidateControlDigit(personalidentityNumber) == false)
                {
                    Console.WriteLine("The given personal identity numbers cannot account for the control digit. Please try again.");
                    GetUserInput();
                }
                // Finally, if the number succeeded all validations, print out that it was ok along with the sex of the person.
                else
                {
                    Console.WriteLine($"\nThe personal identity number is valid.\nThe sex of the person is {CheckSex(lastDigit)}.");
                    Environment.Exit(1);
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"The personal identity number is not valid.\nPlease try again.\n");
                GetUserInput();
            }
        }

        static bool ValidateYear(int year, int month, int day)
        {
            // Check that the year is in the valid span 1753 - 2021-01-01.
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

        static int GetDaysOfMonth(int month, int year)
        {
            // This returns how many days a given month on a given year has.
            switch(month) 
            {
                case 01: case 03: case 05: case 07: case 08: case 10: case 12:
                    return 31;
                case 04: case 06: case 09: case 11:
                    return 30;
                case 02:
                    if (year % 400 == 0)
                    {
                        return 29;
                    }
                    else
                    {
                        if (year % 100 == 0)
                        {
                            return 28;
                        }
                        else if (year % 4 == 0)
                        {
                            return 29;
                        }
                        else
                        {
                            return 28;
                        }
                    }
                default:
                    return 0;
            }
        }

        static bool ValidateMonth(int month)
        {
            // Checking if the input month is within the allowed span of 1-12.
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
            // Returns the sex dependent on the last digit of the personal identity number.
            if (lastDigit == 0 || lastDigit % 2 == 0)
            {
                return "Female";
            }
            else
            {
                return "Male";
            }
        }

        static string TransformpersonalidentityNumber(string userInput)
        {
            // Transforms a 10 digit personal identity number to a 12 digit personal identity number.
            
            // Declare necessary variables for doing the age calculations.
            string fourDigitYear = "";
            var currentDate = DateTime.Now;
            int currentYear = int.Parse(currentDate.ToString("yyyy"));
            int currentDecade = int.Parse(currentDate.ToString("yy"));
            int personalidentityNumberYear = int.Parse(userInput[0].ToString() + userInput[1].ToString());

            switch (userInput[6].ToString())
            {
                case "-":
                    if (personalidentityNumberYear >= 0 && personalidentityNumberYear <= currentDecade)
                    {
                        fourDigitYear = (currentYear - currentDecade + personalidentityNumberYear).ToString();
                    }
                    else
                    {
                        fourDigitYear = (currentYear - (100 + currentDecade - personalidentityNumberYear)).ToString();
                    }
                    // Create a 12 digit personal identity number with the new four digit year.
                    userInput = userInput.Remove(6, 1);
                    userInput = fourDigitYear + userInput.Remove(0, 2);
                    break;
                case "+":
                    if (personalidentityNumberYear >= 0 && personalidentityNumberYear <= currentDecade)
                    {
                        fourDigitYear = (currentYear - (100 + currentDecade - personalidentityNumberYear)).ToString();
                    }
                    else
                    {
                        fourDigitYear = (currentYear - (200 + currentDecade - personalidentityNumberYear)).ToString();
                    }
                    // Create a 12 digit personal identity number with the new four digit year.
                    userInput = userInput.Remove(6, 1);
                    userInput = fourDigitYear + userInput.Remove(0, 2);
                    break;
                default:
                    Console.WriteLine("Something about the personal identity number is not correct. It should be in the format:\nYYYYMMDDnnnc or YYMMDD-nnnc.\nPlease try again.");
                    GetUserInput();
                    break;
            }
            return userInput;
        }

        static bool ValidateControlDigit(string personalidentityNumber)
        {
            // Calculate the control digit in a personal identity number through the Luhn-algorithm and validate the result with the control digit.
            // Store all the number in a string for easier operations later.
            string controlDigits = "";
            int multiplicator = 2;

            // Loop through the personal identity number and multiply by 2 and 1 respectively.
            // Store all the numbers in a string for easier manipulation later on.
            for (int i = 2; i < 11; i++)
            {
                string controlDigit = (int.Parse(personalidentityNumber[i].ToString()) * multiplicator).ToString();
                controlDigits = controlDigits + controlDigit;
                if (multiplicator == 2)
                {
                    multiplicator--;
                }
                else
                {
                    multiplicator++;
                }
            }

            // Sum all the integers from the previous calculation.
            int controlNumber = 0;
            foreach (char digit in controlDigits)
            {
                controlNumber = controlNumber + int.Parse(digit.ToString());
            }

            // Insert the summerized numbers in the mathematical function.
            // Check if the calculated numbers corresponds to the control digit.
            if (((10 - (controlNumber % 10)) % 10) == int.Parse(personalidentityNumber[11].ToString()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
