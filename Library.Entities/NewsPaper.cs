
using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Entities
{
    public class NewsPaper : Edition
    {
        private string _issn;
        private string _publicationHouse;
        private DateTime _date;

        public int NewsPaperId { get; set; }

        [StringLength(300,ErrorMessage = "Length is big. Max lenght is 300")]
        public string PublicationHouse
        {
            get
            {
                return _publicationHouse;
            }
            set
            {
                _publicationHouse = value;
            }
        }

        public int Number { get; set; }

        public DateTime Date 
        {
            get
            {
                return _date;
            }
            set
            {
                if (value > DateTime.Now.AddYears(1))
                {
                    throw new WrongData("The date of issue of the newspaper cannot exceed the current year");
                }
                _date = value;
            } 
        }

        [RegularExpression(@"(ISSN [0-9]{4}[-][0-9]{4})\b", ErrorMessage = "This ISSN format is not available")]
        public string ISSN 
        {
            get
            {
                return _issn;
            }
            set 
            {
                _issn = value;
            }
        }

        public override string ToString()
        {
            return $"Title: {Title}, " +
                $"City: {PublicationPlace}, " +
                $"Publisher: {PublicationHouse}, " +
                $"Year of publishing: {PublicationYear}, " +
                $"Count pages: {CountPages}, " +
                $"Description: {Description} " +
                $"Register number: {Number}, " +
                $"Date: {Date}, " +
                $"ISBN: {ISSN}";
        }

        public override string GetTitle()
        {
            if (Number != 0) 
            { 
                return $"{Title} №{Number}/{PublicationYear}";
            }
            else
            {
                return $"{Title} {PublicationYear}";
            }
        }

        public override string GetIdentifier()
        {
            return ISSN;
        }
    }
}
