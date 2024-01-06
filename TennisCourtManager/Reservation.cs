using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisCourtManager
{
    internal class Reservation
    {
        public int Id { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public int Court { get; set; }
        public int Customer { get; set; }
        public int Cost { get; set; }

        public Reservation()
        {
            
        }
    }
}
