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

        public static Tweener DoPos(this Transform target, float x, float y, float z, float time)
        {
            Vector3 pos = new Vector3(x,y,z);
            return TweenManager.Instance.DoPosition(target,pos,time);
        }

        public static Tweener DoLocalPos(this Transform target, Vector3 pos, float time)
        {
            return TweenManager.Instance.DoPosition(target,target.position - target.localPosition + pos,time);
        }

        public static Tweener DoLocalPos(this Transform target, float x, float y, float z, float time)
        {
            Vector3 pos = new Vector3(x,y,z);
            return TweenManager.Instance.DoPosition(target,target.position - target.localPosition + pos,time);
        }

        public static Tweener DoRot(this Transform target, Quaternion rot, float time)
        {
            return TweenManager.Instance.DoRotation(target,rot,time);
        }

        public static Tweener DoScale(this Transform target, Vector3 scale, float time)
        {
            return TweenManager.Instance.DoScale(target,scale,time);
        }

        public static Tweener DoScale(this Transform target, float scaleNum, float time)
        {
            Vector3 scale = new Vector3(scaleNum,scaleNum,scaleNum);
            return TweenManager.Instance.DoScale(target,scale,time);
        }

        public static Tweener DoAnchoredPos(this RectTransform target, Vector2 pos, float time)
        {
            return TweenManager.Instance.DoAnchoredPosition(target,pos,time);
        }

        public static Tweener DoAnchoredPos(this Transform target, Vector2 pos, float time)
        {
            return TweenManager.Instance.DoAnchoredPosition(target,pos,time);
        }
    }
    
}
