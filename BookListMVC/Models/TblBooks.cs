using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookListMVC.Models
{
    public partial class TblBooks
    {
        public uint Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        public string Isbn { get; set; }
    }
}
