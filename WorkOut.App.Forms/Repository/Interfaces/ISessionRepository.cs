using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOut.App.Forms.ViewModel.Interface;

namespace WorkOut.App.Forms.Repository.Interfaces
{
    public interface ISessionRepository
    {
        ISessionViewModel[] GetSessions();

        ISessionViewModel GetSession(int sessionId);

        void AddSession(ISessionViewModel session);

        void UpdateSession(ISessionViewModel session);

        void DeleteSession(ISessionViewModel session);
    }
}
