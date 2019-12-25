using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SerializeUtil
{
    public static void SaveToPlayerPref(string key,object obj)
    {
        byte[] bytes = Serialize(obj);
        SetOpposite(bytes);
        string data = Convert.ToBase64String(bytes);
        PlayerPrefs.SetString(key,data);
    }

    public static object LoadFromPlayerPref(string key)
    {
        string data = PlayerPrefs.GetString(key);
        if (data == null || data == string.Empty)
        {
            return null;
        }
        else
        {
            byte[] bytes = Convert.FromBase64String(data);
            SetOpposite(bytes);
            return Deserialize(bytes);
        }
    }

    public static void SaveToFile(object obj, string path)
    {
        using (FileStream fs = new FileStream(path,FileMode.Create))
        {
            byte[] bytes = Serialize(obj);
            SetOpposite(bytes);
            fs.Write(bytes,0,bytes.Length);
            fs.Flush();
            fs.Close();
        }
    }

    public static object LoadFromFile(string path)
    {
        using (FileStream fs = new FileStream(path,FileMode.Open))
        {
            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes,0,bytes.Length);
            SetOpposite(bytes);
            return Deserialize(bytes);
        }
    }

    private static byte[] Serialize(object obj)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using (MemoryStream memoryStream = new MemoryStream())
        {
            formatter.Serialize(memoryStream,obj);
            return memoryStream.ToArray();
        }
    }

    private static object Deserialize(byte[] bytes)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using (MemoryStream memoryStream = new MemoryStream(bytes))
        {
            return formatter.Deserialize(memoryStream);
        }
    }

    /// <summary>
    /// byte数组每一位取反,作为一个简单的加密
    /// </summary>
    /// <param name="bytes"></param>
    private static void SetOpposite(byte[] bytes)
    {
        for (int i = 0; i < bytes.Length; i++)
        {
            bytes[i] =(byte) ~bytes[i];
        }
    }
}
