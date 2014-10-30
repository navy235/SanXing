using System;
using System.Collections.Generic;
using System.Linq;
namespace Mt.Core
{
    public static class Extensions
    {
        public static bool IsNullOrDefault<T>(this T? value) where T : struct
        {
            return default(T).Equals(value.GetValueOrDefault());
        }

        public static List<int> ToList(this string Value)
        {
            if (string.IsNullOrEmpty(Value))
            {
                return new List<int>();
            }
            var list = Value.Split(',').Select(x => Convert.ToInt32(x)).ToList();
            return list;
        }
    }
}
