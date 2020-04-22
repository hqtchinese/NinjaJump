using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.UI
{
    /// <summary>
    /// UI窗口类,作为特殊的面板，可以由UIManager来管理层级
    /// </summary>
    public abstract class UIWindow : UIPanel
    {
        public bool hasMask;
        public override UIType Type => UIType.Window;

        protected Canvas m_mask;
        
        public override void Close()
        {
            UIManager.Instance.CloseWindow(this);
        }

        public override void Open()
        {
            UIManager.Instance.OpenWindow(this);
        }

        public virtual void TurnToWindow()
        {

        }

        public virtual void OnMaskClick(Vector2 pos)
        {

        }

        public void ResetMask()
        {
            if(!m_mask)
                m_mask = UIManager.Instance.CreateMask(this);

            Canvas canvas = GetComponent<Canvas>();
            m_mask.gameObject.SetActive(true);
            m_mask.sortingOrder = canvas.sortingOrder - 1;
        }

    }

}
