using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameBase.Pool
{
    public class Pool 
    {
        private Dictionary<Object,Product> m_productDict;
        private Dictionary<GameObject,Production> m_instanceDict;

        public Pool()
        {
            m_productDict = new Dictionary<Object, Product>();
            m_instanceDict = new Dictionary<GameObject, Production>();
        }


        public GameObject Spawn(GameObject obj, Transform parent, Action<GameObject> init, float lifeTime = float.MaxValue)
        {
            if (init == null)
                throw new Exception("生成对象必须要有初始化方法!");

            if (m_productDict.TryGetValue(obj, out Product product))
            {
                Production production = product.Spawn(parent, lifeTime);
                if (!m_instanceDict.ContainsKey(production.Obj))
                {
                    m_instanceDict.Add(production.Obj,production);
                }

                init(production.Obj);
                return production.Obj;
            }
            else
            {
                product = new Product(obj);
                m_productDict.Add(obj,product);
                Production production = product.Spawn(parent, lifeTime);
                init(production.Obj);
                return production.Obj;
            }
        }

        public void Despawn(GameObject obj, bool isDestroy = false)
        {
            if (m_instanceDict.TryGetValue(obj,out Production production))
            {
                production.Product.Despawn(production, isDestroy);
                if (isDestroy)
                {
                    m_instanceDict.Remove(obj);
                }
            }
            else
                Debug.LogWarning("无法找到对象实例的记录");            
        }

        public void Update()
        {
            foreach (var product in m_productDict.Values)
            {
                product.Update();
            }
        }

        public void Destroy()
        {
            foreach (var item in m_productDict)
            {
                item.Value.Destroy();
            }

            m_productDict = null;
            m_instanceDict = null;
        }
    }

}
