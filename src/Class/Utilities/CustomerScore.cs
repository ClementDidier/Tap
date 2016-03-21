using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tap.Class.Utilities
{
    public class CustomerScore
    {
        public CustomerScore(string name, int points)
        {
            this.Name = name;
            this.Points = points;
        }

#if DEBUG
        public override string ToString() => $"[Name : {Name}, Points : {Points}]";
#endif

        [JsonProperty("name")]
        public string Name
        {
            get;
            set;
        }

        [JsonProperty("points")]
        public int Points
        {
            get;
            set;
        }
    }
}
