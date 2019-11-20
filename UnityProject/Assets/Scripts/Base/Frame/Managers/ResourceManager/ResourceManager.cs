using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Object = UnityEngine.Object;


/*
    网上搜索Resources.Load本身就有缓存机制,但是也有人说自己做一份缓存效率会提高
    所以这里加载时根据参数来判断是否要缓存
 */
namespace GameBase
{

    public class ResourceManager : ManagerBase<ResourceManager>
    {
        private Dictionary<string,Object> m_objDict = new Dictionary<string, Object>();

        public T Load<T>(string path, bool cache = false) where T : Object
        {
            if (cache)
            {
                m_objDict.TryGetValue(path, out Object ret);
                if(!ret)
                {
                    T obj = Resources.Load<T>(path);
                    if (obj)
                        m_objDict.Add(path,obj);
                    else
                        Debug.LogError($"找不到需要读取的资源:{path}");

                    return obj;
                }
                else
                {
                    return ret as T;
                }
            }
            else
            {
                T obj = Resources.Load<T>(path);
                if (!obj)
                    Debug.LogError($"找不到需要读取的资源:{path}");
                return obj;
            }
        }

        

        public Object Load(string path, bool cache = false)
        {
            if (cache)
            {
                m_objDict.TryGetValue(path, out Object ret);
                if(!ret)
                {
                    ret = Resources.Load(path);
                    if (ret)
                        m_objDict.Add(path,ret);
                    else
                        Debug.LogError($"找不到需要读取的资源:{path}");

                    return ret;
                }
                else
                {
                    return ret;
                }
            }
            else
            {
                Object obj = Resources.Load(path);
                return obj;
            }

        }

        public GameObject Load<T>(bool cache = false) where T : MonoBehaviour
        {
            ResourceAttribute attr = Attribute.GetCustomAttribute(typeof(T),typeof(ResourceAttribute)) as ResourceAttribute;
            if (attr != null)
                return Load<GameObject>(attr.ResPath,cache);
            else
                return null;
        }

        public void Release(string path)
        {
            m_objDict.Remove(path);
        }

        public void ReleaseAll()
        {
            m_objDict.Clear();
            GC.Collect();
        }

    }
}

