﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NinjaJump
{
    [RequireModule(typeof(RoleAnimeModule))]
    public class RoleActionModule : Module<RoleDock>
    {
        private RoleAnimeModule m_animeModule;
        private float m_speed = 10;
        private Vector2 m_moveDir;
        private Transform transform;

        public RoleActionModule(RoleDock dock) : base(dock)
        {
        }
        
        public override void Awake()
        {
            m_animeModule = m_dock.GetModule<RoleAnimeModule>();
            transform = m_dock.transform;
        }

        public override void Update()
        {
            if (m_dock.Status == RoleStatus.Jump)
            {
                transform.Translate(m_moveDir * m_speed * Time.deltaTime);
            }
        }

        public void Jump()
        {
            Debug.Log("jump");
            if (m_dock.Status != RoleStatus.Aim)
                return;

            m_moveDir = m_dock.ArrowCenter.transform.position - transform.position;
            
            m_moveDir.Normalize();
            m_dock.Status = RoleStatus.Jump;
            m_animeModule.DoAnime();
        }
        
        
    }
}
