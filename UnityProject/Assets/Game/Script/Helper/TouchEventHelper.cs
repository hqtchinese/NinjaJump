using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NinjaJump
{
    //触摸事件帮助类,检测当前对象上的触摸或者点击操作,发送封装好的事件:点击、长按、拖动
    public class TouchEventHelper : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public bool CheckLongPress;
        public float LongPressTime = 1f;
        public float DragThreshold = 0.2f;

        public event Action ClickEvent;
        public event Action<Vector2> BeginDragEvent;
        public event Action<Vector2> DragEvent;
        public event Action<Vector2> EndDragEvent;
        public event Action LongPressEvent;

        private TouchStatus m_status = TouchStatus.NoTouch;
        private float m_pressTimer;
        private Vector2 m_pressedPos;

        public void Update()
        {
            if (CheckLongPress && m_status == TouchStatus.Press)
            {
                m_pressTimer += Time.deltaTime;
                if (m_pressTimer > LongPressTime)
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
                DoEndDrag(eventData.position);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (m_status == TouchStatus.Dragging)
            {
                DoDrag(eventData.position);
            }
            else if (m_status == TouchStatus.Press)
            {
                if (Vector2.Distance(m_pressedPos,eventData.position) > DragThreshold)
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

        private void DoEndDrag(Vector2 pos)
        {
            EndDragEvent?.Invoke(pos);
            Init();
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
