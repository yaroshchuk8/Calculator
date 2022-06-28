using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Operator
    {
        string val;
        public Operator(string x)
        {
            this.val = x;
        }
        public string value()
        {
            return val;
        }
        public string delete()
        {
            return val.Remove((val).Length - 1); ;
        }
    }
}
