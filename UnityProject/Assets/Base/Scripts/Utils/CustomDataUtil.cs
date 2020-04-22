using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameBase.Util;

namespace GameBase.Util
{
    public class CustomDataUtil
    {
        private const string CUSTOM_DATA_KEY = "_CustomKV_";
        private static List<KeyValuePair<string,string>> dataList;

        public static List<KeyValuePair<string,string>> DataList
        {
            get
            {
                if(dataList == null)
                {
                    dataList = (List<KeyValuePair<string,string>>)SerializeUtil.LoadFromPlayerPref(CUSTOM_DATA_KEY);
                    if(dataList == null)
                        dataList = new List<KeyValuePair<string, string>>();
                }
                
                return dataList;
            }
        }

        public static void SetKV(string key,string val)
        {
            KeyValuePair<string,string> kv = new KeyValuePair<string,string>(key,val);
            for (int i = 0; i < DataList.Count; i++)
            {
                if(DataList[i].Key == key)
                {
                    DataList[i] = kv;
                    return;
                }
            }
            DataList.Add(kv);
        }

        public static string GetVal(string key)
        {
            for (int i = 0; i < DataList.Count; i++)
            {
                if(DataList[i].Key == key)
                {
                    return DataList[i].Value;
                }
            }
            return null;
        }

        public static List<KeyValuePair<string,string>> GetList()
        {
            if(dataList == null)
            {
                dataList = (List<KeyValuePair<string,string>>)SerializeUtil.LoadFromPlayerPref(CUSTOM_DATA_KEY);
                if(dataList == null)
                    dataList = new List<KeyValuePair<string, string>>();
            }
            
            return dataList;
        }

        public static void SaveData()
        {
            SerializeUtil.SaveToPlayerPref(CUSTOM_DATA_KEY,DataList);
        }
    }

}
