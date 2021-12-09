using System;

namespace PluginSystem.Attributes
{
    public class PluginDetails : Attribute
    {
        private string _id;
        public string Id
        {
            get
            {
                if (_id != null)
                    return _id;

                _id = $"{Author}.{Name}".ToLower().Replace(" ", "");
                return _id;
            }
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Version { get; set; }
        public string ApiVersion { get; set; }
    }
}
