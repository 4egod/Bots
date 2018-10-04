using System;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Messenger
{
    public static class Converter
    {
        public static string ToJson(this object value)
        {
            return JsonConvert.SerializeObject(value, Formatting.Indented);
        }

        public static T FromJson<T>(this string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public static byte[] ToBinary(this object value)
        {
            return Encoding.UTF8.GetBytes(value.ToJson());
        }

        public static T FromBinary<T>(byte[] value)
        {
            string s = Encoding.UTF8.GetString(value);
            return FromJson<T>(s);
        }

        public static string ToHex(this byte[] value)
        {
            return string.Concat(value.Select(b => $"{b:X2}"));
        }

        public static byte[] FromHex(this string value)
        {
            byte[] result = new byte[value.Length / 2];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = (byte)Convert.ToInt32(value.Substring(i * 2, 2), 16);
            }

            return result;
        }

        public static string Format(this string value, params object[] args)
        {
            return string.Format(value, args);
        }
    }
}
