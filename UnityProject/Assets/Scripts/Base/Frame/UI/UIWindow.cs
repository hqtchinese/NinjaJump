using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.UI
{
    /// <summary>
    /// UI窗口类,是一种用作装载其他UI元素的特殊UI元素
    /// </summary>
    public abstract class UIWindow : UIElement
    {
        public bool hasMask;
        public override UIType Type => UIType.Window;

        public virtual void Close()
        {
            this.gameObject.SetActive(false);
            OnClose();
        }

        public virtual void Open()
        {
            this.gameObject.SetActive(true);
            OnOpen();
        }

        protected virtual void OnClose()
        {

        }

        protected virtual void OnOpen()
        {

        }
        protected override void OnDestroy()
        {

        }

    }

}
