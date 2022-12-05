
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Entities
{
    public class Patent : Edition
    {
        private DateTime _publicationDate;
        private DateTime _appDate;

        public int PatentId { get; set; }

        public int RegNumber { get; set; }

        public DateTime AppDate 
        {
            get
            {
                return _appDate;
            }
            set
            {
                if (value > DateTime.Now)
                {
                    throw new WrongData("Application date cannot exceed the current");
                }
                _appDate = value;
            }
        }

        public DateTime PublicationDate 
        {
            get
            {
                return _publicationDate;
            }
            set
            {
                if (value > DateTime.Now)
                {
                    throw new WrongData("Publication date cannot exceed the current");
                }
                if (value < _appDate)
                {
                    throw new WrongData("The date of publication cannot be earlier than the date of application");
                }
                _publicationDate = value;
            } 
        }

        public List<Author> Authors { get; set; }

        public override string ToString()
        {
            StringBuilder authorList = new StringBuilder();

            foreach (var author in Authors)
            {
                authorList.Append(author.ToString());
            }

            return $"Title: {Title}, " +
                $"City: {PublicationPlace}, " +
                $"Publisher: {PublicationYear}, " +
                $"Register number: {RegNumber}, " +
                $"Application date: { AppDate}, " +
                $"Publication date: { PublicationDate}, " +
                $"Count pages: {CountPages}, " +
                $"Description: {Description} " +
                $"Authors: {authorList}";
        }

        public override string GetTitle()
        {
            return $"«{Title}» от {PublicationDate.ToShortDateString()}";
        }

        public override string GetIdentifier()
        {
            return RegNumber.ToString();
        }
    }
}
