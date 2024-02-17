using System;
using System.Reflection;

public class MyClass
{
    // Поля
    private int _privateField;
    public string PublicField;
    protected double _protectedField;
    internal bool InternalField;
    public static char StaticField;

    // Методи
    public void DisplayInfo(string name, int age)
    {
        Console.WriteLine($"Name: {name}, Age: {age}");
    }

    public int CalculateSum(params int[] numbers)
    {
        int sum = 0;
        foreach (int num in numbers)
        {
            sum += num;
        }
        return sum;
    }

    public string GenerateMessage(string firstName, string lastName)
    {
        return $"Hello, {firstName} {lastName}!";
    }

    public double CalculateAverage(double[] values)
    {
        if (values.Length == 0)
        {
            throw new ArgumentException("Values array cannot be empty.");
        }

        double sum = 0;
        foreach (double value in values)
        {
            sum += value;
        }
        return sum / values.Length;
    }
}

class Program
{
    static void Main(string[] args)
    {
        var myClassInstance = new MyClass();

        Type myClassType = myClassInstance.GetType();
        TypeInfo myClassTypeInfo = myClassType.GetTypeInfo();

        Console.WriteLine("Type: " + myClassType.Name);
        Console.WriteLine("Is class abstract? " + myClassTypeInfo.IsAbstract);
        Console.WriteLine("Is class sealed? " + myClassTypeInfo.IsSealed);

        MemberInfo[] members = myClassType.GetMembers();
        Console.WriteLine("\nMembers:");
        foreach (var member in members)
        {
            Console.WriteLine(member.Name + " - " + member.MemberType);
        }

        FieldInfo[] fields = myClassType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
        Console.WriteLine("\nFields:");
        foreach (var field in fields)
        {
            Console.WriteLine(field.Name + " - " + field.FieldType);
        }

        MethodInfo methodInfo = myClassType.GetMethod("GenerateMessage");
        if (methodInfo != null)
        {
            string message = (string)methodInfo.Invoke(myClassInstance, new object[] { "John", "Doe" });
            Console.WriteLine("\nGenerated Message: " + message);
        }

        myClassInstance.DisplayInfo("Alice", 30);
        int sum = myClassInstance.CalculateSum(1, 2, 3, 4, 5);
        Console.WriteLine("Sum: " + sum);

        try
        {
            double avg = myClassInstance.CalculateAverage(new double[] { 3.5, 4.2, 6.7 });
            Console.WriteLine("Average: " + avg);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}