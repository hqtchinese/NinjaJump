using System;
using UnityEngine;
using GameBase.UI;
using GameBase.Editor;
using UnityEngine.UI;

public class OpenWindowButton : UIElement
{

    public GameObject OpenWindow;
    public bool closeParent;
    private Type m_windowType;

    protected override void Awake()
    {
        base.Awake();
        Button button = transform.GetComponent<Button>();
        if(OpenWindow)
        {
            UIWindow window = OpenWindow.GetComponent<UIWindow>();
            if(window)
                m_windowType = window.GetType();
            else
                Debug.LogError("预制体上找不到UIWindow组件");
        }
        if(button)
            button.onClick.AddListener(OnBtnClick);
        else
            Debug.LogWarning("这个对象上找不到Button组件!");
    }

    private void OnBtnClick()
    {
        if(closeParent)
        {
            if(ParentPanel.Type == UIType.Window)
                ParentPanel.Close();
        }
        
        if(m_windowType != null)
        {
            UIManager.Instance.OpenWindow(m_windowType);
        }
    }

}
