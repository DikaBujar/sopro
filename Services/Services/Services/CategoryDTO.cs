using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class CategoryDTO
    {
        public int id { get; set; }

        [StringLength(maximumLength: 50)]
        public string name { get; set; }
    }
}