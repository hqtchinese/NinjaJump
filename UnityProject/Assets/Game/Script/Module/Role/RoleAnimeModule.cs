using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

namespace NinjaJump
{
    public class RoleAnimeModule : Module<RoleDock>
    {

        private RoleStatus Status => m_dock.Status;
        private Spine.Animation m_hangAnime;
        private Spine.Animation m_moveAnime;
        private Spine.Animation m_landAnime;

        private RoleStatus m_lastStatus;
        private Spine.AnimationState m_animationState;
        private Spine.Skeleton m_skeleton;

        public RoleAnimeModule(RoleDock dock) : base(dock)
        {

        }

        public override void Init()
        {
            m_lastStatus = RoleStatus.None;
            m_animationState = m_dock.Anime.AnimationState;
            m_skeleton = m_dock.Anime.skeleton;
            m_hangAnime = m_skeleton.Data.FindAnimation(m_dock.HangAnimeName);
            m_moveAnime = m_skeleton.Data.FindAnimation(m_dock.MoveAnimeName);
            m_landAnime = m_skeleton.Data.FindAnimation(m_dock.LandAnimeName);
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
                        m_animationState.SetAnimation(0,m_moveAnime,true);
                        break;
                    }
                    case RoleStatus.Jump:
                    {
                        break;
                    }
                    case RoleStatus.Hold:
                    {
                        m_animationState.SetAnimation(0,m_hangAnime,true);
                        break;
                    }
                    case RoleStatus.Land:
                    {
                        m_animationState.SetAnimation(0,m_landAnime,false);
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
