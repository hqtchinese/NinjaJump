using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.UI
{
    /// <summary>
    /// UI面板,可以由UIManager来创建，但是没有特殊功能，层级需要自己设置
    /// </summary>
    public abstract class UIPanel : UIElement
    {
        public override UIType Type => UIType.Panel;

        public virtual void Close()
        {
            UIManager.Instance.ClosePanel(this);
        }

        public virtual void Open()
        {
            UIManager.Instance.OpenPanel(this);
        }
        
        public void SetToTop(int order)
        {
            if(order < 0)
            {
                Debug.LogWarning("设置顶部面板,order需要大于0");
                return;
            }

            order = (BaseConst.UI_WINDOW_STACK_MAX_DEPTH + 1) * BaseConst.UI_SORTING_ORDER_SPACE + order;
            SetSortingOrder(order);
        }

        public void SetToBottom(int order)
        {
            if(order > 0)
            {
                Debug.LogWarning("设置底部面板,order需要小于0");
            }

            order = -BaseConst.UI_SORTING_ORDER_SPACE + order;
            SetSortingOrder(order);
        }

        public void SetSortingOrder(int order)
        {
            Canvas canvas = GetComponent<Canvas>();
            if(canvas)
            {
                canvas.sortingOrder = order;
            }
        }

    }
}
