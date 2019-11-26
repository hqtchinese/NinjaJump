using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameBase;

namespace NinjaJump
{
    
    public class GameController : MonoSingleton<GameController>
    {
        public Vector2 ViewPos;
        public GameState State { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            
        }

        //加载游戏场景中的物体、UI等
        private void InitGameScene()
        {

        }

        


    }

    public enum GameState
    {
        Ready,
        Gaming,
        Pause,
        GameOver,

    }
}
