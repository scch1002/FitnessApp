using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkOut.App.Forms.DataModel
{
    [Table("WorkOutAssignment")]
    public class WorkOutAssignmentRow
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int AssignmentId { get; set; }

        public int SessionDefinitionId { get; set; }

        public int WorkOutDefinitionId { get; set; }

        public int WorkOutType { get; set; }
    }
}
