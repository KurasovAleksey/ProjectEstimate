using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEstimate.ViewModels
{
    public class ProjectInfoDto
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string ApplicationDomain { get; set; }

        public string Info { get; set; }

        public DateTime StartingDate { get; set; }

        public DateTime Deadline { get; set; }
    }
}
