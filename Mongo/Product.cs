using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEstimate.Mongo
{
    public class Product
    {
        public double DBTime { get; set; }

        public double Reliability { get; set; }

        public double Usability { get; set; }

        public double Functionality { get; set; }

        public double Efficiency { get; set; }

        public double OperatingSpeed { get; set; }
    }
}
