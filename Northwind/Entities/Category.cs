using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Northwind.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }

        [StringLength(15)]
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
