using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExt
{
    public static T GetOrAddComponent<T>(this Transform trans) where T : Component
    {
        return trans.gameObject.GetOrAddComponent<T>();
    }
}
