using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NinjaJump
{
    public class RoleAnimeModule : Module<RoleDock>
    {

        private RoleStatus Status => m_dock.Status;
        private RoleStatus m_lastStatus;

        public RoleAnimeModule(RoleDock dock) : base(dock)
        {
            
        }

        public override void Init()
        {
            m_lastStatus = RoleStatus.None;
        }

        public override void Update()
        {

            Turn();
            DoAnime();
        }

        private void Turn()
        {

        }

        public void DoAnime()
        {
            if (m_lastStatus != Status)
            {
                switch (Status)
                {
                    case RoleStatus.Stand:
                    {
                        break;
                    }
                    case RoleStatus.Move:
                    {
                        break;
                    }
                    case RoleStatus.Jump:
                    {
                        break;
                    }
                    case RoleStatus.Hold:
                    {
                        break;
                    }
                    case RoleStatus.Dying:
                    {
                        break;
                    }
                    case RoleStatus.Attack:
                    {
                        break;
                    }
                }
                m_lastStatus = Status;
            }
        }

    }

}
