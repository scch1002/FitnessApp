using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkOut.App.Forms.Messages
{
    public class DeleteAssignmentMessage
    {
        public int WorkoutDefinitionId { get; set; }

        public int SessionDefinitionId { get; set; }
    }
}
