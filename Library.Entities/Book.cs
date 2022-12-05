
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Entities
{
    public class Book : Edition
    {
        private string _publicationHouse;
        private string _isbn;

        public int BookId { get; set; }

        [StringLength(300, ErrorMessage = "Length is big. Max lenght is 300")]
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

        [RegularExpression(@"ISBN\x20(?=.{13})\d{1,5}([- ])\d{1,7}\1\d{1,6}\1(\d|X)", ErrorMessage = "ISBN spelled wrong")]
        public string ISBN 
        {
            get
            {
                return _isbn;
            }
            set
            {
                _isbn = value;
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
                $"Publisher: {PublicationHouse}, " +
                $"Year of publishing: {PublicationYear}, " +
                $"Count pages: {CountPages}, " +
                $"Description: {Description} " +
                $"ISBN: {ISBN} " +
                $"Authors: {authorList}";
        }

        public override string GetTitle()
        {
            StringBuilder authorList = new StringBuilder();

            foreach (var author in Authors)
            {
                authorList.Append(author.ToString() + " ");
            }

            return $"{authorList} - {Title}({PublicationYear})";
        }

        public override string GetIdentifier()
        {
            return ISBN;
        }
    }
}
