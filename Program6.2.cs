using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab6_Task2
{
    // Інтерфейс із абстрактними методами
    public interface IMedicalService
    {
        int GetTotalVisitors();
        void DisplayMinVisitorsAppointment();
    }

    // Базовий клас "Лікар"
    public class Doctor
    {
        public string LastName { get; set; } = string.Empty;
        public string Speciality { get; set; } = string.Empty;

        public Doctor() { }

        public Doctor(string lastName, string speciality)
        {
            LastName = lastName;
            Speciality = speciality;
        }

        public int GetLastNameLength()
        {
            return LastName.Length;
        }
    }

    // Клас для опису одного прийому
    public class Appointment
    {
        public string Day { get; set; } = string.Empty;
        public string Shift { get; set; } = string.Empty;
        public int VisitorsCount { get; set; }

        public Appointment(string day, string shift, int visitorsCount)
        {
            Day = day;
            Shift = shift;
            VisitorsCount = visitorsCount;
        }
    }

    // Похідний клас, що реалізує інтерфейс
    public class DoctorSchedule : Doctor, IMedicalService
    {
        private List<Appointment> appointments = new List<Appointment>();

        public DoctorSchedule(string lastName, string speciality)
            : base(lastName, speciality) { }

        public void AddAppointment(string day, string shift, int visitors)
        {
            appointments.Add(new Appointment(day, shift, visitors));
        }

        public int GetTotalVisitors()
        {
            return appointments.Sum(a => a.VisitorsCount);
        }

        public void DisplayMinVisitorsAppointment()
        {
            if (appointments.Count == 0)
            {
                Console.WriteLine("Дані про прийоми відсутні.");
                return;
            }

            var minApp = appointments.OrderBy(a => a.VisitorsCount).First();
            Console.WriteLine($"Прийом з мін. кількістю відвідувачів ({minApp.VisitorsCount}): {minApp.Day}, зміна: {minApp.Shift}");
        }

        public void PrintFullReport()
        {
            Console.WriteLine($"\n================ ІНФОРМАЦІЯ ПРО ЛІКАРЯ ================");
            Console.WriteLine($"Лікар: {LastName} | Фах: {Speciality}");
            Console.WriteLine($"Довжина прізвища: {GetLastNameLength()} символів");
            Console.WriteLine("-------------------------------------------------------");

            if (appointments.Count == 0)
            {
                Console.WriteLine("Ще не додано жодного прийому.");
            }
            else
            {
                Console.WriteLine("{0,-15} | {1,-12} | {2,-15}", "День", "Зміна", "Кількість");
                Console.WriteLine("-------------------------------------------------------");

                foreach (var app in appointments)
                {
                    Console.WriteLine("{0,-15} | {1,-12} | {2,-15}", app.Day, app.Shift, app.VisitorsCount);
                }

                Console.WriteLine("-------------------------------------------------------");
                Console.WriteLine($"Загальна кількість відвідувачів: {GetTotalVisitors()}");
                DisplayMinVisitorsAppointment();
            }
            Console.WriteLine("=======================================================\n");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Створюємо початкового лікаря з попередніми даними без зайвих виводів у консоль
            DoctorSchedule doctor = new DoctorSchedule("Петренко", "Кардіолог");
            doctor.AddAppointment("Понеділок", "Ранкова", 14);
            doctor.AddAppointment("Вівторок", "Вечірня", 6);
            doctor.AddAppointment("Середа", "Ранкова", 20);

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n================ КОНСОЛЬНЕ МЕНЮ ================");
                Console.WriteLine("1. Додати новий прийом");
                Console.WriteLine("2. Вивести повний звіт та розрахунки");
                Console.WriteLine("3. Вихід");
                Console.Write("Оберіть пункт меню (1-3): ");

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        Console.Write("Введіть день тижня (наприклад, Четвер): ");
                        string day = Console.ReadLine() ?? "День";

                        Console.Write("Введіть зміну (Ранкова / Вечірня): ");
                        string shift = Console.ReadLine() ?? "Зміна";

                        Console.Write("Введіть кількість відвідувачів: ");
                        if (int.TryParse(Console.ReadLine(), out int visitors))
                        {
                            doctor.AddAppointment(day, shift, visitors);
                        }
                        else
                        {
                            Console.WriteLine(" Помилка: введіть числове значення для кількості відвідувачів.");
                        }
                        break;

                    case "2":
                        doctor.PrintFullReport();
                        break;

                    case "3":
                        exit = true;
                        Console.WriteLine("Роботу програми завершено.");
                        break;

                    default:
                        Console.WriteLine(" Невірний вибір. Будь ласка, оберіть пункт від 1 до 3.");
                        break;
                }
            }
        }
    }
}