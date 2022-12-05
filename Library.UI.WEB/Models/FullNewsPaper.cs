
using Library.Entities;
using System.Collections.Generic;

namespace Library.UI.WEB.Models
{
    public class FullNewsPaper
    {
        public NewsPaper newsPaper { get; set; }
        public List<NewsPaper> listNewsPapers { get; set; }
    }
}
