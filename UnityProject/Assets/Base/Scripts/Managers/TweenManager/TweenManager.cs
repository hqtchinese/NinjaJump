using System;
using System.Collections.Generic;
using UnityEngine;


namespace GameBase.Tool.Tween
{
    public class TweenManager : ManagerBase<TweenManager>
    {
        private List<Tweener> m_tweenerList;

        protected void Awake()
        {
            m_tweenerList = new List<Tweener>();
        }

        protected void Update()
        {
            ProcessTweener();
        }

        public Tweener DoPosition(Transform target, Vector3 pos, float time)
        {
            PosTweener tweener = new PosTweener()
            {
                Target = target,
                EndPos = pos,
                AnimationTime = time,
            };
            tweener.Init();
            m_tweenerList.Add(tweener);
            return tweener;
        }

        public Tweener DoRotation(Transform target,Quaternion rot,float time)
        {
            RotTweener tweener = new RotTweener()
            {
                Target = target,
                EndRot = rot,
                AnimationTime = time
            };

            tweener.Init();
            m_tweenerList.Add(tweener);
            return tweener;
        }

        public Tweener DoAnchoredPosition(Transform target, Vector2 pos, float time)
        {
            AnchoredPosTweener tweener = new AnchoredPosTweener()
            {
                Target = target,
                EndPos = pos,
                AnimationTime = time  
            };

            tweener.Init();
            m_tweenerList.Add(tweener);
            return tweener;
        }

        private void ProcessTweener()
        {
            for (int i = 0; i < m_tweenerList.Count; i++)
            {
                if (m_tweenerList[i].Process(Time.deltaTime))
                {
                    //如果当前Tweener已经执行完成,则将队列的最后一个Tweener放到当前位置,再移除最后一个
                    //这样主要是为了在移除中间的元素时不用重新拷贝数组
                    int lastIndex = m_tweenerList.Count - 1;
                    m_tweenerList[i] = m_tweenerList[lastIndex];
                    m_tweenerList.RemoveAt(lastIndex);
                    //因为移过来的最后一个Tweener还没有执行,所以i--复位一下
                    i--;
                }
            }
        }

    }

    
}
