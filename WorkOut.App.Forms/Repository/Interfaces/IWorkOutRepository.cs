using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOut.App.Forms.Model;
using WorkOut.App.Forms.ViewModel.Interface;

namespace WorkOut.App.Forms.Repository.Interfaces
{
    public interface IWorkOutRepository
    {
        IWorkoutViewModel[] GetWorkOuts(ISessionViewModel session);

        void AddWorkOut(IWorkoutViewModel workout);

        void UpdateWorkOut(IWorkoutViewModel workout);

        void DeleteWorkOut(IWorkoutViewModel workOut);
    }
}
