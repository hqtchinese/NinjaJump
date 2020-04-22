using System;
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
        protected UIPanel ParentPanel { get; set; }

        public virtual UIType Type => UIType.Element;

        protected virtual void Awake() 
        {
            FindParentPanel();
            RegistEvent();
        }

        protected void FindParentPanel()
        {
            UIPanel panel = this as UIPanel;
            if(panel)
            {
                ParentPanel = panel;
            }
            else
            {
                ParentPanel = transform.GetComponentInParent<UIPanel>();
                if(!ParentPanel)
                {
                    Debug.LogWarning("该UI元素不是UIPanel,并且找不到上级的UIPanel");
                }
            }
        }

        private void RegistEvent()
        {
            EventManager.Instance.Register(this);
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

