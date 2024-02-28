using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

class Program
{
    static void Main()
    {
        Menu.LoadLoremIpsumText();

        while (true)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Меню:");
            Console.WriteLine("1. Вивести вказану кількість слів у тексті \"Lorem ipsum\"");
            Console.WriteLine("2. Виконати математичну операцію");
            Console.WriteLine("0. Вийти");

            Console.Write("Оберіть пункт меню (введіть номер): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Menu.DisplaySpecifiedWordCount();
                    break;
                case "2":
                    Menu.PerformMathOperation();
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                    break;
            }
        }
    }

    
}

class Menu
{
    private static string loremIpsumText;

    public static void LoadLoremIpsumText()
    {
        string filePath = "C:\\Users\\1psyh\\OneDrive\\Рабочий стол\\Универ\\Adaptive\\LR1\\LoremIpsum.txt";
        try
        {
            loremIpsumText = File.ReadAllText(filePath);
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Файл {filePath} не знайдено.");
            Environment.Exit(1);
        }
    }

    public static void DisplaySpecifiedWordCount()
    {
        Console.Write("Введіть кількість слів для виведення: ");
        int specifiedWordCount = Convert.ToInt32(Console.ReadLine());

        string[] words = loremIpsumText.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        if (specifiedWordCount <= words.Length)
        {
            for (int i = 0; i < specifiedWordCount; i++)
            {
                Console.Write(words[i] + " ");
            }
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine($"Текст містить менше слів, ніж вказаною кількість ({words.Length}).");
        }
    }

    public static void PerformMathOperation()
    {
        Console.Write("Введіть перше число: ");
        double operand1 = Convert.ToDouble(Console.ReadLine());

        Console.Write("Введіть друге число: ");
        double operand2 = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Введіть операцію (+, -, *, /): ");
        char operation = Convert.ToChar(Console.ReadLine());

        double result = 0;

        switch (operation)
        {
            case '+':
                result = operand1 + operand2;
                break;
            case '-':
                result = operand1 - operand2;
                break;
            case '*':
                result = operand1 * operand2;
                break;
            case '/':
                result = operand1 / operand2;
                break;
            default:
                Console.WriteLine("Невірна операція.");
                return;
        }

        Console.WriteLine($"Результат операції: {result}");
    }
}