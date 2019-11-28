using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NinjaJump
{
    public class TouchEventHelper : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public bool CheckLongPress;

        public event Action ClickEvent;
        public event Action<Vector2> BeginDragEvent;
        public event Action<Vector2> DragEvent;
        public event Action LongPressEvent;

        private TouchStatus m_status = TouchStatus.NoTouch;
        private float m_pressTimer;
        private Vector2 m_pressedPos;

        public void Update()
        {
            if (CheckLongPress && m_status == TouchStatus.Press)
            {
                m_pressTimer += Time.deltaTime;
                if (m_pressTimer > GameDefine.LONG_PRESS_TIME)
                {
                    DoLongPress();
                }
            }
        }

        private void Init()
        {
            m_status = TouchStatus.NoTouch;
            m_pressTimer = 0;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            m_status = TouchStatus.Press;
            m_pressedPos = eventData.position;
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            if (m_status == TouchStatus.Press)
                DoClick();
            
            if (m_status == TouchStatus.Dragging)
                Init();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (m_status == TouchStatus.Dragging)
            {
                DoDrag(eventData.position);
            }
            else if (m_status == TouchStatus.Press)
            {
                if (Vector2.Distance(m_pressedPos,eventData.position) > GameDefine.DRAG_BEGIN_DIS)
                {
                    DoBeginDrag(eventData.position);
                }
            }
        }
        
        private void DoClick()
        {
            ClickEvent?.Invoke();
            Init();
        }

        private void DoBeginDrag(Vector2 pos)
        {
            m_status = TouchStatus.Dragging;
            BeginDragEvent?.Invoke(pos);
        }

        private void DoDrag(Vector2 pos)
        {
            DragEvent?.Invoke(pos);
        }

        private void DoLongPress()
        {
            LongPressEvent?.Invoke();
            Init();
        }
    }
    
    public enum TouchStatus
    {
        NoTouch,
        Press,
        LongPress,
        Dragging
    }
}
