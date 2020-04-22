using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Util
{
    public class ObjPool<T> where T : class ,new() 
    {
        protected List<T> m_list;

        public ObjPool()
        {
            m_list = new List<T>();
        }

        public T Request()
        {
            if(m_list.Count > 0)
            {
                int lastIndex = m_list.Count - 1;
                T obj = m_list[lastIndex];
                m_list.RemoveAt(m_list.Count - 1);
                return obj;
            }
            else
            {
                return new T();
            }
        }

        public void GiveBack(T obj)
        {
            m_list.AddIfNotContains(obj);
        }
    }

}
