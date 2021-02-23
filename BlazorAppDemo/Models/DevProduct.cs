using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAppDemo.Models
{
    public class DevProduct
    {
        [Key]
        public string productID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public ICollection<DevProductComments> Comments { get; set; }

        public DevProduct()
        {
            productID = Guid.NewGuid().ToString();
        }
    }

    public class DevProductComments
    {
        [Key]
        public string commentID { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public bool Approved { get; set; }

        public DevProductComments()
        {
            commentID = Guid.NewGuid().ToString();
        }
    }
}
