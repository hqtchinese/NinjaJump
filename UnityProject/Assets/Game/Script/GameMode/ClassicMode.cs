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
        private RoleDock m_role;
        private List<BoardDock> m_boardList;
        private Pool m_pool;
        private GameObject m_boardPrefab;
        private Transform m_boardTrans;
        private Vector2 m_viewOffset;
        private bool m_nextBoardIsLeft;
        private float m_curBoardHeight;

        public override void Init()
        {
            m_pool = PoolManager.Instance[POOL_NAME];
            m_viewOffset = new Vector2(0,ScreenSpaceHelper.ScreenHeight * 0.25f);
            m_nextBoardIsLeft = true;

            InitUI();
            InitEnvirament();
            InitBoard();
            InitRole();
            EventManager.Instance.Register(GameEvent.OnJumpOff,OnRoleJumpOff);
        }

        public override void Start()
        {
            GameController.Instance.SetGaming();
        }

        public override void EndGame()
        {
            PoolManager.Instance.ReleasePool(POOL_NAME);
        }

        public override void GameUpdate(float deltaTime)
        {
            if (m_role)
            {
                GameController.Instance.ViewPos = new Vector2(0,m_role.PositionFixer.Position.y + m_viewOffset.y);
            }
        }

        private void InitBoard()
        {
            int boardID = GameController.Instance.PlayerInfo.boardID;
            m_boardPrefab = SkinSetting.Asset.GetBoardSkin(boardID);
            m_boardList = new List<BoardDock>();
            for (int i = 0; i < GameSetting.Asset.BoardListLength; i++)
            {
                SpawnNextBoard();
            }
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
                m_role = obj.GetComponent<RoleDock>();
            });
            
        }

        private void InitEnvirament()
        {
            int roleID = GameController.Instance.PlayerInfo.envID;
            GameObject envPrefab = SkinSetting.Asset.GetEnvSkin(roleID);
            if (!envPrefab) 
                envPrefab = SkinSetting.Asset.GetEnvSkin(0);

            m_pool.Spawn(envPrefab,GameController.Instance.EnvTrans,(obj) => {});
        }

        private void CheckBoard()
        {
            
        }

        private Vector2 GetNextBoardPos()
        {
            Vector2 pos = new Vector2();
            if (m_nextBoardIsLeft)
            {
                pos.x = -GameSetting.Asset.BoardHorizonOffset;
            }
            else
            {
                pos.x = GameSetting.Asset.BoardHorizonOffset;
            }
            m_nextBoardIsLeft = !m_nextBoardIsLeft;

            float randomRise = Random.Range(GameSetting.Asset.BoardSpaceMin,GameSetting.Asset.BoardSpaceMax);
            m_curBoardHeight = m_curBoardHeight + randomRise;
            pos.y = m_curBoardHeight;

            return pos;
        }

        private void OnRoleJumpOff(object[] param)
        {
            BoardDock board = param.GetValue(0) as BoardDock;

            if (board)
                m_pool.Despawn(board);

            SpawnNextBoard();
        }

        private void SpawnNextBoard()
        {
            m_pool.Spawn(m_boardPrefab,GameController.Instance.GameTrans,InitBoard);
        }

        private void InitBoard(GameObject obj)
        {
            BoardDock board = obj.GetComponent<BoardDock>();
            board.Length = 5;
            board.GetModule<BoardSizeModule>().Resize();
            PositionFixer pf = board.GetComponent<PositionFixer>();
            pf.Position = GetNextBoardPos();
            pf.FixPos();
        }
    }
}
