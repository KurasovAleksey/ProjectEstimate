using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEstimate.Mongo
{

    public class Material
    {
        public string Title { get; set; }

        public decimal Cost { get; set; }

        public int Quantity { get; set; }

        public string Unit { get; set; }
    }
}
