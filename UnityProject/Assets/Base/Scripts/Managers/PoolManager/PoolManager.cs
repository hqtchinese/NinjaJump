using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Pool
{
    public class PoolManager : ManagerBase<PoolManager>
    {
        private const string DEFAULT_POOL = "default";
        private Dictionary<string,Pool> m_poolDict;

        public Pool this[string key]
        {
            get
            {
                if (m_poolDict.TryGetValue(key, out Pool val))
                {
                    return val;
                }
                else
                {
                    return CreatePool(key);
                }
            }
        }

        private void Awake()
        {
            m_poolDict = new Dictionary<string, Pool>();
        }

        private void Update()
        {
            foreach (var pool in m_poolDict.Values)
            {
                pool.Update();
            }
        }


        public GameObject Spawn(GameObject obj,Transform parent,Action<GameObject> init,float lifeTime = 0)
        {
            return this[DEFAULT_POOL].Spawn(obj,parent,init,lifeTime);
        }

        public void Despawn(GameObject obj,bool isDestroy = false)
        {
            this[DEFAULT_POOL].Despawn(obj, isDestroy);
        }

        public void ReleasePool(Pool pool)
        {
            foreach (var item in m_poolDict)
            {
                if (item.Value == pool)
                {
                    m_poolDict.Remove(item.Key);
                    pool.Destroy();
                    GC.Collect();
                    break;
                }
            }
        }

        public void ReleasePool(string name)
        {
            if (m_poolDict.TryGetValue(name, out Pool pool))
            {
                m_poolDict.Remove(name);
                pool.Destroy();
            }
            GC.Collect();
        }

        public void OnDestroy()
        {
            foreach (var pool in m_poolDict.Values)
            {
                pool.Destroy();
            }
        }

        private Pool CreatePool(string key)
        {
            if (m_poolDict.ContainsKey(key))
            {
                Debug.LogError("缓存池已存在");
                return null;
            }
            else
            {
                Pool pool = new Pool();
                m_poolDict.Add(key,pool);
                return pool;
            }
        }

    }
}
