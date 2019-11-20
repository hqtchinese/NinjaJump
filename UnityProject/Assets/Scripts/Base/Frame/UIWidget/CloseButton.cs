using UnityEngine;
using GameBase.UI;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CloseButton : UIElement
{
    private Button m_button;
    protected override void Awake() 
    {
        base.Awake();
        m_button = transform.GetComponent<Button>();
        if (m_button)
            m_button.onClick.AddListener(CloseWindow);
        else
            Debug.LogWarning("这个对象上找不到Button组件");
    }

    private void CloseWindow()
    {
        ParentWindow?.Close();
    }

    protected override void OnDestroy() 
    {
        base.OnDestroy();
    }
}
