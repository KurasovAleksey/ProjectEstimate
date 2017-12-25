using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEstimate.Mongo
{
    public class Position
    {
        public string Title { get; set; }

        public double Salary { get; set; }

        public int Quantity { get; set; }

        public override bool Equals(object obj)
        {
            var position = obj as Position;
            return position != null &&
                   Title == position.Title;
        }

        public override int GetHashCode()
        {
            return 434131217 + EqualityComparer<string>.Default.GetHashCode(Title);
        }
    }
}
