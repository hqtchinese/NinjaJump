using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T m_instance;
        public static T Instance 
        { 
            get
            {
                if(m_instance == null)
                {
                    Object obj = GameObject.FindObjectOfType(typeof(T));
                    if(obj)
                    {
                        m_instance = obj as T;
                    }
                }
                return m_instance;
            }
        }

        protected virtual void Awake()
        {
            if (!m_instance)
                m_instance = this as T;
        }
    }

    public class Singleton<T> where T : new()
    {
        private static T m_instance;
        public static T Instance
        {
            get
            {
                if(m_instance == null)
                {
                    m_instance = new T();
                }
                return m_instance;
            }
        }

    }

}
