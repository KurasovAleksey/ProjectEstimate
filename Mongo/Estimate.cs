using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEstimate.Mongo
{
    public class Estimate
    {
        public decimal Cost { get; set; }

        public decimal SellingPrice { get; set; }

        public decimal AgreedCost { get; set; }

        public decimal OperatingCosts { get; set; }

        public double ProductionEffect { get; set; }

        public double UsageEffect { get; set; }

        public decimal MachineHourCost { get; set; }

        public decimal MHCSaving { get; set; }

        public decimal SalarySaving { get; set; }

        public decimal MaterialSaving { get; set; }

        public double TotalEconomicEffect { get; set; }

        public double EconomicEfficiency { get; set; }

        public double IntegralConcurrency { get; set; }
    }
}
