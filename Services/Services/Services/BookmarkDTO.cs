using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class BookmarkDTO
    {
        public int? id { get; set; }

        [StringLength(maximumLength: 500)]
        public string url { get; set; }

        public string shortdescription { get; set; }
        public string userID { get; set; }

        public virtual Category category { get; set; }

        public DateTime createdate { get; set; }
    }
}