using UnityEngine;
using GameBase.UI;
using UnityEngine.UI;
using GameBase.Editor;

[RequireComponent(typeof(Button))]
public class CloseButton : UIElement
{   
    [RenameField("是否返回按钮")]
    public bool IsReturn;
    
    protected override void Awake() 
    {
        base.Awake();
        Button button = transform.GetComponent<Button>();
        if (button)
            button.onClick.AddListener(OnBtnClick);
        else
            Debug.LogWarning("这个对象上找不到Button组件");
    }

    private void OnBtnClick()
    {
        if(IsReturn)
            UIManager.Instance.Return();
        else
            ParentPanel?.Close();
    }

}
