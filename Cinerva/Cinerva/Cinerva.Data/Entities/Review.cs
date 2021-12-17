using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinerva.Data.Entities
{
    public class Review
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int PropertyId { get; set; }
        public Property Property { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public DateTime ReviewDate { get; set; }

    }
}
