using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Units.MapFeatures
{
    public class ResourceNode : UnitInfo
    {
        public Resources CurrentResources;
        public ResourceNode()
        {
            CurrentResources = new Resources();
        }
    }
}
