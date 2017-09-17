using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOut.App.Forms.Model;
using WorkOut.App.Forms.ViewModel.Interface;
using WorkOut.App.Forms.Repository.Interfaces;

namespace WorkOut.App.Forms.Repository.Interfaces
{
    public interface ISetRepository
    {
        ISetViewModel[] GetSets(int workOutId);

        void AddSet(ISetViewModel set);

        void UpdateSet(ISetViewModel set);

        void DeleteSet(ISetViewModel set);
    }
}
