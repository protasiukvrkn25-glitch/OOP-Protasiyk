using System;

class PaymentMethod
{
    public string Name { get; set; }
    public bool IsActive { get; set; }

    public PaymentMethod(string name, bool isActive)
    {
        Name = name;
        IsActive = isActive;
    }

    public virtual void ProcessPayment(decimal amount)
    {
        Console.WriteLine($"Оплата {amount} грн через {Name}");
    }

    public string GetMethodType()
    {
        return "Загальний спосіб оплати";
    }
}

class CreditCard : PaymentMethod
{
    public string CardNumber { get; set; }
    public string ExpiryDate { get; set; }

    public CreditCard(
        string name,
        bool isActive,
        string cardNumber,
        string expiryDate)
        : base(name, isActive)
    {
        CardNumber = cardNumber;
        ExpiryDate = expiryDate;
    }

    public override void ProcessPayment(decimal amount)
    {
        Console.WriteLine(
            $"Оплата {amount} грн карткою {CardNumber}");
    }

    public void AuthorizeTransaction()
    {
        Console.WriteLine("Транзакцію картки авторизовано.");
    }

    public new string GetMethodType()
    {
        return "Кредитна картка";
    }
}

class PayPal : PaymentMethod
{
    public string Email { get; set; }

    public PayPal(
        string name,
        bool isActive,
        string email)
        : base(name, isActive)
    {
        Email = email;
    }

    public override void ProcessPayment(decimal amount)
    {
        Console.WriteLine(
            $"Оплата {amount} грн через PayPal ({Email})");
    }

    public void SendInvoice()
    {
        Console.WriteLine("Рахунок PayPal надіслано.");
    }
}

class Program
{
    static void Main()
    {
        PaymentMethod payment = new PaymentMethod(
            "PaymentMethod", true);

        CreditCard card = new CreditCard(
            "Visa",
            true,
            "**** 1234",
            "12/28");

        PayPal paypal = new PayPal(
            "PayPal",
            true,
            "student@example.com");

        Console.WriteLine("=== Звичайний спосіб оплати ===");
        payment.ProcessPayment(500);

        Console.WriteLine("\n=== CreditCard ===");
        card.ProcessPayment(1000);
        card.AuthorizeTransaction();

        Console.WriteLine("\n=== PayPal ===");
        paypal.ProcessPayment(750);
        paypal.SendInvoice();

        Console.WriteLine("\n=== Поліморфізм ===");

        PaymentMethod p1 = card;
        PaymentMethod p2 = paypal;

        p1.ProcessPayment(1200);
        p2.ProcessPayment(900);

        Console.WriteLine("\n=== Демонстрація new ===");

        Console.WriteLine("CreditCard: " + card.GetMethodType());

        PaymentMethod baseReference = card;
        Console.WriteLine(
            "PaymentMethod: " + baseReference.GetMethodType());
    }
}