
using System;
using System.Collections.Generic;

interface IValidator<T>
{
    bool IsValid(T value);
}

interface IValidatorFactory<T>
{
    IValidator<T> CreateValidator();
}

class EmailValidator : IValidator<string>
{
    public bool IsValid(string value)
    {
        return value.Contains("@") && value.Contains(".");
    }
}

class PasswordValidator : IValidator<string>
{
    public bool IsValid(string value)
    {
        return value.Length >= 8;
    }
}

class EmailValidatorFactory : IValidatorFactory<string>
{
    public IValidator<string> CreateValidator()
    {
        return new EmailValidator();
    }
}

class PasswordValidatorFactory : IValidatorFactory<string>
{
    public IValidator<string> CreateValidator()
    {
        return new PasswordValidator();
    }
}

abstract class ValidationRule<T>
{
    public abstract bool Validate(T value);

    public void DisplayValidationResult(T value, bool isValid)
    {
        Console.WriteLine(
            $"Значення: {value} — {(isValid ? "коректне" : "некоректне")}");
    }
}

class MinLengthRule : ValidationRule<string>
{
    public override bool Validate(string value)
    {
        return value.Length >= 5;
    }
}

class ContainsDigitRule : ValidationRule<string>
{
    public override bool Validate(string value)
    {
        foreach (char c in value)
        {
            if (char.IsDigit(c))
                return true;
        }

        return false;
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("=== ІНТЕРФЕЙС IValidatorFactory ===");

        List<IValidatorFactory<string>> factories = new()
        {
            new EmailValidatorFactory(),
            new PasswordValidatorFactory()
        };

        string[] values = { "student@gmail.com", "abc12345" };

        foreach (var factory in factories)
        {
            IValidator<string> validator = factory.CreateValidator();

            foreach (string value in values)
            {
                Console.WriteLine(
                    $"{validator.GetType().Name}: {value} — " +
                    (validator.IsValid(value) ? "коректне" : "некоректне"));
            }

            Console.WriteLine();
        }

        Console.WriteLine("=== АБСТРАКТНИЙ КЛАС ValidationRule ===");

        List<ValidationRule<string>> rules = new()
        {
            new MinLengthRule(),
            new ContainsDigitRule()
        };

        string[] testValues = { "abc", "hello5" };

        foreach (var rule in rules)
        {
            Console.WriteLine($"\nПравило: {rule.GetType().Name}");

            foreach (string value in testValues)
            {
                bool result = rule.Validate(value);
                rule.DisplayValidationResult(value, result);
            }
        }
    }
}
