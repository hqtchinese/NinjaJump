using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NinjaJump
{
    public class RoleActionModule : Module<RoleDock>
    {
        private RoleAnimeModule m_animeModule;
        private float m_speed = 3;
        private Vector2 m_moveDir;

        public RoleActionModule(RoleDock dock) : base(dock)
        {
            Dependence = new System.Type[]{typeof(RoleAnimeModule)};
        }
        
        public override void Awake()
        {
            m_animeModule = m_dock.GetModule<RoleAnimeModule>();
        }

        public override void Update()
        {
            if (m_dock.Status == RoleStatus.Jump)
            {
                
            }
        }

        public void Jump(Vector2 targetPos)
        {
            Debug.Log("jump");
            if (m_dock.Status != RoleStatus.Aim)
                return;

            // m_moveDir = targetPos - m_dock.transform.position;

            m_dock.Status = RoleStatus.Jump;
            m_animeModule.DoAnime();
        }
        
        
    }
}
