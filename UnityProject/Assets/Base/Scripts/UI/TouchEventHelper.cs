using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameBase.UI
{
    //触摸事件帮助类,检测当前对象上的触摸或者点击操作,发送封装好的事件:点击、长按、拖动
    public class TouchEventHelper : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public bool CheckLongPress;
        public float LongPressTime = 1f;
        public float DragThreshold = 0.2f;
        public Vector2 PressedPos { get; private set; }

        public event Action<Vector2> ClickEvent;
        public event Action<Vector2> BeginDragEvent;
        public event Action<Vector2> DragEvent;
        public event Action<Vector2> EndDragEvent;
        public event Action<Vector2> LongPressEvent;


        private TouchStatus m_status = TouchStatus.NoTouch;
        private float m_pressTimer;
        private bool m_isPressed;

        public void Update()
        {
            if (CheckLongPress && m_status == TouchStatus.Press)
            {
                m_pressTimer += Time.deltaTime;
                if (!m_isPressed && m_pressTimer > LongPressTime)
                {
                    DoLongPress();
                }
            }
        }

        private void Init()
        {
            m_status = TouchStatus.NoTouch;
            m_pressTimer = 0;
            m_isPressed = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            m_status = TouchStatus.Press;
            PressedPos = eventData.position;
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            if (!m_isPressed && m_status == TouchStatus.Press)
                DoClick(eventData.position);
            
            if (m_status == TouchStatus.Dragging)
                DoEndDrag(eventData.position);

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
                if (Vector2.Distance(PressedPos,eventData.position) > DragThreshold)
                {
                    DoBeginDrag(eventData.position);
                }
            }
        }
        
        private void DoClick(Vector2 pos)
        {
            ClickEvent?.Invoke(pos);
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
        }

        private void DoLongPress()
        {
            LongPressEvent?.Invoke(PressedPos);
            //这里不初始化,是为了触发长按之后还能继续拖动,
            // Init();
            m_isPressed = true;
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
