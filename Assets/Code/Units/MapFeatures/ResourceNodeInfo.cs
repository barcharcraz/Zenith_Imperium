using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Units.MapFeatures
{
    [Serializable]
    public class ResourceNodeInfo : UnitInfo<BasicController>
    {
        public Resources CurrentResources;
        public ResourceNodeInfo()
        {
            CurrentResources = new Resources();
        }
    }
}
