using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab6_Task1
{
    // Інтерфейс для бази даних студентів
    interface IStudentDatabase
    {
        void AddStudent(string lastName, string birthDate, string birthPlace);
        void SortAndDisplay();
        void DisplayTwins();
    }

    // Клас студента
    class Student : IStudentDatabase
    {
        public string LastName { get; set; } = string.Empty;
        public string BirthDate { get; set; } = string.Empty; // Формат ДД:ММ:РР
        public string BirthPlace { get; set; } = string.Empty;

        private List<Student> students;

        public Student()
        {
            students = new List<Student>
            {
                new Student("Коваленко", "12:05:04", "Київ"),
                new Student("Петренко", "03:11:03", "Львів"),
                new Student("Шевченко", "12:05:04", "Харків"),
                new Student("Мельник", "21:01:05", "Одеса"),
                new Student("Бойко", "03:11:03", "Дніпро")
            };
        }

        public Student(string lastName, string birthDate, string birthPlace)
        {
            LastName = lastName;
            BirthDate = birthDate;
            BirthPlace = birthPlace;
            students = new List<Student>(); // Ініціалізуємо порожнім списком для цього конструктора
        }

        // Метод додавання нового студента до бази
        public void AddStudent(string lastName, string birthDate, string birthPlace)
        {
            students.Add(new Student(lastName, birthDate, birthPlace));
            Console.WriteLine("✅ Студента успішно додано до бази!");
        }

        // Допоміжний метод для переведення рядка ДД:ММ:РР у DateTime для сортування
        private DateTime ParseDate(string dateStr)
        {
            var parts = dateStr.Split(':');
            int day = int.Parse(parts[0]);
            int month = int.Parse(parts[1]);
            int year = int.Parse(parts[2]);
            int fullYear = year < 50 ? 2000 + year : 1900 + year;
            return new DateTime(fullYear, month, day);
        }

        // Реалізація методу інтерфейсу: сортування за зростанням дат народження та виведення таблиці
        public void SortAndDisplay()
        {
            if (students.Count == 0)
            {
                Console.WriteLine("База даних студентів порожня.");
                return;
            }

            var sorted = students.OrderBy(s => ParseDate(s.BirthDate)).ToList();

            Console.WriteLine("\n--- ТАБЛИЦЯ: Всі студенти за зростанням дати народження ---");
            Console.WriteLine("{0,-15} | {1,-12} | {2,-15}", "Прізвище", "Дата народж.", "Місце народж.");
            Console.WriteLine(new string('-', 47));

            foreach (var st in sorted)
            {
                Console.WriteLine("{0,-15} | {1,-12} | {2,-15}", st.LastName, st.BirthDate, st.BirthPlace);
            }
            Console.WriteLine();
        }

        // Реалізація методу інтерфейсу: виведення «двійнят» (співпадають день та місяць)
        public void DisplayTwins()
        {
            var twins = students
                .GroupBy(s => s.BirthDate.Substring(0, 5)) // Групуємо лише за ДД:ММ
                .Where(g => g.Count() > 1)
                .SelectMany(g => g)
                .ToList();

            Console.WriteLine("\n--- ТАБЛИЦЯ: «Двійнята» (однакові день і місяць народження) ---");
            if (twins.Count == 0)
            {
                Console.WriteLine("Студентів з однаковою датою народження не знайдено.");
                return;
            }

            Console.WriteLine("{0,-15} | {1,-12} | {2,-15}", "Прізвище", "Дата народж.", "Місце народж.");
            Console.WriteLine(new string('-', 47));

            foreach (var st in twins)
            {
                Console.WriteLine("{0,-15} | {1,-12} | {2,-15}", st.LastName, st.BirthDate, st.BirthPlace);
            }
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            IStudentDatabase db = new Student();

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n================ КОНСОЛЬНЕ МЕНЮ ================");
                Console.WriteLine("1. Додати нового студента");
                Console.WriteLine("2. Вивести студентів (відсортованих за датою народження)");
                Console.WriteLine("3. Вивести таблицю «Двійнята» (з однаковими днями і місяцями)");
                Console.WriteLine("4. Вихід");
                Console.Write("Оберіть пункт меню (1-4): ");

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        Console.Write("Введіть прізвище студента: ");
                        string lastName = Console.ReadLine() ?? "Невідомо";

                        Console.Write("Введіть дату народження (у форматі ДД:ММ:РР, наприклад 15:08:03): ");
                        string birthDate = Console.ReadLine() ?? "01:01:00";

                        Console.Write("Введіть місце народження: ");
                        string birthPlace = Console.ReadLine() ?? "Невідомо";

                        db.AddStudent(lastName, birthDate, birthPlace);
                        break;

                    case "2":
                        db.SortAndDisplay();
                        break;

                    case "3":
                        db.DisplayTwins();
                        break;

                    case "4":
                        exit = true;
                        Console.WriteLine("Роботу програми завершено.");
                        break;

                    default:
                        Console.WriteLine(" Невірний вибір. Будь ласка, оберіть пункт від 1 до 4.");
                        break;
                }
            }
        }
    }
}