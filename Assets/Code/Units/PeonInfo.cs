﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Units
{
    public class PeonInfo : UnitInfo<UnitController>
    {
        public Resources StoredResources = new Resources();
        public float HarvestRate; //rate of harvesting in one resource per harvestRate seconds
        public float harvestAmount;
    }
}
