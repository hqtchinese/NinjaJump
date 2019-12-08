using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameBase;
using GameBase.UI;

namespace NinjaJump
{
    public class GameController : MonoSingleton<GameController>
    {

        public Transform GameTrans;
        public Transform EnvTrans;



        public Vector2 ViewPos;
        public Vector2 Speed;

        public GameState State { get; private set; }
        public RoleDock Role { get; private set; }
        public GameModeBase GameMode { get; private set; }


        protected void Start()
        {
            UIManager.Instance.OpenWindow<MainMenu>();
        }

        public void Update()
        {
            GameMode?.GameUpdate(Time.deltaTime);
        }

        public void StartClassicMode()
        {
            GameMode = new ClassicMode();
            GameMode.Init();
            GameMode.Start();
        }
    }
    
}
