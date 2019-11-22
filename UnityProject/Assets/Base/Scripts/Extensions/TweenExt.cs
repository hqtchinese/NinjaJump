using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Tool.Tween
{
    public static class TweenExt
    {
        public static Tweener DoPos(this Transform target, Vector3 pos, float time)
        {
            return TweenManager.Instance.DoPosition(target,pos,time);
        }

        public static Tweener DoLocalPos(this Transform target, Vector3 pos, float time)
        {
            return TweenManager.Instance.DoPosition(target,target.position - target.localPosition + pos,time);
        }

        public static Tweener DoRot(this Transform target, Quaternion rot, float time)
        {
            return TweenManager.Instance.DoRotation(target,rot,time);
        }

        public static Tweener DoAnchoredPos(this RectTransform target, Vector2 pos, float time)
        {
            return TweenManager.Instance.DoAnchoredPosition(target,pos,time);
        }
    }
    
}
