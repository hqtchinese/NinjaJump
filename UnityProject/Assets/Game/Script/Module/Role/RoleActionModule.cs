using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameBase;

namespace NinjaJump
{
    [RequireModule(typeof(RoleAnimeModule))]
    public class RoleActionModule : Module<RoleDock>
    {
        private RoleAnimeModule m_animeModule;
        private float m_speed = 10;
        private Vector2 m_velocity;
        private Transform transform;
        private BoardDock m_holdBoard;

        public RoleActionModule(RoleDock dock) : base(dock)
        {
        }
        
        public override void Awake()
        {
            m_animeModule = m_dock.GetModule<RoleAnimeModule>();
            m_dock.CollisionHandler.CollisionEnterEvent += OnCollisionEnter;
            transform = m_dock.transform;
        }

        public override void Init()
        {
            m_speed = GameSetting.Asset.BaseRoleSpeed;
        }

        public override void Update()
        {
            if (m_dock.Status == RoleStatus.Jump)
            {
                m_velocity.y -= GameSetting.Asset.Gravity * Time.deltaTime;
                Translate(m_velocity * Time.deltaTime);
            }
        }

        public void SetAiming()
        {
            SetStatus(RoleStatus.Aim);
        }

        public void Jump()
        {
            if (m_dock.Status != RoleStatus.Aim)
                return;


            m_dock.Dir = m_dock.Dir == FaceDir.Left ? FaceDir.Right : FaceDir.Left;
            Vector2 moveDir = m_dock.ArrowCenter.transform.position - transform.position;
            moveDir.Normalize();
            m_velocity = moveDir * m_speed;
            
            SetStatus(RoleStatus.Jump);

            EventManager.Instance.Broadcast(GameEvent.OnJumpOff,m_holdBoard);
            m_holdBoard = null;
        }
        
        public void Translate(Vector2 offset)
        {
            m_dock.PositionFixer.Position += offset;
        }

        private void OnCollisionEnter(Collision2D collision)
        {
            if(!GameController.Instance.IsGaming)
                return;
            
            if (GameSetting.Asset.BoardLayer == 1 << collision.collider.gameObject.layer)
            {
                m_holdBoard = collision.collider.GetComponent<BoardDock>();
                SetStatus(RoleStatus.Hold);
            }
        }

        private void SetStatus(RoleStatus status)
        {
            m_dock.Status = status;
        }
    }
}
