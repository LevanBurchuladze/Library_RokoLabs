
using System;

namespace Library.Dto
{
    public class NewsPaperDto : EditionDto
    {
        public int NewsPaperId { get; set; }
        public string PublicationHouse { get; set; }
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public string ISSN { get; set; }
    }
}
