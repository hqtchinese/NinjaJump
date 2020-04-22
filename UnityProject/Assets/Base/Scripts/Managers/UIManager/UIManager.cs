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
        public Canvas Mask;
        public bool IsFocusOnUI => CurFocus != null;

        private Dictionary<Type,UIWindow> m_windowDict;
        private Dictionary<Type,UIPanel> m_panelDict;
        private UIPanel m_topPanel;
        private UIPanel m_bottomPanel;
        private WindowStack m_windowStack;      //窗口栈,处理活动窗口
        private Stack<UIWindow> m_routeStack;   //窗口路由栈,主要处理界面的返回,包含非活动窗口
        protected virtual void Awake()
        {
            Init();
        }
        
        private void Init()
        {
            if (m_windowDict == null)
                m_windowDict = new Dictionary<Type, UIWindow>();

            if (m_panelDict == null)
                m_panelDict = new Dictionary<Type, UIPanel>();

            if (m_windowStack == null)
                m_windowStack = new WindowStack();

            if(m_routeStack == null)
                m_routeStack = new Stack<UIWindow>();

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

        /// <summary>
        /// 设置当前获得焦点的UIElement对象
        /// </summary>
        /// <param name="element">UIElement对象</param>
        public void SetFocus(UIElement element)
        {
            if(CurFocus && CurFocus != element)
                CurFocus.OnLostFocus();
            
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
            
        }

        /// <summary>
        /// 打开窗口并推至栈顶,如果还未创建则创建
        /// </summary>
        /// <typeparam name="T">窗口类型</typeparam>
        public T OpenWindow<T>() where T : UIWindow
        {
            return OpenWindow(typeof(T)) as T;
        }

        /// <summary>
        /// 打开窗口并推至栈顶,如果还未创建则创建
        /// </summary>
        public UIWindow OpenWindow(Type type)
        {
            if (!m_windowDict.TryGetValue(type,out UIWindow window))
                window = AddWindow(type);

            window.Open();

            return window;
        }
        
        /// <summary>
        /// 设置一个窗口为打开状态
        /// </summary>
        public void OpenWindow(UIWindow window)
        {
            window.gameObject.SetActive(true);
            Push(window);
            if(window.hasMask)
                window.ResetMask();
        }

        public UIPanel OpenPanel<T>() where T : UIPanel
        {
            return OpenPanel(typeof(T));
        }

        public UIPanel OpenPanel(Type type)
        {
            if(!m_panelDict.TryGetValue(type, out UIPanel panel))
                panel = AddPanel(type);

            panel.Open();

            return panel;
        }

        public void OpenPanel(UIPanel panel)
        {
            panel.gameObject.SetActive(true);
        }

        /// <summary>
        /// 关闭指定类型的窗口
        /// </summary>
        /// <typeparam name="T">窗口类型</typeparam>
        public void CloseWindow<T>() where T : UIWindow
        {
            if(m_windowDict.TryGetValue(typeof(T),out UIWindow window))
            {
                window.Close();
            }
        }

        public void CloseWindow(UIWindow window)
        {
            window.gameObject.SetActive(false);
            RemoveFromStack(window);
        }

        public void ClosePanel<T>() where T : UIPanel
        {
            if(m_panelDict.TryGetValue(typeof(T), out UIPanel panel))
            {
                panel.Close();
            }
        }

        public void ClosePanel(UIPanel panel)
        {
            panel.gameObject.SetActive(false);
        }

        /// <summary>
        /// 将已存在的窗口推至栈顶
        /// </summary>
        public void Push(Type type)
        {
            if (m_windowDict.TryGetValue(type,out UIWindow window))
                m_windowStack.Push(window);
            else
                Debug.LogWarning("无法将不是UIManager创建的窗口推至栈顶,请使用UIManager.Instance.OpenWindow<UIWindow>()");
        }

        public void Push(UIWindow window)
        {
            m_windowStack.Push(window);
            if(m_routeStack.Contains(window))
            {
                while (m_routeStack.Peek() != window)
                {
                    m_routeStack.Pop();
                }
            }
            else
            {
                m_routeStack.Push(window);
            }
        }

        public void RemoveFromStack(UIWindow window)
        {
            m_windowStack.Remove(window);
            if(m_routeStack.Contains(window))
            {
                while (m_routeStack.Peek() != window)
                {
                    m_routeStack.Pop();
                }
                if(m_routeStack.Count > 1)
                {
                    m_routeStack.Pop();
                }
            }
        }

        public void TurnToWindow<T>(UIWindow from) where T: UIWindow
        {
            TurnToWindow(from,typeof(T));
        }

        public void TurnToWindow(UIWindow from, Type type)
        {
            if(!m_windowDict.TryGetValue(type, out UIWindow to))
            {
                to = AddWindow(type);
            }
            TurnToWindow(from,to);
        }

        public void TurnToWindow(UIWindow from, UIWindow to)
        {
            if(m_routeStack.Contains(to))
            {
                while (m_routeStack.Peek() != to)
                {
                    m_routeStack.Pop();
                }
            }
            else
            {
                m_routeStack.Push(to);
            }
        }

        /// <summary>
        /// 返回上一级UI,只包含活动状态的窗口
        /// </summary>
        // public void Return()
        // {
        //     if (m_windowStack.Count > 0)
        //     {
        //         UIWindow window = m_windowStack.Pop();
        //         window.Close();
        //         CurFocus = m_windowStack.Peek();
        //     }
        // }

        public void Return()
        {
            UIWindow window = m_routeStack.Peek();
            Return(window);
        }

        public void Return(UIWindow curWindow)
        {
            if(curWindow == null)
                return;
            
            if(m_routeStack.Contains(curWindow))
            {
                while (m_routeStack.Peek() != curWindow)
                {
                    m_routeStack.Pop();
                }

                if(m_routeStack.Count > 1)
                {
                    UIWindow foo = m_routeStack.Pop();
                    foo.Close();
                    m_routeStack.Peek().Open();
                }
            }
            else
            {
                curWindow.Close();
            }
        }

        public Canvas CreateMask(UIWindow window)
        {
            GameObject obj = new GameObject("Mask",typeof(RectTransform));
            RectTransform trans = obj.GetComponent<RectTransform>();
            BlockRayCast blocker = obj.AddComponent<BlockRayCast>();
            blocker.color = new Color32(0,0,0,100);
            TouchEventHelper touchHelper = obj.AddComponent<TouchEventHelper>();
            touchHelper.ClickEvent += window.OnMaskClick;
            trans.SetParent(window.transform);
            trans.anchorMin = new Vector2(0.5f,0.5f);
            trans.anchorMax = new Vector2(0.5f,0.5f);
            trans.offsetMin = new Vector2(-Screen.width / 2,-Screen.height / 2);
            trans.offsetMax = new Vector2(Screen.width / 2,Screen.height / 2);
            Canvas canvas = obj.AddComponent<Canvas>();
            obj.AddComponent<GraphicRaycaster>();
            canvas.overrideSorting = true;
            return canvas;
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
            return AddWindow(typeof(T));
        }

        /// <summary>
        /// 添加窗口至容器内,如果已经存在UIRoot下直接添加,不存在则创建
        /// </summary>
        private UIWindow AddWindow(Type type)
        {
            UIWindow window = UIRoot.GetComponentInChildren(type) as UIWindow;
            if (window)
            {
                //先将对象设置为活动状态,否则非活动状态下在后面修改Canvas的overrideSorting属性会失败
                window.gameObject.SetActive(true);
                Canvas canvas = window.gameObject.GetOrAddComponent<Canvas>();
                canvas.overrideSorting = true;
                window.transform.GetOrAddComponent<GraphicRaycaster>();
                m_windowDict.Add(type,window);
            }
            else
            {
                window = CreateWindow(type);
            }
            return window;
        }

        private UIPanel AddPanel(Type type)
        {
            UIPanel panel = UIRoot.GetComponentInChildren(type) as UIPanel;
            if(panel)
            {
                panel.gameObject.SetActive(true);
                Canvas canvas = panel.gameObject.GetOrAddComponent<Canvas>();
                canvas.overrideSorting = true;
                panel.gameObject.GetOrAddComponent<GraphicRaycaster>();
                m_panelDict.Add(type,panel);
            }
            else
            {
                panel = CreatePanel(type);
            }
            return panel;
        }

        /// <summary>
        /// 根据类型创建窗口
        /// </summary>
        private UIWindow CreateWindow(Type UIType)
        {
            ResourceAttribute attr = Attribute.GetCustomAttribute(UIType,typeof(ResourceAttribute),false) as ResourceAttribute;
            string resPath = attr == null ? $"Prefabs/UI/Windows/{UIType.Name}" : attr.ResPath;

            GameObject obj = ResourceManager.Instance.Load<GameObject>(resPath);
            
            if (obj)
            {
                if (obj.GetComponent<UIWindow>() == null)
                {
                    Debug.LogError($"需要实例化的对象中找不到UIWindow组件");
                    return null;
                }

                GameObject gameObj = Instantiate<GameObject>(obj,UIRoot);
                UIWindow window = gameObj.GetComponent<UIWindow>();
                Canvas canvas = window.transform.GetOrAddComponent<Canvas>();
                canvas.overrideSorting = true;
                window.transform.GetOrAddComponent<GraphicRaycaster>();
                m_windowDict.Add(UIType,window);
                
                return window;
            }
            else
            {
                Debug.LogError($"无法找到需要实例化的窗口,路径:{resPath}");
                return null;
            }
        }


        /// <summary>
        /// 根据类型创建面板
        /// </summary>
        private UIPanel CreatePanel(Type UIType)
        {
            ResourceAttribute attr = Attribute.GetCustomAttribute(UIType,typeof(ResourceAttribute),false) as ResourceAttribute;
            string resPath = attr == null ? $"Prefabs/UI/Panels/{UIType.Name}" : attr.ResPath;

            GameObject obj = ResourceManager.Instance.Load<GameObject>(resPath);
            
            if (obj)
            {
                if (obj.GetComponent<UIPanel>() == null)
                {
                    Debug.LogError($"需要实例化的对象中找不到UIPanel组件");
                    return null;
                }

                GameObject gameObj = Instantiate<GameObject>(obj,UIRoot);
                UIPanel panel = gameObj.GetComponent<UIPanel>();
                Canvas canvas = panel.transform.GetOrAddComponent<Canvas>();
                canvas.overrideSorting = true;
                panel.transform.GetOrAddComponent<GraphicRaycaster>();
                m_panelDict.Add(UIType,panel);

                return panel;
            }
            else
            {
                Debug.LogError($"无法找到需要实例化的面板,路径:{resPath}");
                return null;
            }
        }

        /// <summary>
        /// 释放所有窗口
        /// </summary>
        private void ReleaseAll()
        {
            m_windowDict.Clear();
            m_windowStack.Clear();
            m_panelDict.Clear();
        }
    }

}
