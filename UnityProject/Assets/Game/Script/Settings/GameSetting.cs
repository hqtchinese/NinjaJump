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
        public int BoardPoolSize;
        [RenameField("木板长度")]
        public float BoardLength;
        [RenameField("木板水平偏移")]
        public float BoardHorizonOffset;
        [RenameField("木板的Layer")]
        public LayerMask BoardLayer;
        [RenameField("角色的Layer")]
        public LayerMask RoleLayer;
        [RenameField("角色基础移动速度")]
        public float BaseRoleSpeed;
        [RenameField("重力加速度")]
        public float Gravity;
    }

}
