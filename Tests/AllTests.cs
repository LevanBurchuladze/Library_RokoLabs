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
        [TestCase("����������� �����", "������", 2022, 200, "� ����� ��������", "�������� �����", "ISBN 1-56389-668-0")]
        [TestCase("� ���������", "�����-���������", 2022, 200, "� ����� ��������", "�������� �����", "ISBN 1-56389-668-0")]
        [TestCase("���� � �������", "Saint-Peterburg", 2022, 200, "� ����� ��������", "�������� �����", "ISBN 1-56389-668-0")]
        [TestCase("���� � �������", "Saratov", 2022, 200, "� ����� ��������", "�������� �����", "ISBN 1-56389-668-0")]
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
        [TestCase("", "�������", 2001, 200, "� ���-��", "������������", "ISBN 1-56389-668-0")] //������ ��������
        [TestCase("��������", "�������", 2001, 200, "� ���-��", "������������", "ISBN 1-56389-668-0")] //�������� ������ � ��������� �� �������
        [TestCase("��������", "saratov", 2001, 200, "� ���-��", "������������", "ISBN 1-56389-668-0")] //�������� ������ � ��������� �� ����������
        [TestCase("����������� �����", "������", 2022, 200, "� ����� ��������", "�������� �����", "ISBN 1-56389")] //ISBN
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
        [TestCase("�����������", "�����", 2022, 100, "�����", 1, "01.01.2021", "01.01.2022")] // ������ � ���������
        [TestCase("�����", "������", 2010, -100, "����������", 1, "01.01.2021", "01.01.2022")] // ���������� �������� ������������
        [TestCase("���", "�����", 3000, 100, "�����", 1, "01.01.2021", "01.01.2022")] // ��� ���� ��������
        [TestCase("���", "�����", 2021, 100, "�����", 1, "01.01.2023", "01.01.2022")] // ���� ������ ������ ��������
        [TestCase("���", "�����", 2021, 100, "�����", 1, "01.01.2021", "01.01.2023")] // ���� ���������� ���� ��������
        [TestCase("���", "�����", 2021, 100, "�����", 1, "01.01.2022", "01.01.2021")] // ���� ������ ����� ���� ����������
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
        [TestCase("�����-�����", "������", 2022, 100, "� ��������� ��������", 1, "01.01.2021", "01.01.2022")] // ���������� ������
        [TestCase("�����-�����", "Usa", 2022, 100, "� ��������� ��������", 1, "01.01.2021", "01.01.2022")] // ���������� ������
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
        [TestCase("���������", "������", "����", 2021, 505, "� ���� �������� ����", 135, "ISSN 1234-43211", "12,12,2021")] //ISSN ������
        [TestCase("������ ������", "�������", "����", 2000, 12, "� ���� �������� ��������", 164, "ISSN 1234-4321", "12,12,2023")] //���� ���� ��������
        [TestCase("������ ������ �� �����", "������", "����", 2001, 1000, "� ���� �������� ������", 124, "ISSN 0000-9999", "03,06,2021")] //��� ������� = ���� ������

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
        [TestCase("��������", "�����-���������", "����", 2021, 50, "� ���� ��������", 124, "ISSN 1234-4321", "12,12,2021")] //���������� ���������
        [TestCase("������ ������ �� �����", "������", "����", 2001, 1000, "� ���� �������� ������", 124, "ISSN 0000-9999", "03,06,2001")] //���������� ���������
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