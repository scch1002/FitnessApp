using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkOut.App.Forms.DataModel
{
    [Table("SessionDefinition")]
    public class SessionDefinitionRow
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int SessionDefinitonId { get; set; }

        public string SessionName { get; set; }

        public int SessionOrder { get; set; }
    }
}
