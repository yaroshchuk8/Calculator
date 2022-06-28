using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Number
    {
        string val;
        public Number(string x)
        {
            this.val = x;
        }
        public string value()
        {
            return val;
        }
    }
}
