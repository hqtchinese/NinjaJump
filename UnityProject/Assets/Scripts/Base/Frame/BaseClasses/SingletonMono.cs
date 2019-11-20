using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase
{
    public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
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

    }

}
