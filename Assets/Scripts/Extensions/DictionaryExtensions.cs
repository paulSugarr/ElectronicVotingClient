﻿using System;
 using System.Collections;
 using System.Collections.Generic;
using System.Linq;
 using Core;

 namespace Extensions
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
        public static object[] GetArray(this Dictionary<string, object> target, string key)
        {
            return (object[]) target[key];
        }
        public static List<T> GetList<T>(this Dictionary<string, object> target, string key)
        {
            return (List<T>) target[key];
        }
        public static ArrayList GetArrayList(this Dictionary<string, object> target, string key)
        {
            return (ArrayList) target[key];
        }
        public static string[] GetStringArray(this Dictionary<string, object> target, string key)
        {
            return GetList(target, key).Cast<string>().ToArray();
        }
        public static byte[] GetByteArray(this Dictionary<string, object> target, string key)
        {
            return GetList(target, key).Cast<byte>().ToArray();
        }
        public static int[] GetIntArray(this Dictionary<string, object> target, string key)
        {
            var list = target.GetList<object>(key);
            var result = new List<int>();
            foreach (var item in list)
            {
                result.Add(Convert.ToInt32(item));
            }
            return result.ToArray();
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