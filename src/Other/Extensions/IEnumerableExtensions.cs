using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Extensions;
public static class IEnumerableExtensions
{
    /// <summary>
    ///   Checks whether all items in the enumerable are same (Uses <see cref="object.Equals(object)" /> to check for equality)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="enumerable">The enumerable.</param>
    /// <returns>
    ///   Returns true if there is 0 or 1 item in the enumerable or if all items in the enumerable are same (equal to
    ///   each other) otherwise false.
    /// </returns>
    public static bool AreAllSame<T>(this IEnumerable<T> enumerable)
    {
        if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));

        using (var enumerator = enumerable.GetEnumerator())
        {
            var toCompare = default(T);
            if (enumerator.MoveNext())
            {
                toCompare = enumerator.Current;
            }

            while (enumerator.MoveNext())
            {
                if (toCompare != null && !toCompare.Equals(enumerator.Current))
                {
                    return false;
                }
            }
        }

        return true;
    }

    public static bool IsAMatch<T>(this IEnumerable<T> enumerable1, IEnumerable<T> enumerable2)
    {
        if (enumerable1 is null) throw new ArgumentNullException(nameof(enumerable1));
        if (enumerable2 is null) return false;

        using (var enumerator1 = enumerable1.GetEnumerator())
        using (var enumerator2 = enumerable2.GetEnumerator())
        {
            while (enumerator1.MoveNext())
            {
                if (!enumerator2.MoveNext())
                {
                    return false;
                }

                if (enumerator1.Current.Equals(enumerator2.Current) == false)
                {
                    return false;
                }
            }

            if (enumerator2.MoveNext())
            {
                return false;
            }

            return true;
        }
    }
}
