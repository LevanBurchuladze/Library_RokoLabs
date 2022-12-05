
using System;

namespace Library.Dto
{
    public class PatentDto : EditionDto
    {
        public int PatentId { get; set; }
        public int RegNumber { get; set; }
        public DateTime AppDate { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}
