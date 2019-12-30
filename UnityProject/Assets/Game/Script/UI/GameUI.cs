using UnityEngine;
using UnityEngine.UI;
using GameBase;
using GameBase.UI;

namespace NinjaJump
{
    [Resource("UI/GameUI")]
    public class GameUI : UIWindow
    {
        public Text CurHeight;
        public TouchEventHelper TouchBoard;


        [RegistEvent(GameEvent.OnHeightChange)]
        public void OnHeightChange(object[] param)
        {
            CurHeight.text = param[0].ToString();
        }
        
    }

}
