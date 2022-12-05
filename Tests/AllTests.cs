using Library.Entities;
using NUnit.Framework;
using System;
using System.Diagnostics;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("Капитанская дочка", "Москва", 2022, 200, "о дочке капитана", "Лабиринт Пресс", "ISBN 1-56389-668-0")]
        [TestCase("У Лукоморья", "Санкт-Петербург", 2022, 200, "о дочке капитана", "Лабиринт Пресс", "ISBN 1-56389-668-0")]
        [TestCase("Маша и Медведь", "Saint-Peterburg", 2022, 200, "о дочке капитана", "Лабиринт Пресс", "ISBN 1-56389-668-0")]
        [TestCase("Маша и Медведь", "Saratov", 2022, 200, "о дочке капитана", "Лабиринт Пресс", "ISBN 1-56389-668-0")]
        public void CheckBookTrueField(string title, string pubPlace, int pubYear, int countPages, string description, string pubHouse, string ISBN)
        {
            Assert.DoesNotThrow(() =>
            {
                Book book = new Book()
                {
                    Title = title,
                    Type = 1,
                    PublicationPlace = pubPlace,
                    PublicationHouse = pubHouse,
                    PublicationYear = pubYear,
                    CountPages = countPages,
                    Description = description,
                    ISBN = ISBN,
                };
            });
        }

        [Test]
        [TestCase("", "Саратов", 2001, 200, "о чем-то", "Издательство", "ISBN 1-56389-668-0")] //пустое название
        [TestCase("Название", "саратов", 2001, 200, "о чем-то", "Издательство", "ISBN 1-56389-668-0")] //название города с маленькой на русском
        [TestCase("Название", "saratov", 2001, 200, "о чем-то", "Издательство", "ISBN 1-56389-668-0")] //название города с маленькой на английском
        [TestCase("Капитанская дочка", "Москва", 2022, 200, "о дочке капитана", "Лабиринт Пресс", "ISBN 1-56389")] //ISBN
        public void CheckBookFalseField(string title, string pubPlace, int pubYear, int countPages, string description, string pubHouse, string ISBN)
        {
            try
            {
                Book book = new Book()
                {
                    Title = title,
                    Type = 1,
                    PublicationPlace = pubPlace,
                    PublicationHouse = pubHouse,
                    PublicationYear = pubYear,
                    CountPages = countPages,
                    Description = description,
                    ISBN = ISBN,
                };
            }
            catch (WrongData e)
            {
                Assert.Pass(e.Message);
            }
            Assert.Fail();
        }

        [Test]
        [TestCase("Короновирус", "китай", 2022, 100, "вирус", 1, "01.01.2021", "01.01.2022")] // страна с маленькой
        [TestCase("Аниме", "Япония", 2010, -100, "мультфильм", 1, "01.01.2021", "01.01.2022")] // количество страницу отрицательно
        [TestCase("Чай", "Индия", 3000, 100, "питье", 1, "01.01.2021", "01.01.2022")] // год выше текущего
        [TestCase("Чай", "Индия", 2021, 100, "питье", 1, "01.01.2023", "01.01.2022")] // дата заявки больше текущего
        [TestCase("Чай", "Индия", 2021, 100, "питье", 1, "01.01.2021", "01.01.2023")] // дата публикации выше текущего
        [TestCase("Чай", "Индия", 2021, 100, "питье", 1, "01.01.2022", "01.01.2021")] // дата заявки позде даты публикации
        public void CheckPatentFalseField(string title, string pubPlace, int pubYear, int countPages, string description,
            int regNumber, DateTime appDate, DateTime pubDate)
        {
            try
            {
                Patent patent = new Patent()
                {
                    Title = title,
                    Type = 3,
                    PublicationPlace = pubPlace,
                    RegNumber = regNumber,
                    AppDate = appDate,
                    PublicationDate = pubDate,
                    PublicationYear = pubYear,
                    CountPages = countPages,
                    Description = description,
                };
            }
            catch (WrongData e)
            {
                Assert.Pass(e.Message);
            }
            Assert.Fail();
        }

        [Test]
        [TestCase("Микро-Челик", "Россия", 2022, 100, "О маленьком человеке", 1, "01.01.2021", "01.01.2022")] // правильный шаблон
        [TestCase("Микро-Челик", "Usa", 2022, 100, "О маленьком человеке", 1, "01.01.2021", "01.01.2022")] // правильный шаблон
        public void CheckPatentTrueField(string title, string pubPlace, int pubYear, int countPages, string description,
    int regNumber, DateTime appDate, DateTime pubDate)
        {
            Assert.DoesNotThrow(() =>
            {
                Patent patent = new Patent()
                {
                    Title = title,
                    Type = 3,
                    PublicationPlace = pubPlace,
                    RegNumber = regNumber,
                    AppDate = appDate,
                    PublicationDate = pubDate,
                    PublicationYear = pubYear,
                    CountPages = countPages,
                    Description = description,
                };
            });
        }

        [Test]
        [TestCase("Комерсант", "Москва", "ФГУМ", 2021, 505, "о всех новостях мира", 135, "ISSN 1234-43211", "12,12,2021")] //ISSN формат
        [TestCase("Желтая пресса", "Саратов", "ФГУС", 2000, 12, "о всех новостях саратова", 164, "ISSN 1234-4321", "12,12,2023")] //дата выше текущего
        [TestCase("Москва слезам не верит", "Москва", "ФГУМ", 2001, 1000, "о всех новостях москвы", 124, "ISSN 0000-9999", "03,06,2021")] //год выпуска = году газеты

        public void CheckNewsPaperFalseField(string title, string pubPlace, string pubHouse, int pubYear, int countPages, string description,
            int number,string issn, DateTime date)
        {
            try
            {
                NewsPaper newsPaper = new NewsPaper()
                {
                    Title = title,
                    Type = 2,
                    PublicationPlace = pubPlace,
                    PublicationHouse = pubHouse,
                    PublicationYear = pubYear,
                    CountPages = countPages,
                    Description = description,
                    Number = number,
                    ISSN = issn,
                    Date = date
                };
            }
            catch (WrongData e)
            {
                Assert.Pass(e.Message);
            }
            Assert.Fail();
        }

        [Test]
        [TestCase("Известия", "Санкт-Петербург", "ФГУП", 2021, 50, "о всех новостях", 124, "ISSN 1234-4321", "12,12,2021")] //правильное написание
        [TestCase("Москва слезам не верит", "Москва", "ФГУМ", 2001, 1000, "о всех новостях москвы", 124, "ISSN 0000-9999", "03,06,2001")] //правильное написание
        public void CheckNewsPaperTrueField(string title, string pubPlace, string pubHouse, int pubYear, int countPages, string description,
    int number, string issn, DateTime date)
        {
            Assert.DoesNotThrow(() =>
            {
                NewsPaper newsPaper = new NewsPaper()
                {
                    Title = title,
                    Type = 2,
                    PublicationPlace = pubPlace,
                    PublicationHouse = pubHouse,
                    PublicationYear = pubYear,
                    CountPages = countPages,
                    Description = description,
                    Number = number,
                    ISSN = issn,
                    Date = date
                };
            });
        }
    }
}