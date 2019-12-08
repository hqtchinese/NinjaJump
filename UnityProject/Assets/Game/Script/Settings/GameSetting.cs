using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameBase;
using GameBase.Editor;

namespace NinjaJump
{

    [Resource("")]
    [CreateAssetMenu(menuName = "GameAssets/Settings/GameSetting",fileName = "GameSetting")]
    public class GameSetting : LocalAsset<GameSetting>
    {
        [RenameField("木板最小间距")]
        public float BoardSpaceMin;
        [RenameField("木板最大间距")]
        public float BoardSpaceMax;

    }

}
