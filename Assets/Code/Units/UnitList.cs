using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Units
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Lists and stores the unit infos avalible to a player
    ///             this is nessassary both to store information about upgrades
    ///             and the like as well as to make sure that constructing buildings
    ///             that can produce worker units works correctly, in that as long as  </summary>
    ///
    /// <remarks>   Charlie, 2/1/2013. </remarks>
    ///-------------------------------------------------------------------------------------------------
    public class UnitList
    {
        private List<UnitInfo> m_list;
        public UnitList()
        {
            m_list = new List<UnitInfo>();
        }

    }
}
