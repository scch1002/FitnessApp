using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkOut.App.Forms.Model
{
    public class WorkOutAssignment
    {
        public int WorkOutDefinitionId { get; set; }

        public int SessionDefinitionId { get; set; }

        public int WorkOutType { get; set; }
    }
}
