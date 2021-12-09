using PluginSystem.API;
using PluginSystem.API.Roles;

namespace ExamplePlugin
{
    public class CustomClassD : RoleClassd
    {
        public CustomClassD()
        {
            this.Name = "CustomClassD";
            this.StartInventory.Add(ItemType.Adrenaline);
            this.MaxHealth = 3000;
        }
    }
}
