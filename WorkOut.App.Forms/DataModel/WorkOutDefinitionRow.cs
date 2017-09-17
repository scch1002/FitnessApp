using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkOut.App.Forms.DataModel
{
    [Table("WorkOutDefinition")]
    public class WorkOutDefinitionRow
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int WorkOutDefinitionId { get; set; }

        public string WorkOutName { get; set; }

        public int NumberOfWarmUpSets { get; set; }

        public int WarmUpRepetitions { get; set; }

        public int WarmUpWeight { get; set; }

        public int WarmUpWeightIncrement { get; set; }

        public int NumberOfSets { get; set; }

        public int Repetitions { get; set; }

        public int Weight { get; set; }

        public int WeightIncrement { get; set; }

        public bool AutoIncrementStartingWeight { get; set; }

        public int RestTimeBetweenSetsMinutes { get; set; }

        public int RestTimeBetweenSetsSeconds { get; set; }

        public int SessionDefinitionId { get; set; }
    }
}
