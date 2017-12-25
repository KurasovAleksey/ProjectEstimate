using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEstimate.Mongo
{
    public class Mark
    {
        public string Title { get; set; }

        public double Value { get; set; }

        public override bool Equals(object obj)
        {
            var mark = obj as Mark;
            return mark != null &&
                   Title == mark.Title;
        }

        public override int GetHashCode()
        {
            return 434131217 + EqualityComparer<string>.Default.GetHashCode(Title);
        }
    }
}
