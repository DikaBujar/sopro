using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTO
{
    public class BookmarksDTO
    {
        public int id { get; set; }

        [StringLength(maximumLength: 500)]
        public string url { get; set; }

        public string shortdescription { get; set; }

        public DateTime createdate { get; set; }
    }
}