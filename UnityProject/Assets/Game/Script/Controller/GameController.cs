using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameBase;

namespace NinjaJump
{
    public class GameController : MonoSingleton<GameController>
    {

        public Transform GameTrans;
        public Transform EnvTrans;

        public TouchEventHelper TouchBoard;


        public Vector2 ViewPos;
        public Vector2 Speed;

        public GameState State { get; private set; }
        public RoleDock Role { get; private set; }



        protected void Start()
        {
            EventManager.Instance.Broadcast(SceneInitState.InitScene);
            CreateGameScene();
        }

        public void Update()
        {
            if (State == GameState.Gaming)
            {
                
            }
        }

        //加载游戏场景中的物体、UI等
        private void CreateGameScene()
        {
            CreateEnvirament();
            CreateRole();
        }


        private void CreateRole()
        {
            //实例化角色
            GameObject playerPrefab = ResourceManager.Instance.Load<RoleDock>();
            Role = Instantiate(playerPrefab,GameTrans).GetComponent<RoleDock>();
        }

        private void CreateEnvirament()
        {
            GameObject envPrefab = ResourceManager.Instance.Load<GameObject>("Prefab/Env/NinjaVillage");
            Instantiate(envPrefab,EnvTrans);
        }

        public void InitGame()
        {

        }


    }

    
}
