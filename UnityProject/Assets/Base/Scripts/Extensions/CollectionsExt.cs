using System;
using System.Collections.Generic;


public static class CollectionExt
{
    #region Dictionary
    /// <summary>
    /// 获取字典的成员,如果没有则添加
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TValue GetOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
    {
        if (!dict.TryGetValue(key, out TValue val))
            dict.Add(key, value);

        return value;
    }

    /// <summary>
    /// 弹出字典的成员
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static TValue Popup<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
    {
        if (dict.TryGetValue(key, out TValue val))
            dict.Remove(key);

        return val;
    }

    /// <summary>
    /// 如果字典中存在键则返回false,不存在则添加然后返回true
    /// </summary>
    /// <param name="key"></param>
    /// <param name="val"></param>
    /// <returns></returns>
    public static bool AddIfNotContains<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue val)
    {
        if (dict.ContainsKey(key))
        {
            return false;
        }
        else
        {
            dict.Add(key, val);
            return true;
        }
    }

    #endregion

    #region List


    /// <summary>
    /// 随机获得List中的一个成员,需在外部确保list.count > 0
    /// </summary>
    /// <param name="list"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T Random<T>(this List<T> list)
    {
        return list[UnityEngine.Random.Range(0, list.Count)];
    }

    public static Dictionary<TKey,TVal> ToDict<TKey,TVal>(this List<TVal> list,Func<TVal,TKey> func)
    {
        Dictionary<TKey,TVal> dict = new Dictionary<TKey, TVal>();
        foreach (var item in list)
        {
            dict.Add(func(item),item);
        }

        return dict;
    }

    #endregion

    #region Set
    /// <summary>
    /// 如果Set中存在对象则返回false,不存在则添加然后返回true
    /// </summary>
    /// <param name="set"></param>
    /// <param name="val"></param>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    public static bool AddIfNotContains<TValue>(this HashSet<TValue> set, TValue val)
    {
        if (set.Contains(val))
        {
            return false;
        }
        else
        {
            set.Add(val);
            return true;
        }
    }

    #endregion
}
