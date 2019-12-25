using System.Collections;
using System;
using UnityEngine;


namespace NinjaJump
{
    [Serializable]
    public class Player
    {
        public string playerName;
        public float highScore;
        public int roleID;
        public int envID;
        public int boardID;

    #region Settings
        //操作灵敏度
        public float controlSensity;
        //音效音量
        public float AEVolume;
        //音乐音量
        public float BGMVolume;



    #endregion
    }

}
