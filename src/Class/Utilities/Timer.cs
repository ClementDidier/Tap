using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tap
{
    class Timer
    {
        private int ticks = 0;

        public bool WaitTicks(int ticks)
        {
            if(this.ticks++ >= ticks)
            {
                this.ticks = 0;
                return true;
            }
            return false;
        }
    }
}
