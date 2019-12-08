using UnityEngine;
using UnityEngine.UI;
using GameBase;
using GameBase.UI;

namespace NinjaJump 
{
    [Resource("UI/MainMenu")]
    public class MainMenu : UIWindow
    {

        public Button ClassicModeBtn;

        protected override void Awake()
        {
            base.Awake();
            ClassicModeBtn.onClick.AddListener(OnClassicModeBtnClick);
        }
        
        public void OnClassicModeBtnClick()
        {
            GameController.Instance.StartClassicMode();
            Close();
        }

    }

}
