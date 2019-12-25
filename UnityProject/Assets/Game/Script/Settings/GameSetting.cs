using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameBase;
using GameBase.Editor;

namespace NinjaJump
{

    [Resource("Settings/GameSetting")]
    [CreateAssetMenu(menuName = "GameAssets/Settings/GameSetting",fileName = "GameSetting")]
    public class GameSetting : LocalAsset<GameSetting>
    {
        [RenameField("木板最小间距")]
        public float BoardSpaceMin;
        [RenameField("木板最大间距")]
        public float BoardSpaceMax;
        [RenameField("总共木板对象的数量")]
        public int BoardListLength;
    }

}
