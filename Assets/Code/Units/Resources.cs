using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;

[Serializable]
public class Resources
{
    public enum ResourceTypes
    {
        Food,
        Gold,
        Stone,
        Copper,
        Tin,
        Bronze
    };
    
    public int[] ResourceArray = new int[Enum.GetValues(typeof (ResourceTypes)).Length];

    public int Food
    {
        get { return ResourceArray[(int) ResourceTypes.Food]; }
        set { ResourceArray[(int) ResourceTypes.Food] = value; }
    }

    public int Gold
    {
        get { return ResourceArray[(int) ResourceTypes.Gold]; }
        set { ResourceArray[(int) ResourceTypes.Gold] = value; }
    }

    public int Stone
    {
        get { return ResourceArray[(int) ResourceTypes.Stone]; }
        set { ResourceArray[(int) ResourceTypes.Stone] = value; }
    }

    public int Copper
    {
        get { return ResourceArray[(int) ResourceTypes.Copper]; }
        set { ResourceArray[(int) ResourceTypes.Copper] = value; }
    }

    public int Tin
    {
        get { return ResourceArray[(int) ResourceTypes.Tin]; }
        set { ResourceArray[(int) ResourceTypes.Tin] = value; }
    }

    public int Bronze
    {
        get { return ResourceArray[(int) ResourceTypes.Bronze]; }
        set { ResourceArray[(int) ResourceTypes.Bronze] = value; }
    }

    public int Sum()
    {
        int retval = 0;
        retval = Food + Gold + Stone + Copper + Tin + Bronze;
        return retval;
    }

    public static Resources operator -(Resources lhs, Resources rhs)
    {
        Resources retval = new Resources();
        retval.Food = lhs.Food - rhs.Food;
        retval.Gold = lhs.Gold - rhs.Gold;
        retval.Stone = lhs.Stone - rhs.Stone;
        retval.Copper = lhs.Copper - rhs.Copper;
        retval.Tin = lhs.Tin - rhs.Tin;
        retval.Bronze = lhs.Bronze - rhs.Bronze;
        return retval;
    }

    /// <summary>
    /// Gets a resource struct with <paramref name="n"/> total resources
    /// by taking 1 from each resource in this Resource struct until the new
    /// struct has n total resources
    /// 
    /// The order in which resources will be taken is undefined
    /// </summary>
    /// <param name="n">the total number of resources to get</param>
    /// <returns>new resource struct with n resources</returns>
    public Resources GetResources(int n)
    {
        Resources retval = new Resources();
        for (int i = 0; i < ResourceArray.Length; i++)
        {
            if (ResourceArray[i] >= 1 && retval.Sum() < n)
            {
                retval.ResourceArray[i] = 1;
            }
        }
        return retval;
    }

    public static Resources operator +(Resources lhs, Resources rhs)
    {
        Resources retval = new Resources();
        retval.Food = lhs.Food + rhs.Food;
        retval.Gold = lhs.Gold + rhs.Gold;
        retval.Stone = lhs.Stone + rhs.Stone;
        retval.Copper = lhs.Stone + rhs.Stone;
        retval.Tin = lhs.Stone + rhs.Stone;
        retval.Bronze = lhs.Bronze + rhs.Bronze;
        return retval;
    }

    public static bool operator >(Resources lhs, Resources rhs)
    {
        return lhs.Sum() > rhs.Sum();
    }

    public static bool operator <(Resources lhs, Resources rhs)
    {
        return lhs.Sum() < rhs.Sum();
    }

    public static bool operator >=(Resources lhs, Resources rhs)
    {
        return lhs.Sum() >= rhs.Sum();
    }

    public static bool operator <=(Resources lhs, Resources rhs)
    {
        return lhs.Sum() <= rhs.Sum();
    }

    public static bool operator >=(Resources lhs, float rhs)
    {
        return lhs.Sum() >= rhs;
    }

    public static bool operator <=(Resources lhs, float rhs)
    {
        return lhs.Sum() <= rhs;
    }

    public static bool operator <(Resources lhs, float rhs)
    {
        return lhs.Sum() < rhs;
    }

    public static bool operator >(Resources lhs, float rhs)
    {
        return lhs.Sum() > rhs;
    }

}