using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameBase;

namespace NinjaJump
{
    public class GameController : MonoSingleton<GameController>
    {

        //游戏的3层
        public Transform BackLayer,GameLayer,FrontLayer;
        public TouchEventHelper TouchBoard;

        public SpriteRenderer sprite;

        public Vector2 ViewPos;
        public Vector2 Speed;

        public GameState State { get; private set; }
        public RoleController Role { get; private set; }



        protected void Start()
        {
            EventManager.Instance.Broadcast(SceneInitState.InitScene);
            CreateGameScene();
            Debug.Log(sprite.size);
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
            //实例化角色
            GameObject playerPrefab = ResourceManager.Instance.Load<RoleController>();
            Role = Instantiate(playerPrefab,GameLayer).GetComponent<RoleController>();
        }


        public void InitGame()
        {

        }


    }

    
}
