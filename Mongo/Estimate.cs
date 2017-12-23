using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEstimate.Mongo
{
    public class Estimate
    {
        public double Laboriousness { get; set; }

        public decimal CostPrice { get; set; }

        public double EconomicEfficiency { get; set; }

        public double Concurrency { get; set; }
    }
}
