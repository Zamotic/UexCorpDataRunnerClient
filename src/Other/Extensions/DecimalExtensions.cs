using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Extensions;
public static class DecimalExtensions
{
    public static bool IsGreaterThan(this decimal target, decimal value)
    {
        if(target > value)
        {
            return true;
        }
        return false;
    }
    public static bool IsGreaterThanOrEqualTo(this decimal target, decimal value)
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
    public static bool IsLessThan(this decimal target, decimal value)
    {
        if (target < value)
        {
            return true;
        }
        return false;
    }
    public static bool IsLessThanOrEqualTo(this decimal target, decimal value)
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

