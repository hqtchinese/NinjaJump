using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.UI
{
    public enum UIType
    {
        Element,
        Window,
        Panel
    }

    [DisallowMultipleComponent]
    public class UIElement : MonoBehaviour
    {
        public bool IsFocus { get; set; }
        protected UIWindow ParentWindow { get; set; }

        public virtual UIType Type => UIType.Element;

        protected virtual void Awake() 
        {
            FindParentWindow();
        }

        protected void FindParentWindow()
        {
            Transform trans = transform;
            int safeCount = 0;
            while(true)
            {
                UIWindow tmp = trans.GetComponent<UIWindow>();
                if (tmp)
                {
                    ParentWindow = tmp;
                    break;
                }
                else
                {
                    if (trans.transform.parent)
                    {
                        trans = trans.transform.parent;
                    }
                    else
                    {
                        Debug.LogWarning($"对象:({gameObject.name})\n该UI元素不是一个窗口,且找不到他的上级窗口");
                        break;
                    }
                }

                if (++safeCount > BaseConst.UI_MAX_DEPTH)
                {
                    Debug.LogWarning($"对象:({gameObject.name})\n搜索层级超过{BaseConst.UI_MAX_DEPTH}层,停止寻找上级窗口");
                    break;
                }
            }
        }


        public virtual void OnLostFocus()
        {

        }

        public virtual void OnFocus()
        {

        }

        public virtual void Refresh()
        {

        }

        protected virtual void OnDestroy()
        {
            
        }
    }

}

