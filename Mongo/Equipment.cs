using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEstimate.Mongo
{
    public class Equipment
    {
        public string Title { get; set; }

        public double Cost { get; set; }

        public int Quantity { get; set; }

        public double PowerConsumption { get; set; }

        public override bool Equals(object obj)
        {
            var equipment = obj as Equipment;
            return equipment != null &&
                   Title == equipment.Title;
        }

        public override int GetHashCode()
        {
            return 434131217 + EqualityComparer<string>.Default.GetHashCode(Title);
        }
    }
}
