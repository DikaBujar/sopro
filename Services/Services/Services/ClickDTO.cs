using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class ClickDTO
    {
        public int id { get; set; }
        public string url { get; set; }
        public string userId { get; set; }
        public DateTime createDate { get; set; }

        public int TotalClicks { get; set; }
    }
}