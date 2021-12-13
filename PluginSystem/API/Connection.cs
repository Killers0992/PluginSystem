using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginSystem.API
{
    public abstract class Connection
    {
        public abstract string IpAddress { get; }
        public abstract void Disconnect();
        public abstract bool IsBanned { get; }
    }
}
