using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase
{
    public class LocalAsset<T> : ScriptableObject where T : LocalAsset<T>
    {
        private static T m_asset;
        public static T Asset 
        { 
            get
            {
                if (!m_asset)
                {
                    ResourceAttribute attr = Attribute.GetCustomAttribute(typeof(T),typeof(ResourceAttribute)) as ResourceAttribute;
                    if (attr != null)
                        m_asset = ResourceManager.Instance.Load<T>(attr.ResPath);
                    m_asset.Init();
                }
                return m_asset;
            }
        }
        
        public virtual void Init()
        {
        }
    }
    
}
