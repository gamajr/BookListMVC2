using System;
using System.Collections.Generic;

namespace BookListMVC.Models
{
    public partial class TblBooks
    {
        public uint Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Isbn { get; set; }
    }
}
