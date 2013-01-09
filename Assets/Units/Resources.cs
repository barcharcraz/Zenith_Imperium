using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
public struct Resources
{
    public int Food { get; set; }
    public int Gold { get; set; }
    public int Stone { get; set; }
    public int Copper { get; set; }
    public int Tin { get; set; }
    public int Bronze { get; set; }

    public static Resources operator-(Resources lhs, Resources rhs)
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
    public static Resources operator+(Resources lhs, Resources rhs)
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
}
