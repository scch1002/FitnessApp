using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkOut.App.Forms.DataModel
{
    [Table("WorkOut")]
    public class WorkOutRow
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int WorkOuId { get; set; }

        public int WorkOutType { get; set; }

        public string WorkOutName { get; set; }

        public bool WorkOutComplete { get; set; }

        public int WorkOutDefinitionId { get; set; }

        public int SessionId { get; set; }
    }
}
