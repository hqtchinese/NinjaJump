using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExt  
{
    public static T GetOrAddComponent<T>(this GameObject obj) where T : Component
    {
        T comp = obj.GetComponent<T>();
        if (!comp)
            comp = obj.AddComponent<T>();
        
        return comp;
    }

    
}
