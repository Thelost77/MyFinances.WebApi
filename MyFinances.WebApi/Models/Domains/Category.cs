using System;
using System.Collections.Generic;

#nullable disable

namespace MyFinances.WebApi.Models.Domains
{
    public partial class Category
    {
        public Category()
        {
            Operations = new HashSet<Operation>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Operation> Operations { get; set; }
    }
}
