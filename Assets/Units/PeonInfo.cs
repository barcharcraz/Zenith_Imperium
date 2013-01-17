using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Units
{
    class PeonInfo : UnitInfo
    {
        public Resources StoredResources;
        public float HarvestRate; //rate of harvesting in one resource per harvestRate seconds
        public float harvestAmount;
    }
}
