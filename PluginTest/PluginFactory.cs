using System.Collections.Generic;

namespace Photon.Hive.Plugin.PluginTest
{
    class PluginFactory : IPluginFactory
    {
        public IGamePlugin Create(IPluginHost gameHost, string pluginName, Dictionary<string, string> config, out string errorMsg)
        {
            var plugin = new TestPlugin();
            if (plugin.SetupInstance(gameHost, config, out errorMsg))
            {
                return plugin;
            }
            return null;
        }
    }
}
