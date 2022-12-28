using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Models
{
    public class BookModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string ShortDescription { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }
        public int CategoryInt { get; set; }
        public string Authors { get; set; }
        public string BookUrl { get; set; }
        public bool IsFeatured { get; set; }
    }
}