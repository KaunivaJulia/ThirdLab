using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SecondLab
{
    [PrimaryKey(nameof(from), nameof(to))]
    public class Interval
    {
        
        public DateTime from { get; set; }
        public DateTime to { get; set; }

        public Interval()
        {

        }

        public int FindInterval()
        {
            return (to - from).Days;
        }
    }
}
