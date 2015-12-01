using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wijkoeler
{
    class Engine
    {
        public bool status { get; set; }
        public void turnOn()
        {
            status = true;
        }
        public void turnOff()
        {
            status = false;

        }

    }
}
