using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.UI
{
    /// <summary>
    /// 窗口栈,不是标准的栈,因为有移除中间元素和将中间元素推至栈顶的操作
    /// </summary>
    public class WindowStack
    {
        public int Count => m_windows.Count;

        private LinkedList<UIWindow> m_windows;

        public WindowStack()
        {
            m_windows = new LinkedList<UIWindow>();
        }

        /// <summary>
        /// 弹出栈顶的元素并返回,如果栈中没有元素则返回空
        /// </summary>
        /// <returns>UIWindow对象或者null</returns>
        public UIWindow Pop()
        {
            if (m_windows.Count > 0)
            {
                UIWindow window = m_windows.Last.Value;
                m_windows.RemoveLast();
                return window;
            }
            else
            {
                return null;
            }
        }
    
        /// <summary>
        /// 获得栈顶的元素,如果栈中没有则返回空
        /// </summary>
        /// <returns>UIWindow对象或者null</returns>
        public UIWindow Peek()
        {
            return m_windows.Count > 0 ? m_windows.Last.Value : null;
        }

        /// <summary>
        /// 将一个已存在于栈中或者不存在栈中的窗口推至栈顶
        /// </summary>
        /// <param name="window">UIWindow对象</param>
        public void Push(UIWindow window)
        {
            if (!window)
            {
                Debug.LogError("推至栈顶的值不能为空");
                return;
            }

            LinkedListNode<UIWindow> node = m_windows.Find(window);
            if (node != null)
            {
                m_windows.Remove(node);
                m_windows.AddLast(node);
                ResortLayer();
            }
            else
            {
                if (m_windows.Count >= BaseConst.UI_WINDOW_STACK_MAX_DEPTH)
                {
                    Debug.LogWarning($"窗口栈已满,栈中最多允许容纳{BaseConst.UI_WINDOW_STACK_MAX_DEPTH}个窗口");
                    return;
                }
                m_windows.AddLast(window);
                window.GetComponent<Canvas>().sortingOrder = m_windows.Count * BaseConst.UI_SORTING_ORDER_SPACE;
            }
            
            return;
        }

        /// <summary>
        /// 根据窗口栈中的顺序为窗口重设Layer
        /// </summary>
        public void ResortLayer()
        {
            int count = 0;
            foreach (var window in m_windows)
            {
                Canvas canvas = window.GetComponent<Canvas>();
                if (canvas && canvas.gameObject.activeSelf)
                {
                    canvas.sortingOrder = ++count * BaseConst.UI_SORTING_ORDER_SPACE;
                    if(window.hasMask)
                        window.ResetMask();
                }
            }
        }

        /// <summary>
        /// 返回窗口栈中是否存在该窗口
        /// </summary>
        /// <param name="window">UIWindow对象</param>
        /// <returns>true or false</returns>
        public bool Contains(UIWindow window)
        {
            return m_windows.Contains(window);
        }

        public void Remove(UIWindow window)
        {
            m_windows.Remove(window);
            ResortLayer();
        }

        /// <summary>
        /// 清除栈中所有对象
        /// </summary>
        public void Clear()
        {
            m_windows.Clear();
        }
    }

}
