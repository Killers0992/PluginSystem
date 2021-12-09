using MonoMod;

namespace PluginSystem.Patcher.Patches.Initializer
{
    [MonoModPatch("global::ServerStatic")]
    public class ServerStaticPatch : ServerStatic
    {
        public extern void orig_Awake();

        public void Awake()
        {
            orig_Awake();
            PluginSystem.Initialize();
        }
    }
}
