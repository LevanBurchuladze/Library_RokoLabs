using Library.Entities;
using Library.Interfaces.Service;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Library.UI
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IEditionService _editionService;

        public Worker(ILogger<Worker> logger, IEditionService editionService)
        {
            _editionService = editionService;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            ShowInfo();
            int number;
            do
            {
                number = Convert.ToInt32(Console.ReadLine());

                switch (number)
                {
                    case 1:
                        Add();
                        break;
                    case 2:
                        Delete();
                        break;
                    case 3:
                        Show();
                        break;
                    case 4:
                        FindByTitle();
                        break;
                    case 5:
                        SortByYear();
                        break;
                    case 6:
                        FindBooksByAuthor();
                        break;
                    case 7:
                        FindPatentsByAuthor();
                        break;
                    case 8:
                        FindAllByAuthor();
                        break;
                    case 9:
                        FindBooksByPubHouseGroupBy();
                        break;
                    case 10:
                        SortByDate();
                        break;
                    case 0:
                        return Task.CompletedTask;
                    default:
                        Console.WriteLine("There is no such option, choose another!");
                        break;
                }

                ShowInfo();
            } while (number != 0);

            return Task.CompletedTask;
        }

        private void Add()
        {
            Console.WriteLine("To add a specific object, enter: ");
            Console.WriteLine("1 - Book:");
            Console.WriteLine("2 - Newspaper:");
            Console.WriteLine("3 - Patent:");
            int type = Convert.ToInt32(Console.ReadLine());
            switch (type)
            {
                case 1:
                    AddBook();
                    break;
                case 2:
                    AddNewsPaper();
                    break;
                case 3:
                    AddPatent();
                    break;
                default:
                    Console.WriteLine("Incorrect object type entered");
                    break;
            }
        }

        private void AddPatent()
        {
            try
            {
                Console.Write("Enter patent title: ");
                string title = Console.ReadLine();
                Console.Write("Enter the place of publication (city): ");
                string publicationPlace = Console.ReadLine();
                Console.Write("Enter registration number: ");
                int regNumber = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter the application submission date: ");
                DateTime appDate = Convert.ToDateTime(Console.ReadLine());
                Console.Write("Enter publication date: ");
                DateTime publicationDate = Convert.ToDateTime(Console.ReadLine());
                Console.Write("Enter year of publication: ");
                int publicationYear = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter number of pages: ");
                int countPages = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter description: ");
                string description = Console.ReadLine();

                Console.WriteLine("Enter the authors (one at a time), to stop enter an empty line: ");

                List<Author> authors = new List<Author>();
                string tempAuthor;
                do
                {
                    tempAuthor = Console.ReadLine();
                    if(tempAuthor != "")
                    {
                        string[] author = tempAuthor.Split();
                        authors.Add(new Author { FirstName = author[0], SecondName = author[1] });
                    }
                } while (tempAuthor !="");

                Patent patent = new Patent()
                {
                    Title = title,
                    Type = 3,
                    PublicationPlace = publicationPlace,
                    RegNumber = regNumber,
                    AppDate = appDate,
                    PublicationDate = publicationDate,
                    PublicationYear = publicationYear,
                    CountPages = countPages,
                    Description = description,
                    Authors = authors
                };
                _editionService.InsertEdition(patent);
            }
            catch (FormatException)
            {
                Console.Clear();
                Console.WriteLine("Wrong data format entered");
                Console.WriteLine();
            }
            catch (WrongData e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
                Console.WriteLine();
            }
        }

        private void AddNewsPaper()
        {
            try
            {
                Console.Write("Enter newspaper name: ");
                string title = Console.ReadLine();
                Console.Write("Enter the place of publication (city): ");
                string publicationPlace = Console.ReadLine();
                Console.Write("Enter publisher: ");
                string publicationHouse = Console.ReadLine();
                Console.Write("Enter year of publication: ");
                int publicationYear = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter number of pages: ");
                int countPages = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter note: ");
                string description = Console.ReadLine();
                Console.Write("Enter number:");
                int number = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter ISSN: ");
                string ISSN = Console.ReadLine();
                Console.Write("Enter Date: ");
                DateTime date = Convert.ToDateTime(Console.ReadLine());

                NewsPaper newsPaper = new NewsPaper()
                {
                    Title = title,
                    Type = 2,
                    PublicationPlace = publicationPlace,
                    PublicationHouse = publicationHouse,
                    PublicationYear = publicationYear,
                    CountPages = countPages,
                    Description = description,
                    Number = number,
                    ISSN = ISSN,
                    Date = date
                };
                _editionService.InsertEdition(newsPaper);
            }
            catch (FormatException)
            {
                Console.Clear();
                Console.WriteLine("Wrong data format entered");
                Console.WriteLine();
            }
            catch (WrongData e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
                Console.WriteLine();
            }
        }

        private void AddBook()
        {
            try
            {
                Console.Write("Enter book title: ");
                string title = Console.ReadLine();
                Console.Write("Enter the place of publication (city): ");
                string publicationPlace = Console.ReadLine();
                Console.Write("Enter publisher: ");
                string publicationHouse = Console.ReadLine();
                Console.Write("Enter year of publication: ");
                int publicationYear = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter number of pages: ");
                int countPages = Convert.ToInt32(Console.ReadLine());
                Console.Write(" Enter description: ");
                string description = Console.ReadLine();
                Console.Write("ENTER ISBN: ");
                string ISBN = Console.ReadLine();
                Console.WriteLine("Enter the authors (one at a time), to stop enter an empty line: ");

                List<Author> authors = new List<Author>();
                string tempAuthor;
                do
                {
                    tempAuthor = Console.ReadLine();
                    if (tempAuthor != "")
                    {
                        string[] author = tempAuthor.Split();
                        authors.Add(new Author { FirstName = author[0], SecondName = author[1] });
                    }

                } while (tempAuthor !="");

                Book book = new Book()
                {
                    Title = title,
                    Type = 1,
                    PublicationPlace = publicationPlace,
                    PublicationHouse = publicationHouse,
                    PublicationYear = publicationYear,
                    CountPages = countPages,
                    Description = description,
                    ISBN = ISBN,
                    Authors = authors
                };
                _editionService.InsertEdition(book);
            }
            catch (FormatException)
            {
                Console.Clear();
                Console.WriteLine("Wrong data format entered");
                Console.WriteLine();
            }
            catch (WrongData e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
                Console.WriteLine();
            }
        }

        private void Delete()
        {
            Console.WriteLine("Enter NoteId to delete entry");
            int EditionId = Convert.ToInt32(Console.ReadLine());
            _editionService.DeleteEdition(EditionId);
        }

        private void Show()
        {
            List<Edition> catalog = _editionService.GetEditions();
            foreach (var item in catalog)
            {
                Console.WriteLine(item);
            }
        }

        private void SortByDate()
        {
            List<Edition> editions = _editionService.GetEditionsByDate();
            List<Edition> catalog = _editionService.GetEditions();
            List<Edition> SortByDateCatalog = new List<Edition>();
            foreach (var edition in editions)
            {
                foreach (var cat in catalog)
                {
                    if(edition.EditionId == cat.EditionId)
                    {
                        SortByDateCatalog.Add(cat);
                    }

                }
            }
            foreach (var item in SortByDateCatalog)
            {
                Console.WriteLine(item);
            }
        }

        private void FindBooksByPubHouseGroupBy()
        {
            Console.WriteLine("Enter the publisher with the specified character set");
            string pubHouse = Console.ReadLine();
            List<Book> books = _editionService.GetBooksByPubHouse(pubHouse);
            foreach (var book in books)
            {
                Console.WriteLine(book);
            }
        }

        private void FindAllByAuthor()
        {
            Console.WriteLine("Enter author: ");
            string author = Console.ReadLine();
            string[] authorField = author.Split();
            Author newAuthor = new Author() { FirstName = authorField[0], SecondName = authorField[1] };

            List<Edition> editions = new List<Edition>(_editionService.GetPatentsByAuthor(newAuthor));
            editions.AddRange(_editionService.GetBooksByAuthor(newAuthor));

            foreach (var edition in editions)
            {
                Console.WriteLine(edition);
            }
        }

        private void FindPatentsByAuthor()
        {
            Console.WriteLine("Enter author:");
            string author = Console.ReadLine();
            string[] authorField = author.Split();
            Author newAuthor = new Author() { FirstName = authorField[0], SecondName = authorField[1] };
            List<Patent> patents = _editionService.GetPatentsByAuthor(newAuthor);

            foreach (var patent in patents)
            {
                Console.WriteLine(patent);
            }
        }

        private void FindBooksByAuthor()
        {
            Console.WriteLine("Enter author:");
            string author = Console.ReadLine();
            string[] authorField = author.Split();
            Author newAuthor = new Author() { FirstName = authorField[0], SecondName = authorField[1] };
            List<Book> books = _editionService.GetBooksByAuthor(newAuthor);

            foreach (var book in books)
            {
                Console.WriteLine(book);
            }
        }

        private void SortByYear()
        {
            Console.WriteLine("Enter:");
            Console.WriteLine("true - to sort in ascending order:");
            Console.WriteLine("false - descending:");
            bool howSort = Convert.ToBoolean(Console.ReadLine());

            List<Edition> SortByDateCatalog = new List<Edition>();
            List<Edition> editions = new List<Edition>();
            List<Edition> catalog = new List<Edition>();
            if (howSort)
            {
                editions = _editionService.GetEditionsByDate();
                catalog = _editionService.GetEditions();
            }
            else
            {
                editions = _editionService.GetEditionsByDateDesc();
                catalog = _editionService.GetEditions();
            }
            foreach (var edition in editions)
            {
                foreach (var cat in catalog)
                {
                    if (edition.EditionId == cat.EditionId)
                    {
                        SortByDateCatalog.Add(cat);
                        break;
                    }

                }
            }
            foreach (var item in SortByDateCatalog)
            {
                Console.WriteLine(item);
            }
        }

        private void FindByTitle()
        {
            Console.Write("Введите название:");
            string title = Console.ReadLine();
            List<Edition> catalog = _editionService.GetEditionsByName(title);
            foreach (var item in catalog)
            {
                Console.WriteLine(item);
            }
        }

        private static void ShowInfo()
        {
            Console.WriteLine("Select an item:");
            Console.WriteLine("1: Adding entries to the directory");
            Console.WriteLine("2: Removing entries from the directory");
            Console.WriteLine("3: Catalog browsing");
            Console.WriteLine("4: Search editions by title");
            Console.WriteLine("5: Sort by year of manufacture in forward and reverse order");
            Console.WriteLine("6: Search for all books by a given author (including co-authors)");
            Console.WriteLine("7: Search for all patents of a given inventor (including co-authorship)");
            Console.WriteLine("8: Search for all books and patents of a given author (including co-authors)");
            Console.WriteLine("9: Display all books whose publishing name begins with the specified character set, grouped by publisher");
            Console.WriteLine("10: Grouping records by year of publication");
            Console.WriteLine("0: Exit the application");
        }
    }
}
