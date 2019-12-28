using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameBase
{

    public class GameMain : MonoBehaviour
    {
        private static GameMain m_instance;

        public static GameMain Instance 
        {
            get
            {
                if(m_instance == null)
                {
                    Object obj = GameObject.FindObjectOfType(typeof(GameMain));
                    if(obj)
                    {
                        m_instance = obj as GameMain;
                    }
                    else
                    {
                        GameObject foo = new GameObject("GameMain");
                        m_instance = foo.AddComponent<GameMain>();
                    }
                }
                return m_instance;
            }
        }


        public float GameTime { get; private set; }

        private Dictionary<Type,ManagerBase> managerDict = new Dictionary<System.Type, ManagerBase>();
        

        private void Awake()
        {
            Init();
        }

        private void Update()
        {
            GameTime += Time.deltaTime;
        }

        public T AddManager<T>() where T : ManagerBase
        {
            Type type = typeof(T);
            if (!managerDict.TryGetValue(type,out ManagerBase manager))
            {
                if (!manager)
                {
                    manager = GameObject.FindObjectOfType(type) as T;
                    if (!manager)
                    {
                        GameObject obj = new GameObject(type.Name);
                        obj.transform.SetParent(transform);
                        manager = obj.AddComponent<T>();
                    }
                    managerDict.Add(type,manager);
                }
            }
            return manager as T;
        }


        public void Init()
        {
            DontDestroyOnLoad(gameObject);
        }


    }
}
