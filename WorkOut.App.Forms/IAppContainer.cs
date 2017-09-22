using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkOut.App.Forms
{
    public interface IAppContainer
    {
        T Resolve<T>();
    }
}
