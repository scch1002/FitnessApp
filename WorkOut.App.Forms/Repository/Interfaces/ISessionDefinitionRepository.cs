using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOut.App.Forms.ViewModel.Interface;

namespace WorkOut.App.Forms.Repository.Interfaces
{
    public interface ISessionDefinitionRepository
    {
        ISessionDefinitionViewModel[] GetSessionDefinitions();

        void AddSessionDefinition(ISessionDefinitionViewModel sessionDefinition);

        void UpdateSessionDefinition(ISessionDefinitionViewModel sessionDefinition);

        void DeleteSessionDefinition(ISessionDefinitionViewModel sessionDefinition);
    }
}
