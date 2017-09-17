using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOut.App.Forms.DataModel;
using WorkOut.App.Forms.Model;
using WorkOut.App.Forms.Repository.Interfaces;
using WorkOut.App.Forms.ViewModel.Interface;
using Xamarin.Forms;

namespace WorkOut.App.Forms.Repository
{
    public class SetRepository : ISetRepository
    {
        public ISetViewModel[] GetSets(int workOutId)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                return new ObservableCollection<ISetViewModel>(connection.Query<SetRow>("SELECT * FROM [Set] WHERE WorkOutId = ?", workOutId)
                    .Select(s => 
                    {
                        var set = App.Container.Resolve<ISetViewModel>();
                        set.WorkOutId = s.WorkOutId;
                        set.SetId = s.SetId;
                        set.SetType = (WorkOutAssignment.WorkOutTypes)s.SetType;
                        set.SetName = s.SetName;
                        set.CompletedRepetitions = s.CompletedRepetitions;
                        set.TotalRepetitions = s.TotalRepetitions;
                        set.Weight = s.Weight;
                        return set;
                    }))
                    .ToArray();
            }
        }

        public void AddSet(ISetViewModel set)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                var setRow = new SetRow
                {
                    WorkOutId = set.WorkOutId,
                    SetType = (int)set.SetType,
                    SetName = set.SetName,
                    CompletedRepetitions = set.CompletedRepetitions,
                    TotalRepetitions = set.TotalRepetitions,
                    Weight = set.Weight
                };

                connection.Insert(setRow);

                set.SetId = setRow.SetId;
            }
        }

        public void UpdateSet(ISetViewModel set)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                connection.Update(new SetRow
                {
                    SetId = set.SetId,
                    WorkOutId = set.WorkOutId,
                    SetType = (int)set.SetType,
                    SetName = set.SetName,
                    CompletedRepetitions = set.CompletedRepetitions,
                    TotalRepetitions = set.TotalRepetitions,
                    Weight = set.Weight
                });
            }
        }

        public void DeleteSet(ISetViewModel set)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                connection.Delete<SetRow>(set.SetId);
            }
        }
    }
}
