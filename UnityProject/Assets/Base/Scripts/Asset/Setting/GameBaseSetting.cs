using System;
using System.Collections.Generic;
using UnityEngine;
using GameBase.Editor;

namespace GameBase
{
    [Resource("Asset/Setting/GameBaseSetting")]
    [CreateAssetMenu(menuName = "Setting/GameBaseSetting",fileName = "GameBaseSetting")]
    public class GameBaseSetting : LocalAsset<GameBaseSetting>
    {
        
        public int fps = 60;
        public bool enableLog;
        
        public override void Init()
        {
            
        }
        
    }
    
}