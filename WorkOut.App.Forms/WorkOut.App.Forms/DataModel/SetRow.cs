using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkOut.App.Forms.DataModel
{
    [Table("Set")]
    public class SetRow
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int SetId { get; set; }

        public string SetName { get; set; }

        public int TotalRepetitions { get; set; }

        public int CompletedRepetitions { get; set; }

        public int Weight { get; set; }

        public int SetType { get; set; }

        public int WorkOutId { get; set; }
    }
}
