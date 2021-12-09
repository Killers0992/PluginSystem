using PluginSystem.API;
using PluginSystem.Attributes;
using PluginSystem.Events;
using PluginSystem.Extensions;
using PluginSystem.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace PluginSystem
{
    public class PluginManager
    {
        public static readonly Version ApiVersion = new Version(1, 0, 0);

		public BaseLogger Logger = new BaseLogger();

		private LogHandler loghandler;
		public LogHandler LogHandler
		{
			get { return loghandler; }
			set
			{
				if (loghandler == null)
					loghandler = value;
			}
		}

		private Server server;
		public Server Server
		{
			get { return server; }
			set
			{
				if (server == null)
				{
					server = value;
				}
			}
		}

		private static PluginManager singleton;
		public static PluginManager Manager
		{
			get
			{
				if (singleton == null)
                {
					singleton = new PluginManager();
					PluginManager.PluginsAssemblies.Add(Assembly.GetAssembly(typeof(PluginManager)), "PluginSystem");
				}

				return singleton;
			}
		}

		public IPlugin<IConfig> GetEnabledPlugin(string id)
        {
			return Plugins
				.Where(p => !DisabledPlugins.Contains(p.Key) && p.Key == id)
				.Select(p => p.Value)
				.FirstOrDefault();
        }

		public void EnablePlugins()
        {
			var pluginsToEnable = Plugins
				.Where(p => DisabledPlugins.Contains(p.Value.Details.Id))
				.Select(p => p.Key)
				.ToArray();

			int enabled = 0;

			Logger.Info($"Enabling {pluginsToEnable.Length} plugins...");

			for (int x = 0; x < pluginsToEnable.Length; x++)
			{
				if (!Plugins.TryGetValue(pluginsToEnable[x], out IPlugin<IConfig> plugin))
                {
					Logger.Info($"[{x + 1}/{pluginsToEnable.Length}] Skipped enabling plugin with id \"{pluginsToEnable[x]}\". ( plugin is not registered )");
					continue;
                }

				EventManager.Manager.AddSnapshotEventHandlers(plugin);

				Logger.Info($"[{x + 1}/{pluginsToEnable.Length}] Enabling plugin \"{plugin.Details.Name}\" ({plugin.Details.Version})...");
				try
				{
					plugin.OnEnable();
				}
				catch (Exception ex)
				{
					Logger.Warn($"[{x + 1}/{pluginsToEnable.Length}] Error while invoking OnRegister in plugin \"{plugin.Details.Name}\" ({plugin.Details.Version}).\n{ex}");
					continue;
				}

				Logger.Info($"[{x + 1}/{pluginsToEnable.Length}] Enabled plugin \"{plugin.Details.Name}\" ({plugin.Details.Version}).");
				enabled++;
			}
			Logger.Info($"Enabled {enabled}/{pluginsToEnable.Length} plugins.");
		}

		public void LoadPlugins(string directory)
        {
			LoadDependencies(Path.Combine(directory, "dependencies"));
			LoadPluginAssemblies(directory);
        }

		public void LoadDependencies(string directory)
        {
			var totalDependencies = new DirectoryInfo(directory).GetFiles("*.dll");
			int loaded = 0;

			Logger.Info($"Loading {totalDependencies.Length} dependencies...");

			for (int x = 0; x < totalDependencies.Length; x++)
            {
				var fileName = Path.GetFileNameWithoutExtension(totalDependencies[x].FullName);
				if (LoadedAssemblies.ContainsKey(fileName))
                {
					Logger.Info($"[{x + 1}/{totalDependencies.Length}] Skipped loading dependency \"{fileName}\". ( already loaded )");
                }
                else
                {
					Logger.Info($"[{x + 1}/{totalDependencies.Length}] Loading dependency \"{fileName}\"...");
					try
                    {
						var ass = Assembly.LoadFrom(totalDependencies[x].FullName);
						LoadedAssemblies.Add(fileName, ass);
					}
					catch (Exception ex)
                    {
						Logger.Error($"[{x + 1}/{totalDependencies.Length}] Error while loading dependency \"{fileName}\".\n {ex}");
						continue;
					}
					Logger.Info($"[{x + 1}/{totalDependencies.Length}] Loaded dependency \"{fileName}\".");
					loaded++;
				}
			}
			Logger.Info($"Loaded {loaded}/{totalDependencies.Length} dependencies.");
		}

		public void LoadPluginAssemblies(string directory)
        {
			var totalPlugins = new DirectoryInfo(directory).GetFiles("*.dll");
			int loaded = 0;

			Logger.Info($"Loading {totalPlugins.Length} plugins...");

			for (int x = 0; x < totalPlugins.Length; x++)
			{
				var fileName = Path.GetFileNameWithoutExtension(totalPlugins[x].FullName);
				if (LoadedAssemblies.ContainsKey(fileName))
				{
					Logger.Info($"[{x + 1}/{totalPlugins.Length}] Skipped loading plugin \"{fileName}\". ( already loaded )");
				}
				else
				{
					IPlugin<IConfig> plugin = null;
					Logger.Info($"[{x + 1}/{totalPlugins.Length}] Loading plugin \"{fileName}\"...");
					try
					{
						var ass = Assembly.LoadFrom(totalPlugins[x].FullName);
						var pluginClass = ass.GetTypes().FirstOrDefault(p => 
							!p.IsInterface &&
							!p.IsAbstract &&
							p.BaseType.IsGenericType &&
							p.BaseType.GetGenericTypeDefinition() == typeof(Plugin<>));

						if (pluginClass == null)
                        {
							Logger.Info($"[{x + 1}/{totalPlugins.Length}] Failed loading plugin \"{fileName}\". ( plugin dont have valid class )");
							continue;
						}

						var construct = pluginClass.GetConstructor(Type.EmptyTypes);
						if (construct != null)
                        {
							plugin = construct.Invoke(null) as IPlugin<IConfig>;
                        }
                        else
                        {
							var value = Array.Find(
								pluginClass.GetProperties(
									BindingFlags.Static |
									BindingFlags.NonPublic |
									BindingFlags.Public), 
								property => property.PropertyType == pluginClass)?.GetValue(null);

							if (value != null)
								plugin = value as IPlugin<IConfig>;
						}

						if (plugin == null)
                        {
							Logger.Info($"[{x + 1}/{totalPlugins.Length}] Failed loading plugin \"{fileName}\". ( invalid constructor )");
							continue;
                        }

						PluginDetails details = (PluginDetails)Attribute.GetCustomAttribute(pluginClass, typeof(PluginDetails));

						if (details == null)
						{
							Logger.Info($"[{x + 1}/{totalPlugins.Length}] Failed loading plugin \"{fileName}\". ( missing plugin details )");
							continue;
                        }

						if (details.ApiVersion.ToVersion() != ApiVersion)
                        {
							Logger.Warn($"[{x + 1}/{totalPlugins.Length}] Tried to load an outdated plugin \"{details.Name}\" ({details.Version}).");
                        }
                        else
                        {
							plugin.Details = details;
							Plugins.Add(details.Id, plugin);
							PluginsAssemblies.Add(ass, details.Id);
							DisabledPlugins.Add(details.Id);
							try
							{
								plugin.Register();
							}
							catch (Exception ex)
                            {
								Logger.Warn($"[{x + 1}/{totalPlugins.Length}] Error while invoking Register in plugin \"{details.Name}\" ({details.Version}).\n{ex}");
							}
						}

						LoadedAssemblies.Add(fileName, ass);
					}
					catch (Exception ex)
					{
						Logger.Error($"[{x + 1}/{totalPlugins.Length}] Error while loading plugin \"{fileName}\".\n {ex}");
						continue;
					}
					Logger.Info($"[{x + 1}/{totalPlugins.Length}] Loaded plugin \"{plugin.Details.Name}\" ({plugin.Details.Version}) made by {plugin.Details.Author}.");
					loaded++;
				}
			}
			Logger.Info($"Loaded {loaded}/{totalPlugins.Length} plugins.");
		}

		public void LoadConfig(IPlugin<IConfig> cfg)
        {

        }
											    
		public readonly static Dictionary<string, Assembly> LoadedAssemblies = new Dictionary<string, Assembly>();

		public readonly static Dictionary<Assembly, string> PluginsAssemblies = new Dictionary<Assembly, string>();

		public readonly static Dictionary<string, IPlugin<IConfig>> Plugins = new Dictionary<string, IPlugin<IConfig>>();

		public readonly static List<string> DisabledPlugins = new List<string>();
	}
}
