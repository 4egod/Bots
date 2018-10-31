using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Messenger.Bot.Extension
{
    /// <summary>
    /// see :
    /// https://stackoverflow.com/questions/10418651/using-enummemberattribute-and-doing-automatic-string-conversions
    /// </summary>
    internal static class JsonExtension
    {
        /// <summary>
        /// Get <see cref="Enum"/> 's <see cref="EnumMemberAttribute"/>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ToEnumString<T>(this T type)
        {
            var enumType = typeof (T);
            var name = Enum.GetName(enumType, type);
            var enumMemberAttribute = ((EnumMemberAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true)).Single();
            return enumMemberAttribute.Value;
        }

        /// <summary>
        /// find <see cref="Enum"/> where it's <see cref="EnumMemberAttribute"/> 's value matched to target string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string str)
        {
            var enumType = typeof(T);
            foreach (var name in Enum.GetNames(enumType))
            {
                var enumMemberAttribute = ((EnumMemberAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true)).Single();
                if (enumMemberAttribute.Value == str) return (T)Enum.Parse(enumType, name);
            }
            //throw exception or whatever handling you want or
            return default(T);
        }
    }
}
