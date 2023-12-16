using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondLab
{
    internal class Year
    {
        public int year { get; set; }
        public Year()
        {
         
        }
        public bool isLeapYear()
        {

            return year % 4 == 0 && (year % 100 != 0 || year % 400 == 0);
        }
    }
}