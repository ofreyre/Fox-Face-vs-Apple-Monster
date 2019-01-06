using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilsEnum {

    public static int EnumCount<T>() {
        return System.Enum.GetValues(typeof(T)).Length;
    }

    public static List<T> Enum2List<T>()
    {
        List<T> list = new List<T>();
        System.Array enums = System.Enum.GetValues(typeof(T));
        foreach (T en in enums)
        {
            list.Add(en);
        }
        return list;
    }

    public static T[] Enum2Array<T>()
    {
        System.Array enums = System.Enum.GetValues(typeof(T));
        int n = enums.Length;
        T[] list = new T[n];
        for (int i = 0; i < n; i++)
        {
            list[i] = (T)enums.GetValue(i);
        }
        return list;
    }

    public static string[] Enum2ArayString<T>()
    {
        System.Array enums = System.Enum.GetValues(typeof(T));
        int n = enums.Length;
        string[] list = new string[n];
        for (int i = 0; i < n; i++)
        {
            list[i] = enums.GetValue(i).ToString();
        }
        return list;
    }

    public static Dictionary<K, int> Enum2Dictionary<K>(int defaultValue = 0)
    {
        System.Array enums = System.Enum.GetValues(typeof(K));
        int n = enums.Length;
        Dictionary<K, int> dic = new Dictionary<K, int>();
        for (int i = 0; i < n; i++)
        {
            dic.Add((K)enums.GetValue(i), defaultValue);
        }
        return dic;
    }
}
