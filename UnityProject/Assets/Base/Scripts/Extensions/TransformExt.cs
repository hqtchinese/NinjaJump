using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExt
{
    public static T GetOrAddComponent<T>(this Transform trans) where T : Component
    {
        return trans.gameObject.GetOrAddComponent<T>();
    }

    public static void FaceToDir(this Transform trans,Vector3 dir)
    {
        float angle = Vector3.Angle(dir,Vector3.up);
        var foo = Vector3.Cross(dir,Vector3.up);
        if(foo.z > 0)
            angle = -angle;
        trans.rotation = Quaternion.Euler(0,0,angle);
    }
}
