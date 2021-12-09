using PluginSystem.API.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PluginSystem
{
    public class RoleManager
    {
        private static RoleManager singleton;

        public static RoleManager Manager
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new RoleManager();
                }
                return singleton;
            }
        }

        public readonly Dictionary<int, IRole> RegisteredRoles = new Dictionary<int, IRole>();

        public readonly List<IRole> DefaultRoles = new List<IRole>()
        {
            new RoleNone(),
            new RoleScp173(),
            new RoleClassd(),
            new RoleSpectator(),
            new RoleScp106(),
            new RoleNtfSpecialist(),
            new RoleScp049(),
            new RoleScientist(),
            new RoleScp079(),
            new RoleChaosConscript(),
            new RoleScp096(),
            new RoleScp0492(),
            new RoleNtfSergeant(),
            new RoleNtfCaptain(),
            new RoleNtfPrivate(),
            new RoleTutorial(),
            new RoleFacilityGuard(),
            new RoleScp93953(),
            new RoleScp93989(),
            new RoleChaosRifleman(),
            new RoleChaosRepressor(),
            new RoleChaosMarauder()
        };

        public void LoadDefaultRoles()
        {
            foreach(var defaultRole in DefaultRoles)
            {
                RegisterRole(defaultRole);
            }
        }

        public void RegisterRole(IRole role)
        {
            var callingAssembly = Assembly.GetCallingAssembly();

            if (RegisteredRoles.TryGetValue(role.RoleId, out IRole orgRole))
            {
                PluginManager.Manager.Logger.RawMessage(callingAssembly, LogType.Info, $"Replaced role {orgRole.Name} ({orgRole.RoleId}) with {role.Name} ({role.RoleId}).", ConsoleColor.Cyan);
                RegisteredRoles[role.RoleId] = role;
            }
            else
            {
                RegisteredRoles.Add(role.RoleId, role);
                PluginManager.Manager.Logger.RawMessage(callingAssembly, LogType.Info, $"Registered new role {role.Name} ({role.RoleId}).", ConsoleColor.Cyan);
            }
        }
    }
}
