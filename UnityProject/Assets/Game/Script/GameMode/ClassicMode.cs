using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameBase;
using GameBase.UI;
using GameBase.Pool;

namespace NinjaJump
{
    public class ClassicMode : GameModeBase
    {
        
        private const string POOL_NAME = "ClassicModePool";

        private RoleDock Role;
        private List<BoardDock> BoardList;
        private Pool m_pool;
        private GameObject m_boardPrefab;
        private Transform m_boardTrans;

        public override void Init()
        {
            m_pool = PoolManager.Instance[POOL_NAME];
            InitUI();
            InitEnvirament();
            InitRole();
            InitBoard();
        }

        public override void Start()
        {
            
        }

        public override void EndGame()
        {
            PoolManager.Instance.ReleasePool(POOL_NAME);
        }

        public override void GameUpdate(float deltaTime)
        {
            
        }

        private void InitBoard()
        {
            m_boardPrefab = ResourceManager.Instance.Load<BoardDock>();
        }

        private void InitUI()
        {
            GameUI gameUI = UIManager.Instance.OpenWindow<GameUI>();
            TouchBoard = gameUI.TouchBoard;
        }

        private void InitRole()
        {
            //实例化角色
            GameObject playerPrefab = ResourceManager.Instance.Load<RoleDock>();
            m_pool.Spawn(playerPrefab,GameController.Instance.GameTrans,(obj) => {
                Role = obj.GetComponent<RoleDock>();
            });
        }

        private void InitEnvirament()
        {
            GameObject envPrefab = ResourceManager.Instance.Load<GameObject>("Prefab/Env/NinjaVillage");
            GameObject.Instantiate(envPrefab,GameController.Instance.EnvTrans);
        }

    }

}
