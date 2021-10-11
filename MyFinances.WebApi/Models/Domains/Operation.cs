using System;
using System.Collections.Generic;

#nullable disable

namespace MyFinances.WebApi.Models.Domains
{
    public partial class Operation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
