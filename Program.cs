
using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        // 1. Prompt the user to input their name
        Console.Write("Enter your name: ");
        string name = Console.ReadLine();

        // 2. Prompt the user to input their birthdate in MM/dd/yyyy format
        string birthDateInput = "";
        DateTime birthDate = DateTime.MinValue;
        bool isValidDate = false;

        // Loop until a valid birthdate is provided
        while (!isValidDate)
        {
            Console.Write("Enter your birthdate (MM/dd/yyyy): ");
            birthDateInput = Console.ReadLine();

            // Validate date format using Regex
            if (Regex.IsMatch(birthDateInput, @"^(0[1-9]|1[0-2])/(0[1-9]|[12][0-9]|3[01])/([0-9]{4})$"))
            {
                // Parse the input into a DateTime object
                if (DateTime.TryParseExact(birthDateInput, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate))
                {
                    isValidDate = true;
                }
                else
                {
                    Console.WriteLine("Invalid date. Please try again.");
                }
            }
            else
            {
                Console.WriteLine("Invalid format. Please use MM/dd/yyyy.");
            }
        }

        // 3. Calculate the user's age
        int age = CalculateAge(birthDate);
        Console.WriteLine($"Hello {name}, you are {age} years old.");

        // 4. Save user info to a file named 'user_info.txt'
        string filePath = "user_info.txt";
        File.WriteAllText(filePath, $"Name: {name}\nAge: {age}\nBirthdate: {birthDate.ToString("MM/dd/yyyy")}");
        Console.WriteLine($"User info saved to {filePath}");

        // 5. Prompt the user to enter a directory path and list all files
        Console.Write("Enter a directory path: ");
        string directoryPath = Console.ReadLine();

        if (Directory.Exists(directoryPath))
        {
            string[] files = Directory.GetFiles(directoryPath);
            Console.WriteLine("Files in the directory:");
            foreach (string file in files)
            {
                Console.WriteLine(Path.GetFileName(file));
            }
        }
        else
        {
            Console.WriteLine("Directory not found.");
        }

        // 6. Prompt user to enter a string and convert it to title case
        Console.Write("Enter a string to convert to title case: ");
        string inputString = Console.ReadLine();
        string titleCaseString = ToTitleCase(inputString);
        Console.WriteLine($"Title case: {titleCaseString}");

        // 7. Explicitly trigger garbage collection
        Console.WriteLine("Triggering garbage collection...");
        GC.Collect();
        GC.WaitForPendingFinalizers(); // Wait for garbage collection to complete
        Console.WriteLine("Garbage collection triggered.");
    }

    // Method to calculate age based on birthdate
    static int CalculateAge(DateTime birthDate)
    {
        DateTime today = DateTime.Today;
        int age = today.Year - birthDate.Year;

        // Check if the birthday has passed this year; if not, subtract one year from the age
        if (birthDate.Date > today.AddYears(-age)) age--;
        return age;
    }

    // Method to convert a string to title case
    static string ToTitleCase(string str)
    {
        TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
        return textInfo.ToTitleCase(str.ToLower());
    }
}