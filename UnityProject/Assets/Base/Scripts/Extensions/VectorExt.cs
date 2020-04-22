using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExt
{
    public static Vector2 ToVec2(this Vector3 v3)
    {
        return v3;
    }

    public static Vector3 ToVec3(this Vector2 v2)
    {
        return v2;
    }
}
