using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOut.App.Forms.DataModel;
using WorkOut.App.Forms.Model;
using Xamarin.Forms;
using ModelWorkOut = WorkOut.App.Forms.Model.WorkOut;

namespace WorkOut.App.Forms.Repository
{
    public static class SetRepository
    {
        public static Set[] GetSets(int workOutId)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                return new ObservableCollection<Set>(connection.Query<SetRow>("SELECT * FROM [Set] WHERE WorkOutId = ?", workOutId)
                    .Select(s => new Set
                    {
                        SetId  = s.SetId,
                        SetType = s.SetType,
                        SetName = s.SetName,
                        CompletedRepetitions = s.CompletedRepetitions,
                        TotalRepetitions = s.TotalRepetitions,
                        Weight = s.Weight
                    }))
                    .ToArray();
            }
        }

        public static void AddSet(Set set, ModelWorkOut workOut)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                var setRow = new SetRow
                {
                    WorkOutId = workOut.WorkOutId,
                    SetType = set.SetType,
                    SetName = set.SetName,
                    CompletedRepetitions = set.CompletedRepetitions,
                    TotalRepetitions = set.TotalRepetitions,
                    Weight = set.Weight
                };

                connection.Insert(setRow);

                set.SetId = setRow.SetId;
            }
        }

        public static void UpdateSet(Set set, ModelWorkOut workOut)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                connection.Update(new SetRow
                {
                    SetId = set.SetId,
                    WorkOutId = workOut.WorkOutId,
                    SetType = set.SetType,
                    SetName = set.SetName,
                    CompletedRepetitions = set.CompletedRepetitions,
                    TotalRepetitions = set.TotalRepetitions,
                    Weight = set.Weight
                });
            }
        }

        public static void DeleteSet(Set set)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                connection.Delete<SetRow>(set.SetId);
            }
        }
    }
}
