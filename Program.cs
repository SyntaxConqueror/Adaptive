using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

class Program
{
    static async Task Main()
    {
        while (true)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Оберіть математичну операцію:");
            Console.WriteLine("1. Додавання");
            Console.WriteLine("2. Віднімання");
            Console.WriteLine("3. Множення");
            Console.WriteLine("4. Ділення");
            Console.WriteLine("5. Вихід");
            Console.Write("Ваш вибір: ");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                if (choice >= 1 && choice <= 4)
                {
                    Console.Write("Введіть перше число: ");
                    double num1 = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Введіть друге число: ");
                    double num2 = Convert.ToDouble(Console.ReadLine());

                    _ = choiceOperation(choice, num1, num2);
                }
                else if (choice == 5)
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                }
            }
            else
            {
                Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
            }
        }
    }

    static void ThreadOperation(Action action)
    {
        Console.WriteLine("Виконання роботи з класом Thread.");

        Thread thread = new Thread(() =>
        {
            action.Invoke();
        });

        thread.Start();
        thread.Join();

        Console.WriteLine("Робота з класом Thread завершена.");
    }

    static async Task AsyncOperation(Func<Task> asyncAction)
    {
        Console.WriteLine("Виконання роботи з Async - Await.");

        await asyncAction.Invoke();

        Console.WriteLine("Робота з Async - Await завершена.");
    }

    private static async Task choiceOperation(int choice, double num1, double num2)
    {
        switch (choice)
        {
            case 1:
                ThreadOperation(() => Console.WriteLine($"Результат додавання: {num1 + num2}"));
                break;
            case 2:
                await AsyncOperation(async () =>
                {
                    await Task.Delay(2000); // Імітація асинхронної операції
                    Console.WriteLine($"Результат віднімання: {num1 - num2}");
                });
                break;
            case 3:
                ThreadOperation(() => Console.WriteLine($"Результат множення: {num1 * num2}"));
                break;
            case 4:
                await AsyncOperation(async () =>
                {
                    await Task.Delay(2000); // Імітація асинхронної операції
                    Console.WriteLine($"Результат ділення: {num1 / num2}");
                });
                break;
        }
    }
}