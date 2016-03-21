using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tap
{
    public interface INavigable
    {
        void LoadContent(object obj = null);

        void UnloadContent();

        bool InNavigationState { get; set; }
    }
}
