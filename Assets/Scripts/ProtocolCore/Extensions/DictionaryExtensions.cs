using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElectronicVoting.Extensions
{
    public static class DictionaryExtensions
    {
        public static string GetString(this Dictionary<string, object> target, string key)
        {
            return (string) target[key];
        }
        public static int GetInt(this Dictionary<string, object> target, string key)
        {
            return Convert.ToInt32(target[key]);
        }
        public static float GetFloat(this Dictionary<string, object> target, string key)
        {
            return Convert.ToSingle(target[key]);
        }
        public static long GetLong(this Dictionary<string, object> target, string key)
        {
            return Convert.ToInt64(target[key]);
        }
        public static double GetDouble(this Dictionary<string, object> target, string key)
        {
            return Convert.ToDouble(target[key]);
        }
        public static List<object> GetList(this Dictionary<string, object> target, string key)
        {
            return (List<object>) target[key];
        }
        public static string[] GetStringArray(this Dictionary<string, object> target, string key)
        {
            return GetList(target, key).Cast<string>().ToArray();
        }
        public static byte[] GetByteArray(this Dictionary<string, object> target, string key)
        {
            return GetList(target, key).Cast<byte>().ToArray();
        }
        public static byte[] GetUTF8Key(this Dictionary<string, object> target, string key)
        {
            var str = target.GetString(key);
            var result = Encoding.Unicode.GetBytes(str);
            return result;
        }
        public static Dictionary<string, object> GetDictionary(this Dictionary<string, object> target, string key)
        {
            return (Dictionary<string, object>)target[key];
        }
        public static Dictionary<string, object> GetChangeableCopy(this IReadOnlyDictionary<string, object> target)
        {
            var result = new Dictionary<string, object>();
            foreach (var pair in target)
            {
                result.Add(pair.Key, pair.Value);
            }

            return result;
        }
        public static IEnumerable<(string, Dictionary<string, object>)> GetAllNode(this Dictionary<string, object> target)
        {
            foreach (var item in target)
            {
                yield return (item.Key, item.Value.ToDictionary());
            }
        }
    }
}