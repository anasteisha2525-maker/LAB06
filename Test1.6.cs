using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab6_Task2;

namespace Test6_2
{
    [TestClass]
    public class DoctorScheduleTests
    {
        [TestMethod]
        public void TestGetTotalVisitors_ForDoctorPetrenko_ReturnsCorrectSum()
        {
            // Arrange (створюємо об'єкт лікаря Петренка та додаємо його прийоми з Main)
            DoctorSchedule doctor = new DoctorSchedule("Петренко", "Кардіолог");
            doctor.AddAppointment("Понеділок", "Ранкова", 14);
            doctor.AddAppointment("Вівторок", "Вечірня", 6);
            doctor.AddAppointment("Середа", "Ранкова", 20);

            // Act (обчислюємо суму: 14 + 6 + 20 = 40)
            int totalVisitors = doctor.GetTotalVisitors();

            // Assert (перевіряємо чи сума дорівнює 40)
            Assert.AreEqual(40, totalVisitors, "Загальна кількість відвідувачів для Петренка обчислена некоректно!");
        }

        [TestMethod]
        public void TestGetLastNameLength_ForDoctorPetrenko_ReturnsCorrectLength()
        {
            // Arrange
            DoctorSchedule doctor = new DoctorSchedule("Петренко", "Кардіолог");

            // Act
            int length = doctor.GetLastNameLength();

            // Assert ("Петренко" містить 8 символів)
            Assert.AreEqual(8, length, "Довжина прізвища лікаря обчислена некоректно!");
        }
    }
}