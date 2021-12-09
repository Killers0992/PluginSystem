using MonoMod;

namespace PluginSystem.Patcher.Patches
{
    [MonoModPatch("global::PlayerInteract")]
    public class PlayerInteractPatch : PlayerInteract
    {
        public extern void orig_UserCode_CmdDetonateWarhead();

        private void UserCode_CmdDetonateWarhead()
        {
            PluginSystem.LastStartedDetonationBy = _hub;
            orig_UserCode_CmdDetonateWarhead();
        }
    }
}
