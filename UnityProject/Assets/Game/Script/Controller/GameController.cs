using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        public bool IsGaming => State == GameState.Gaming;

        public RoleDock Role { get; private set; }
        public GameModeBase GameMode { get; private set; }
        public Player PlayerInfo { get; private set; }



        protected void Start()
        {
            UIManager.Instance.OpenWindow<MainMenu>();

            PlayerInfo = SerializeUtil.LoadFromPlayerPref("Player") as Player;
            if (PlayerInfo == null)
                PlayerInfo = new Player();
            
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

        protected void OnDestroy()
        {
            SerializeUtil.SaveToPlayerPref("Player",PlayerInfo);
        }


        public void SetGaming()
        {
            State = GameState.Gaming;
        }

        public void SetPause()
        {
            State = GameState.Pause;
        }
        
    }
    
}
