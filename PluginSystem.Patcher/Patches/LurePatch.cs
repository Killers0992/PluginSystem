using MonoMod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginSystem.Patcher.Patches
{
    [MonoModPatch("global::LureSubjectContainer")]
    public class LurePatch : LureSubjectContainer
    {
        public extern void orig_SetState(bool p, bool b);

        public void SetState(bool p, bool b)
        {
            if (PluginSystem.LureCanceled)
                return;

            orig_SetState(p, b);
        }
    }
}
