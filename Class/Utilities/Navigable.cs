using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tap
{
    public abstract class Navigable
    {
        public abstract void LoadContent(object obj = null);
        public abstract void UnloadContent();

        public bool InNavigationState
        {
            get;
            set;
        }
    }
}
