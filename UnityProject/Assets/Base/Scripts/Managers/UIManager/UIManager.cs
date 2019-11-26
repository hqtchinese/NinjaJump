using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase.UI
{
    public class UIManager : ManagerBase<UIManager>
    {
        public Transform UIRoot;
        
        public UIElement CurFocus { get; private set; }
        public bool IsFocusOnUI => CurFocus != null;

        private Dictionary<Type,UIWindow> m_windowDict;
        private Dictionary<Type,UIPanel> m_panelDict;
        private WindowStack m_windowStack;



        protected virtual void Awake()
        {
            Init();
        }
        

        /// <summary>
        /// 设置当前获得焦点的UIElement对象
        /// </summary>
        /// <param name="element">UIElement对象</param>
        public void SetFocus(UIElement element)
        {
            CurFocus = element;
        }

        /// <summary>
        /// 移除当前焦点的UIElement对象
        /// </summary>
        public void RemoveFocus()
        {
            CurFocus?.OnLostFocus();
            CurFocus = null;
        }


        /// <summary>
        /// 关闭所有窗口,并清理栈
        /// </summary>
        public void CloseAllWindow()
        {
            foreach (var item in m_windowDict.Values)
            {
                item.Close();
                m_windowStack.Clear();
            }
        }

        /// <summary>
        /// 关闭所有面板
        /// </summary>
        public void CloseAllPanel()
        {
            foreach (var item in m_panelDict.Values)
            {
                item.Close();
            }
        }

        /// <summary>
        /// 打开窗口并推至栈顶,如果还未创建则创建
        /// </summary>
        /// <typeparam name="T">窗口类型</typeparam>
        public T OpenWindow<T>() where T : UIWindow
        {
            if (m_windowDict.TryGetValue(typeof(T),out UIWindow window))
            {
                window.Open();
            }
            else
            {
                window = AddWindow<T>();
                window?.Open();
            }
            Push<T>();
            return window as T;
        }

        /// <summary>
        /// 关闭指定类型的窗口
        /// </summary>
        /// <typeparam name="T">窗口类型</typeparam>
        public void CloseWindow<T>() where T : UIWindow
        {
            if (m_windowDict.TryGetValue(typeof(T),out UIWindow window))
                window.Close();
        }


        /// <summary>
        /// 将已存在的窗口推至栈顶
        /// </summary>
        /// <typeparam name="T">UIWindow类型</typeparam>
        public void Push<T>() where T : UIWindow
        {
            if (m_windowDict.TryGetValue(typeof(T),out UIWindow window))
                m_windowStack.Push(window);
            else
                Debug.LogWarning("无法将不是UIManager创建的窗口推至栈顶,请使用UIManager.Instance.OpenWindow<UIWindow>()");
        }

        /// <summary>
        /// 返回上一级UI
        /// </summary>
        public void Return()
        {
            if (m_windowStack.Count > 0)
            {
                UIWindow window = m_windowStack.Pop();
                window.Close();
                CurFocus = m_windowStack.Peek();
            }
        }

        private void Init()
        {
            if (m_windowDict == null)
                m_windowDict = new Dictionary<Type, UIWindow>();

            if (m_panelDict == null)
                m_panelDict = new Dictionary<Type, UIPanel>();

            if (m_windowStack == null)
                m_windowStack = new WindowStack();

            if (UIRoot == null)
            {
                GameObject obj = GameObject.FindGameObjectWithTag("UIRoot");
                if (obj)
                    UIRoot = obj.transform;
                else
                    Debug.LogError("无法找到UIRoot,请确认场景中是否存在Tag为UIRoot的对象");
            }

            CreateDefaultUI();
        }

        private void CreateDefaultUI()
        {

        }

        
        private void ClearAllUI()
        {

        }

        /// <summary>
        /// 添加窗口至容器内,如果已经存在UIRoot下直接添加,不存在则创建
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private UIWindow AddWindow<T>() where T : UIWindow
        {
            UIWindow window = UIRoot.GetComponentInChildren<T>(true);

            if (window)
            {
                //先将对象设置为活动状态,否则非活动状态下在后面修改Canvas的overrideSorting属性会失败
                window.gameObject.SetActive(true);
                Canvas canvas = window.transform.GetOrAddComponent<Canvas>();
                canvas.overrideSorting = true;
                window.transform.GetOrAddComponent<GraphicRaycaster>();
                m_windowDict.Add(typeof(T),window);
            }
            else
            {
                window = CreateWindow<T>();
            }
            return window;
        }

        /// <summary>
        /// 根据类型创建窗口
        /// </summary>
        /// <typeparam name="T">UIWindow类型</typeparam>
        /// <returns>创建的UIWindow</returns>
        private UIWindow CreateWindow<T>() where T : UIWindow
        {
            Type UIType = typeof(T);
            ResourceAttribute attr = Attribute.GetCustomAttribute(UIType,typeof(ResourceAttribute)) as ResourceAttribute;
            string resPath = attr == null ? $"Prefabs/UI/Windows/{UIType.Name}" : attr.ResPath;

            GameObject obj = ResourceManager.Instance.Load<GameObject>(resPath);
            
            if (obj)
            {
                if (obj.GetComponent<UIWindow>() == null)
                {
                    Debug.LogError($"实例化的对象中找不到UIWindow组件");
                    return null;
                }

                GameObject gameObj = Instantiate<GameObject>(obj,UIRoot);
                UIWindow window = gameObj.GetComponent<UIWindow>();
                if (window)
                {
                    Canvas canvas = window.transform.GetOrAddComponent<Canvas>();
                    canvas.overrideSorting = true;
                    window.transform.GetOrAddComponent<GraphicRaycaster>();
                    m_windowDict.Add(UIType,window);
                }
                else
                {
                    Debug.LogError($"实例化的对象中找不到UIWindow组件");
                }
                return window;
            }
            else
            {
                Debug.LogError($"无法找到需要实例化的窗口,路径:{resPath}");
                return null;
            }
        }

        
        /// <summary>
        /// 将窗口对象推至栈顶
        /// </summary>
        /// <param name="window">UIWindow对象</param>
        private void Push(UIWindow window)
        {
            m_windowStack.Push(window);
        }
    }

}
