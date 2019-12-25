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
        private Vector2 m_viewOffset;

        public override void Init()
        {
            m_pool = PoolManager.Instance[POOL_NAME];
            m_viewOffset = new Vector2(0,ScreenSpaceHelper.ScreenHeight * 0.25f);

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
            if (Role)
            {
                GameController.Instance.ViewPos = new Vector2(0,Role.PositionFixer.Position.y + m_viewOffset.y);
            }
        }

        private void InitBoard()
        {
            int boardID = GameController.Instance.PlayerInfo.boardID;
            m_boardPrefab = SkinSetting.Asset.GetBoardSkin(boardID);
            BoardList = new List<BoardDock>();
        }

        private void InitUI()
        {
            GameUI gameUI = UIManager.Instance.OpenWindow<GameUI>();
            TouchBoard = gameUI.TouchBoard;
        }

        private void InitRole()
        {
            //实例化角色
            int roleID = GameController.Instance.PlayerInfo.roleID;
            GameObject rolePrefab = SkinSetting.Asset.GetRoleSkin(roleID);
            if (!rolePrefab)
                rolePrefab = SkinSetting.Asset.GetRoleSkin(0);

            m_pool.Spawn(rolePrefab,GameController.Instance.GameTrans,(obj) => {
                Role = obj.GetComponent<RoleDock>();
            });
            
        }

        private void InitEnvirament()
        {
            int roleID = GameController.Instance.PlayerInfo.envID;
            GameObject envPrefab = SkinSetting.Asset.GetEnvSkin(roleID);
            if (!envPrefab) 
                envPrefab = SkinSetting.Asset.GetEnvSkin(0);

            GameObject.Instantiate(envPrefab,GameController.Instance.EnvTrans);
        }

        private void CheckBoard()
        {
            
        }

    }

}
