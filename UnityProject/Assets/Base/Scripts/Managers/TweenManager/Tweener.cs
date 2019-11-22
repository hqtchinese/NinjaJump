﻿using System;
using UnityEngine;

namespace GameBase.Tool.Tween
{
    public enum CurveType
    {
        Default,
        Bounce
    }
    public abstract class Tweener
    {
        public Transform Target { get; set; }
        public AnimationCurve Curve { get; set; }
        public float AnimationTime { get; set; }

        protected Action Callback { get; set; }
        protected float m_timer { get; set; }

        public virtual void Init()
        {
            if (Curve == null)
                Curve = CurveAsset.Asset.curveDict[CurveType.Default].curve;

            m_timer = 0;
            Callback = null;
        }

        public bool Process(float deltaTime)
        {
            m_timer += deltaTime;
            if (m_timer < 0)
                return false;
            if (m_timer < AnimationTime)
            {
                Action(m_timer / AnimationTime);
                return false;
            }
            else
            {
                Action(1);
                Callback?.Invoke();
                return true;
            }
        }
        
        public Tweener OnComplete(Action callback)
        {
            Callback = callback;
            return this;
        }

        public Tweener Delay(float delay)
        {
            m_timer -= delay;
            return this;
        }

        public Tweener SetCurve(CurveType type)
        {
            Curve = CurveAsset.Asset.curveDict[type].curve;
            return this;
        }

        protected abstract void Action(float factor);

    }

    public class PosTweener : Tweener
    {
        public Vector3 EndPos { get; set; }
        
        private Vector3 m_startPos;

        public override void Init()
        {
            base.Init();
            m_startPos = Target.position;
        }

        protected override void Action(float factor)
        {
            factor = Curve.Evaluate(factor);
            Target.position = Vector3.LerpUnclamped(m_startPos,EndPos,factor);
        }
    }
    
    public class RotTweener : Tweener
    {
        public Quaternion EndRot { get; set; }

        private Quaternion m_startRot;

        public override void Init()
        {
            base.Init();
            m_startRot = Target.rotation;
        }

        protected override void Action(float factor)
        {
            factor = Curve.Evaluate(factor);
            Quaternion.LerpUnclamped(Target.rotation,EndRot,factor);
        }
        
    }


    public class AnchoredPosTweener : Tweener
    {
        public Vector2 EndPos { get; set; }
        private RectTransform RectTarget => Target as RectTransform;
        private Vector2 m_startPos;
        public override void Init()
        {
            base.Init();
            m_startPos = RectTarget.anchoredPosition;
        }

        protected override void Action(float factor)
        {
            factor = Curve.Evaluate(factor);
            Vector2.LerpUnclamped(m_startPos,EndPos,factor);
        }
    }
}