using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Extensions;
public static class IntExtensions
{
    public static bool IsGreaterThan(this int target, int value)
    {
        if(target > value)
        {
            return true;
        }
        return false;
    }
    public static bool IsGreaterThanOrEqualTo(this int target, int value)
    {
        if (target.IsGreaterThan(value) == true)
        {
            return true;
        }
        if (target.Equals(value) == true)
        {
            return true;
        }
        return false;
    }
    public static bool IsLessThan(this int target, int value)
    {
        if (target < value)
        {
            return true;
        }
        return false;
    }
    public static bool IsLessThanOrEqualTo(this int target, int value)
    {
        if (target.IsLessThan(value) == true)
        {
            return true;
        }
        if (target.Equals(value) == true)
        {
            return true;
        }
        return false;
    }
}

