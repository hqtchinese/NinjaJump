using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.UI
{
    /// <summary>
    /// UI面板,是一种特殊的UI窗口,一般会占满整个屏幕,并且一般处于UI层级的最底部或者最顶部
    /// </summary>
    public abstract class UIPanel : UIWindow
    {
        public override UIType Type => UIType.Panel;


        
    }

}
