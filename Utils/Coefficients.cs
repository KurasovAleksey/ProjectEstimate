using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEstimate.Mongo
{
    public class Coefficients
    {
        static Coefficients instance = new Coefficients();

        public static Coefficients Instance { get => instance; }

        public double TradeProcurementExpenses { get; set; } = 0.15;

        public double ExtraSalary { get; set; } = 0.17;

        public double SocialNeeds { get; set; } = 0.26;

        public double Overheads { get; set; } = 0.15;

        public double Rentability { get; set; } = 0.15;

        public double TransportExpenses { get; set; } = 0.15;

        public double TradeOrgsMarkUp { get; set; } = 0.03;

    }
}
