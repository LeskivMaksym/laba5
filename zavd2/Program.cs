using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// Структура для представлення інформації про абонента
struct CableTVSubscriber
{
    public int AccountNumber;
    public string FullName;
    public string Address;
    public string PhoneNumber;
    public string ContractNumber;
    public DateTime ContractDate;
    public bool HasBenefits;
    public SubscriberType Type;
    public string TariffPlan;
}

// Enum для типу абонента
enum SubscriberType
{
    Individual,
    Commercial
}

class Program
{
    static List<CableTVSubscriber> subscribers = new List<CableTVSubscriber>();
    static string filePath = "subscribers.txt";

    static void Main(string[] args)
    {
        LoadSubscribersFromFile();

        while (true)
        {
            Console.WriteLine("Оберіть опцію:");
            Console.WriteLine("1. Додати нового абонента");
            Console.WriteLine("2. Пошук за номером договору");
            Console.WriteLine("3. Пошук за іменем абонента");
            Console.WriteLine("4. Пошук за тарифним планом");
            Console.WriteLine("5. Вийти");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AddSubscriber();
                    break;
                case 2:
                    SearchByContractNumber();
                    break;
                case 3:
                    SearchByName();
                    break;
                case 4:
                    SearchByTariffPlan();
                    break;
                case 5:
                    SaveSubscribersToFile();
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Невірний вибір опції. Спробуйте ще раз.");
                    break;
            }
        }
    }

    static void LoadSubscribersFromFile()
    {
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] data = line.Split('|');
                CableTVSubscriber subscriber = new CableTVSubscriber
                {
                    AccountNumber = int.Parse(data[0]),
                    FullName = data[1],
                    Address = data[2],
                    PhoneNumber = data[3],
                    ContractNumber = data[4],
                    ContractDate = DateTime.Parse(data[5]),
                    HasBenefits = bool.Parse(data[6]),
                    Type = (SubscriberType)Enum.Parse(typeof(SubscriberType), data[7]),
                    TariffPlan = data[8]
                };
                subscribers.Add(subscriber);
            }
        }
    }

    static void SaveSubscribersToFile()
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var subscriber in subscribers)
            {
                writer.WriteLine($"{subscriber.AccountNumber}|{subscriber.FullName}|{subscriber.Address}|{subscriber.PhoneNumber}|{subscriber.ContractNumber}|{subscriber.ContractDate}|{subscriber.HasBenefits}|{subscriber.Type}|{subscriber.TariffPlan}");
            }
        }
    }

    static void AddSubscriber()
    {
        CableTVSubscriber newSubscriber = new CableTVSubscriber();

        Console.Write("Номер рахунку: ");
        newSubscriber.AccountNumber = int.Parse(Console.ReadLine());

        Console.Write("П.І.П. абонента: ");
        newSubscriber.FullName = Console.ReadLine();

        Console.Write("Адреса: ");
        newSubscriber.Address = Console.ReadLine();

        Console.Write("Номер телефону: ");
        newSubscriber.PhoneNumber = Console.ReadLine();

        Console.Write("Номер договору: ");
        newSubscriber.ContractNumber = Console.ReadLine();

        Console.Write("Дата укладення договору (рік-місяць-день): ");
        newSubscriber.ContractDate = DateTime.Parse(Console.ReadLine());

        Console.Write("Наявність пільг (true/false): ");
        newSubscriber.HasBenefits = bool.Parse(Console.ReadLine());

        Console.Write("Тип абонента (Individual/Commercial): ");
        newSubscriber.Type = (SubscriberType)Enum.Parse(typeof(SubscriberType), Console.ReadLine());

        Console.Write("Тарифний план: ");
        newSubscriber.TariffPlan = Console.ReadLine();

        subscribers.Add(newSubscriber);

        Console.WriteLine("Абонент успішно доданий!");
    }


    static void SearchByContractNumber()
    {
        Console.Write("Введіть номер договору для пошуку: ");
        string contractNumber = Console.ReadLine();

        var results = subscribers.Where(subscriber => subscriber.ContractNumber == contractNumber);

        if (results.Any())
        {
            foreach (var subscriber in results)
            {
                PrintSubscriberInfo(subscriber);
            }
        }
        else
        {
            Console.WriteLine("Абонентів з таким номером договору не знайдено.");
        }
    }

    static void SearchByName()
    {
        Console.Write("Введіть ім'я абонента для пошуку: ");
        string name = Console.ReadLine();

        var results = subscribers.Where(subscriber => subscriber.FullName.Contains(name, StringComparison.OrdinalIgnoreCase));

        if (results.Any())
        {
            foreach (var subscriber in results)
            {
                PrintSubscriberInfo(subscriber);
            }
        }
        else
        {
            Console.WriteLine("Абонентів з таким іменем не знайдено.");
        }
    }

    static void SearchByTariffPlan()
    {
        Console.Write("Введіть назву тарифного плану для пошуку: ");
        string tariffPlan = Console.ReadLine();

        var results = subscribers.Where(subscriber => subscriber.TariffPlan.Equals(tariffPlan, StringComparison.OrdinalIgnoreCase));

        if (results.Any())
        {
            foreach (var subscriber in results)
            {
                PrintSubscriberInfo(subscriber);
            }
        }
        else
        {
            Console.WriteLine("Абонентів з таким тарифним планом не знайдено.");
        }
    }

    static void PrintSubscriberInfo(CableTVSubscriber subscriber)
    {
        Console.WriteLine($"Номер рахунку: {subscriber.AccountNumber}");
        Console.WriteLine($"П.І.П. абонента: {subscriber.FullName}");
        Console.WriteLine($"Адреса: {subscriber.Address}");
        Console.WriteLine($"Номер телефону: {subscriber.PhoneNumber}");
        Console.WriteLine($"Номер договору: {subscriber.ContractNumber}");
        Console.WriteLine($"Дата укладення договору: {subscriber.ContractDate}");
        Console.WriteLine($"Наявність пільг: {subscriber.HasBenefits}");
        Console.WriteLine($"Тип абонента: {subscriber.Type}");
        Console.WriteLine($"Тарифний план: {subscriber.TariffPlan}");
        Console.WriteLine();
    }
}
