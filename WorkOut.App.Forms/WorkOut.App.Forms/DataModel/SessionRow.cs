using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkOut.App.Forms.DataModel
{
    [Table("Session")]
    public class SessionRow
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int SessionId { get; set; }

        public int SessionDefinitionId { get; set; }

        public string SessionName { get; set; }

        public DateTime SessionDate { get; set; }
    }
}
