using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameBase;

namespace NinjaJump
{
    [Resource("Settings/SkinSetting")]
    [CreateAssetMenu(menuName = "GameAssets/Settings/SkinSetting",fileName = "SkinSetting")]
    public class SkinSetting : LocalAsset<SkinSetting>
    {
        [SerializeField]
        protected GameObject[] RoleSkins;

        [SerializeField]
        protected GameObject[] BoardSkins;

        [SerializeField]
        protected GameObject[] EnvSkins;

        public GameObject GetRoleSkin(int id) 
        {
            if (id > -1 && id < RoleSkins.Length)
                return RoleSkins[id];
            else
                return null;
        }

        public GameObject GetBoardSkin(int id) 
        {
            if (id > -1 && id < BoardSkins.Length)
                return BoardSkins[id];
            else
                return null;
        }

        public GameObject GetEnvSkin(int id) 
        {
            if (id > -1 && id < EnvSkins.Length)
                return EnvSkins[id];
            else
                return null;
        }
    }

}
